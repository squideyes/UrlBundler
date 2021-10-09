using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class UtcDateTimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext context)
    {
        return context.GetValidationResult<DateTime>(
            value!, v => v.Kind == DateTimeKind.Utc, "a UTC date/time");
    }
}
