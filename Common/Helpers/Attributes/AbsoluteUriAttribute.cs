using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class AbsoluteUriAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        return context.GetValidationResult<Uri>(value,
            v => v.IsAbsoluteUri, $"an absolute Uri");
    }
}
