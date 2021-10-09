using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SlugAttribute : ValidationAttribute
{
    private readonly string slugKind;
    private readonly int maxLength;

    public SlugAttribute(string slugKind, int maxLength)
    {
        if (!slugKind.IsNonEmptyAndTrimmed())
            throw new ArgumentOutOfRangeException(nameof(slugKind));

        this.slugKind = slugKind;
        this.maxLength = maxLength;
    }

    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        return context.GetValidationResult<string>(
            value, v => v.IsSlug(maxLength), $"a valid {slugKind}");
    }
}
