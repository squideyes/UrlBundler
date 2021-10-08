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

            var result = attrib.GetValidationResult(uri, new ValidationContext(uri))!;

            Validate(result, isValid, "The  property must be set to an absolute Uri.");
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

            var result = attrib.GetValidationResult(
                value, new ValidationContext(value))!;

            Validate(result, isValid, "The  property must be set to a valid email address.");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void EnumValueAttributeShouldWork(bool isValid)
        {
            var attrib = new EnumValueAttribute(typeof(Color));

            var value = isValid ? Color.Red : 0;

            var result = attrib.GetValidationResult(
                value, new ValidationContext(value))!;

            Validate(result, isValid, "The  property must be set to a defined SquidEyes.UnitTests.AttributeTests+Color.");
        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        [InlineData(false, true, true)]
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

            var result = attrib.GetValidationResult(
                list, new ValidationContext(this))!;

            Validate(result, isValid,
                "The  property must be set to a collection with one or more non-default elements.");
        }

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
