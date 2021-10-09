using FluentAssertions;
using SquidEyes.UrlBundler.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class AttributeTests
    {
        private class TestInstance
        {
        }

        private enum Color
        {
            Red = 1,
            Green,
            Blue
        }

        [Theory]
        [InlineData(UriKind.Absolute, "http://cnn.com", true)]
        [InlineData(UriKind.Relative, "data", false)]
        public void AbsoluteUriAttributeShouldWork(
            UriKind uriKind, string uriString, bool isValid)
        {
            var uri = new Uri(uriString, uriKind);

            var attrib = new AbsoluteUriAttribute();

            var result = attrib.GetValidationResult(uri, GetContext())!;

            Validate(result, isValid, MustBeSetTo("an absolute Uri."));
        }

        [Theory]
        [InlineData("somedude@someco.com", true)]
        [InlineData("some-dude@someco.com", true)]
        [InlineData("some-dude@some-co.com", false)]
        [InlineData("some.dude@someco.com", true)]
        [InlineData("somedude@some.co.com", true)]
        [InlineData(" somedude@someco.com", false)]
        [InlineData("somedude@someco.com ", false)]
        [InlineData("@someco.com", false)]
        [InlineData("somedude@", false)]
        [InlineData("some dude@someco.com", false)]
        [InlineData("somedude@some co.com", false)]
        [InlineData("somedude@someco com", false)]
        [InlineData("somedude@someco.", false)]
        [InlineData("somedude@someco. com", false)]
        [InlineData("somedude@someco .com", false)]
        [InlineData("extralong@emailaddress.com", false)]
        [InlineData("somedude @someco.com", false)]
        [InlineData("somedude@ someco.com", false)]
        public void EmailAddressAttributeShouldWork(string value, bool isValid)
        {
            var attrib = new UrlBundler.Common.Helpers.EmailAddressAttribute(20);

            var result = attrib.GetValidationResult(value, GetContext())!;

            Validate(result, isValid, MustBeSetTo("a valid email address."));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void EnumValueAttributeShouldWork(bool isValid)
        {
            var attrib = new EnumValueAttribute(typeof(Color));

            var value = isValid ? Color.Red : 0;

            var result = attrib.GetValidationResult(value, GetContext())!;

            Validate(result, isValid, MustBeSetTo(
                "a defined SquidEyes.UnitTests.AttributeTests+Color."));
        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
        [InlineData(true, true, false)]
        public void HasNonDefaultItemsAttributeShouldWork(
            bool isNull, bool hasItems, bool isValid)
        {
            var attrib = new HasNonDefaultItemsAttribute();

            List<string> list = null!;

            if (!isNull)
            {
                list = new List<string>();

                if (hasItems)
                    list.Add("ABC123");
            }

            var result = attrib.GetValidationResult(list, GetContext())!;

            if (result == ValidationResult.Success)
                return;

            Validate(result, isValid,
                MustBeSetTo("a collection with one or more non-default elements."));
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("ABC ", false)]
        [InlineData(" ABC", false)]
        public void NonEmptyAndTrimmedAttributeShouldWork(string value, bool isValid)
        {
            var attrib = new NonEmptyAndTrimmedAttribute();

            var result = attrib.GetValidationResult(value, GetContext())!;

            Validate(result, isValid, MustBeSetTo("a trimmed non-empty string."));
        }

        [Theory]
        [InlineData("ABC123", true)]
        [InlineData(null, true)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("ABC123 ", false)]
        [InlineData(" ABC123", false)]
        [InlineData("XXXXXXXXXXX", false)]
        public void SlugAttributeShouldWork(string value, bool isValid)
        {
            var attrib = new SlugAttribute("TEST", 10);

            var result = attrib.GetValidationResult(value, GetContext())!;

            Validate(result, isValid, MustBeSetTo("a valid TEST."));
        }

        [Theory]
        [InlineData(DateTimeKind.Utc, true)]
        [InlineData(DateTimeKind.Unspecified, false)]
        [InlineData(DateTimeKind.Local, false)]
        public void UtcDateTimeAttributeShoudlWork(DateTimeKind kind, bool isValid)
        {
            var attrib = new UtcDateTimeAttribute();

            var value = new DateTime(2021, 1, 1, 0, 0, 0, kind);

            var result = attrib.GetValidationResult(value, GetContext())!;

            Validate(result, isValid, MustBeSetTo("a UTC date/time."));
        }

        private static string MustBeSetTo(string suffix) => 
            $"The  property must be set to {suffix}";

        private static ValidationContext GetContext() => new(new TestInstance());

        private static void Validate(ValidationResult result, bool isValid, string message)
        {
            if (isValid)
            {
                result.Should().BeEquivalentTo(ValidationResult.Success);
            }
            else
            {
                result.Should().BeEquivalentTo(
                    new ValidationResult(message, new List<string> { null! }));
            }
        }
    }
}
