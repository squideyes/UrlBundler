using SquidEyes.UrlBundler.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Models;

public class UrlInfoSet
{
    const int SLUG_MAXLENGTH = 50;

    [Required]
    [Helpers.EmailAddress(maxLength: 50)]
    public string? UserName { get; init; }

    [Required]
    [Slug("UrlInfoSetId", SLUG_MAXLENGTH)]
    public string? UrlSetId { get; init; }

    [Required]
    [NonEmptyAndTrimmed]
    public string? Description { get; init; }

    [Required]
    [HasNonDefaultItems]
    public List<UrlInfo> UrlInfos { get; } = new();

    [Required]
    [CustomValidation(typeof(Dictionary<string, string>), "ValidateGroups")]
    public Dictionary<string, string> Groups { get; } = new();

    public static ValidationResult ValidateGroups(
        Dictionary<string, string> groups, ValidationContext context)
    {
        if (groups is null)
            return ValidationResult.Success!;

        if (groups.Any(g => !g.Key.IsSlug(SLUG_MAXLENGTH) || !g.Value.IsNonEmptyAndTrimmed()))
        {
            return context.MustBeSetTo(
                $"a Dictionary<string, string> with one or more valid GroupId+Description key/value pairs");
        }

        if (!groups.Values.IsUnique())
            context.MustBeSetTo("a Dictionary<string, string> with unique values");

        return ValidationResult.Success!;
    }
}
