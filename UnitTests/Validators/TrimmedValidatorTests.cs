using SquidEyes.UrlBundler.Common.Validators;
using Xunit;

namespace SquidEyes.UnitTests
{
    public class TrimmedValidatorTests
    {
        private const string ERROR_MESSAGE =
            "'Test' must be trimmed (10 chars, max).";

        [Theory]
        [InlineData("XXX")]
        [InlineData("", ERROR_MESSAGE)]
        [InlineData(" XXX", ERROR_MESSAGE)]
        [InlineData("XXX ", ERROR_MESSAGE)]
        public void X(string value, params string[] errors)
        {
            var validator = new TrimmedValidator("Test", 10);

            var result = validator.Validate(value);

            TestHelper.ThrowOnError(result, errors, m => m);
        }
    }
}
