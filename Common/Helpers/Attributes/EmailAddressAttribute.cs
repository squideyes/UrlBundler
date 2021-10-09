using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class EmailAddressAttribute : ValidationAttribute
{
    private readonly int maxLength;

    public EmailAddressAttribute(int maxLength)
    {
        if (maxLength <= 5)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        this.maxLength = maxLength;
    }

    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        return context.GetValidationResult<string>(value,
            v => v.IsEmailAddress(maxLength), $"a valid email address");
    }
}
