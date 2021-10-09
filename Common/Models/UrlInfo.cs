using SquidEyes.UrlBundler.Common.Helpers;
using System.ComponentModel.DataAnnotations;
using static SquidEyes.UrlBundler.Common.Models.UrlInfoStatus;

namespace SquidEyes.UrlBundler.Common.Models;

[CustomValidation(typeof(UrlInfo), "ValidateStatusTitleAndExcerpt")]
public class UrlInfo
{
    [Required]
    [AbsoluteUri]
    public Uri? Uri { get; init; }

    [Required]
    [Slug("Alias", 25)]
    public string? Alias { get; init; }

    [Slug("Group", 25)]
    public string? Group { get; init; }

    [NonEmptyAndTrimmed]
    public string? Title { get; set; }

    [NonEmptyAndTrimmed]
    public string? Excerpt { get; set; }

    [Required]
    [EnumValue(typeof(UrlInfoStatus))]
    public UrlInfoStatus Status { get; set; } = New;

    public static ValidationResult ValidateStatusTitleAndExcerpt(
        UrlInfo info, ValidationContext _)
    {
        var prefix = $"{nameof(Title)} and {nameof(Excerpt)}";

        ValidationResult GetErrorResult(string setTo) => new(
            $"The {prefix} properties must be set to {setTo} when {nameof(Status)} is set to {info.Status}.",
                new List<string> { nameof(Title), nameof(Excerpt) });

        var hasTitle = !string.IsNullOrWhiteSpace(info.Title);
        var hasExcerpt = !string.IsNullOrWhiteSpace(info.Excerpt);

        var hasAny = hasTitle || hasExcerpt;
        var hasAll = hasTitle && hasExcerpt;

        return (info.Status, hasAny, hasAll) switch
        {
            (New, true, false) => GetErrorResult("null"),
            (New, false, true) => GetErrorResult("null"),
            (New, true, true) => GetErrorResult("null"),
            (New, false, false) => ValidationResult.Success!,
            (NoInfo, true, false) => GetErrorResult("null"),
            (NoInfo, false, true) => GetErrorResult("null"),
            (NoInfo, true, true) => GetErrorResult("null"),
            (NoInfo, false, false) => ValidationResult.Success!,
            (HasInfo, true, true) => ValidationResult.Success!,
            (HasInfo, true, false) => GetErrorResult("appropriate non-null values"),
            (HasInfo, false, true) => GetErrorResult("appropriate non-null values"),
            (HasInfo, false, false) => GetErrorResult("appropriate non-null values"),
            _ => throw new NotImplementedException(),
        };
    }
}
