using FluentAssertions;
using FluentValidation.Results;
using System;

namespace SquidEyes.UnitTests
{
    internal static class TestHelper
    {
        public static void ThrowOnError(ValidationResult result, 
            string[] errors, Func<string, string> getError)
        {
            result.Errors.Count.Should().Be(errors.Length);

            for (int i = 0; i < result.Errors.Count; i++)
            {
                if (getError(errors[i]) != result.Errors[i].ErrorMessage)
                    throw new ArgumentOutOfRangeException(nameof(result));
            }
        }
    }
}
