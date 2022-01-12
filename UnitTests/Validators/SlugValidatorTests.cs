using Combinatorics.Collections;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Validators;
using System;
using Xunit;
using static SquidEyes.UrlBundler.Common.Models.SlugCase;

namespace SquidEyes.UnitTests
{
    public class SlugValidatorTests
    {
        private const string MUST_NOT_BE_EMPTY = "'Test' must not be empty.";

        private const string MUST_BE_A_VALID_SLUG =
            "'Test' must be a valid Slug (5 {0} chars, max).";

        [Theory]
        [InlineData(AlphaDigitDash)]
        [InlineData(UpperDigitDash)]
        [InlineData(LowerDigitDash)]
        [InlineData(AlphaDash)]
        [InlineData(UpperDash)]
        [InlineData(LowerDash)]
        [InlineData(AlphaDigit)]
        [InlineData(UpperDigit)]
        [InlineData(LowerDigit)]
        public void GoodSlugsYieldNoValidatorErrors(SlugCase slugCase)
        {
            const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string LOWER = "abcdefghijklmnopqrstuvwxyz";
            const string DIGITS = "1234567890";
            const string DASH = "-";

            var chars = slugCase switch
            {
                AlphaDigitDash => UPPER + LOWER + DIGITS + DASH,
                UpperDigitDash => UPPER + DIGITS + DASH,
                LowerDigitDash => LOWER + DIGITS + DASH,
                AlphaDash => UPPER + LOWER + DASH,
                UpperDash => UPPER + DASH,
                LowerDash => LOWER + DASH,
                AlphaDigit => UPPER + LOWER + DIGITS,
                UpperDigit => UPPER + DIGITS,
                LowerDigit => LOWER + DIGITS,
                _ => throw new ArgumentOutOfRangeException(nameof(slugCase))
            };

            var validator = new SlugValidator("Test", 3, AlphaDigitDash);

            var variations = new Variations<char>(
                chars, 3, GenerateOption.WithRepetition);

            foreach (var v in variations)
            {
                if (v[0] == '-' || v[2] == '-' || char.IsDigit(v[0]))
                    continue;

                var text = string.Join("", v);

                var result = validator.Validate(text);

                if (result.Errors.Count != 0)
                    throw new ArgumentOutOfRangeException(nameof(text));
            }
        }

        [Theory]
        [InlineData(AlphaDigitDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDigitDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDigitDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "X--x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "-Xx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "Xx-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, " Xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "Xxx ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "X@x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigitDash, "Xxxxxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDigitDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDigitDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "X--X", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "-XX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "XX-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, " XXX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "XXX ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "X@X", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigitDash, "XXXXXX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDigitDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDigitDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "x--x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "-xx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "xx-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, " xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "xxx ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "x@x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigitDash, "xxxxxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "X--x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "-Xx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "Xx-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, " Xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "Xxx ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "X@x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "Xxxxxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDash, "Xx-xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "X--X", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "-XX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "XX-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, " XXX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "XXX ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "X@X", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "XXXXXX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDash, "XX-XXX", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDash, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDash, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "x--x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "-xx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "xx-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, " xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "xxx ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "x@x", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "xxxxxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDash, "xx-xxx", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDigit, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(AlphaDigit, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "Xx--1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "-Xx1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "Xx1-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, " Xx1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "Xx1 ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "Xx@1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(AlphaDigit, "Xxxx11", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDigit, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(UpperDigit, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "x1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "X--1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "-X1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "X1-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, " XX1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "XX1 ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "X@1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(UpperDigit, "XXXX11", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDigit, " ", 5, MUST_NOT_BE_EMPTY)]
        [InlineData(LowerDigit, "-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "X1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "x--1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "-x1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "x1-", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, " xx1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "xx1 ", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "x@1", 5, MUST_BE_A_VALID_SLUG)]
        [InlineData(LowerDigit, "xxxx11", 5, MUST_BE_A_VALID_SLUG)]
        public void BadValuesReturnValidatorErrors(
            SlugCase slugCase, string value, int maxLength, params string[] errors)
        {
            var validator = new SlugValidator("Test", maxLength, slugCase);

            var result = validator.Validate(value);

            TestHelper.ThrowOnError(result, errors, m => string.Format(m, slugCase));
        }
    }
}
