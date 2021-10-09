using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class NonEmptyAndTrimmedAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        return context.GetValidationResult<string>(value,
            v => v.IsNonEmptyAndTrimmed(), $"a trimmed non-empty string");
    }
}
