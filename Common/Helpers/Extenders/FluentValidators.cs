using FluentValidation;
using SquidEyes.UrlBundler.Common.Models;
using static SquidEyes.UrlBundler.Common.Models.LinkStatus;

namespace SquidEyes.UrlBundler.Common.Helpers;

public static class FluentValidators
{
    public static IRuleBuilderOptionsConditions<T, string?> UserName<T>(
        this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        return ruleBuilder.Custom((item, context) =>
        {
            if (item?.IsEmailAddress(maxLength) == true)
                context.AddFailure($"'{context.PropertyName}' must be a valid email address.");
        });
    }

    public static IRuleBuilderOptionsConditions<T, string?> Slug<T>(
        this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        return ruleBuilder.Custom((item, context) =>
        {
            if (item?.IsSlug(maxLength) != true)
                context.AddFailure($"'{context.PropertyName}' must be a valid Slug ({maxLength:N0} chars, max).");
        });
    }

    public static IRuleBuilderOptionsConditions<T, string?> NonEmptyAndTrimmed<T>(
        this IRuleBuilder<T, string?> ruleBuilder, int maxLength, bool optional)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        return ruleBuilder.Custom((item, context) =>
        {
            if (!optional || item != null)
            {
                if (item?.IsNonEmptyAndTrimmed() != true || item?.Length > maxLength)
                    context.AddFailure($"'{context.PropertyName}' must be trimmed and non-empty ({maxLength:N0} chars, max).");
            }
        });
    }

    public static IRuleBuilderOptionsConditions<T, Link?> GoodStatusLinkAndExcerpt<T>(
        this IRuleBuilder<T, Link?> ruleBuilder)
    {
        return ruleBuilder.Custom((link, context) =>
        {
            var hasTitle = !string.IsNullOrWhiteSpace(link?.Title);
            var hasExcerpt = !string.IsNullOrWhiteSpace(link?.Excerpt);

            var hasAny = hasTitle || hasExcerpt;
            var hasBoth = hasTitle && hasExcerpt;

            if (!GoodStatusTitleAndExcerpt(link!, hasAny, hasBoth))
                context.AddFailure(GetStatusTitleAndExcerptMessage(link!, hasAny, hasBoth));
        });
    }

    private static bool GoodStatusTitleAndExcerpt(Link link, bool hasAny, bool hasBoth)
    {
        return (link.Status, hasAny, hasBoth) switch
        {
            (New, false, false) => true,
            (NoInfo, false, false) => true,
            (HasInfo, true, true) => true,
            _ => false
        };
    }

    private static string GetStatusTitleAndExcerptMessage(Link link, bool hasAny, bool hasBoth)
    {
        string GetErrorResult(string value) => new(
            $"'{nameof(link.Title)}' and '{nameof(link.Excerpt)}' must be {value} when {nameof(link.Status)} is {link.Status}.");

        return (link.Status, hasAny, hasBoth) switch
        {
            (New, true, false) => GetErrorResult("null"),
            (New, false, true) => GetErrorResult("null"),
            (New, true, true) => GetErrorResult("null"),
            (NoInfo, true, false) => GetErrorResult("null"),
            (NoInfo, false, true) => GetErrorResult("null"),
            (NoInfo, true, true) => GetErrorResult("null"),
            (HasInfo, true, false) => GetErrorResult("appropriate non-null values"),
            (HasInfo, false, true) => GetErrorResult("appropriate non-null values"),
            (HasInfo, false, false) => GetErrorResult("appropriate non-null values"),
            _ => null!
        };
    }
}
