using FluentAssertions;
using SquidEyes.UrlBundler.Common.Validators;
using System;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class SlugValidatorTests
    {
        [Theory]
        [InlineData("XXX", 5)]
        [InlineData("XX-XX", 5)]
        [InlineData("Xx-Xx", 5)]
        [InlineData("1X-1X", 5)]
        [InlineData("X1-X1", 5)]
        [InlineData("x1-x1", 5)]
        [InlineData("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", 62)]
        [InlineData("", 5, "'Test' must not be empty.")]
        [InlineData(" ", 5, "'Test' must not be empty.")]
        [InlineData("-", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("X--X", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("-XX", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("XX-", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData(" XXX", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("XXX ", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("X@X", 5, "'Test' must be a valid Slug (5 chars, max).")]
        [InlineData("XXXXXX", 5, "'Test' must be a valid Slug (5 chars, max).")]
        public void Validate(string value, int maxLength, params string[] errors)
        {
            var validator = new SlugValidator("Test", maxLength);

            var result = validator.Validate(value);

            result.Errors.Count.Should().Be(errors.Length);

            for(int i = 0; i < result.Errors.Count; i++)
            {
                if (errors[i] != result.Errors[i].ErrorMessage)
                    throw new ArgumentOutOfRangeException(nameof(result));
            }
        }
    }
}
