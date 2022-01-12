using FluentValidation;
using SquidEyes.UrlBundler.Common.Models;
using static SquidEyes.UrlBundler.Common.Models.LinkStatus;

namespace SquidEyes.UrlBundler.Common.Helpers;

public static class IRuleBuilderExtenders
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
        this IRuleBuilder<T, string?> ruleBuilder, int maxLength, SlugCase slugCase)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        return ruleBuilder.Custom((item, context) =>
        {
            if (item?.IsSlug(maxLength, slugCase) != true)
                context.AddFailure($"'{context.PropertyName}' must be a valid Slug ({maxLength:N0} {slugCase} chars, max).");
        });
    }

    public static IRuleBuilderOptionsConditions<T, string?> Trimmed<T>(
        this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        return ruleBuilder.Custom((item, context) =>
        {
            if (item?.IsTrimmed() != true || item?.Length > maxLength)
                context.AddFailure($"'{context.PropertyName}' must be trimmed ({maxLength:N0} chars, max).");
        });
    }

    public static IRuleBuilderOptionsConditions<T, Link?> StatusLinkAndExcerpt<T>(
        this IRuleBuilder<T, Link?> ruleBuilder)
    {
        return ruleBuilder.Custom((link, context) =>
        {
            var hasTitle = !string.IsNullOrWhiteSpace(link?.Title);
            var hasExcerpt = !string.IsNullOrWhiteSpace(link?.Excerpt);

            var hasAny = hasTitle || hasExcerpt;
            var hasBoth = hasTitle && hasExcerpt;

            if (!HasGoodStatusTitleAndExcerpt(link!, hasAny, hasBoth))
                context.AddFailure(GetStatusTitleAndExcerptMessage(link!, hasAny, hasBoth));
        });
    }

    private static bool HasGoodStatusTitleAndExcerpt(Link link, bool hasAny, bool hasBoth)
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
        string GetMessage(string value) => new(
            $"'{nameof(link.Title)}' and '{nameof(link.Excerpt)}' must be {value} when {nameof(link.Status)} is {link.Status}.");

        return (link.Status, hasAny, hasBoth) switch
        {
            (New, true, false) => GetMessage("null"),
            (New, false, true) => GetMessage("null"),
            (New, true, true) => GetMessage("null"),
            (NoInfo, true, false) => GetMessage("null"),
            (NoInfo, false, true) => GetMessage("null"),
            (NoInfo, true, true) => GetMessage("null"),
            (HasInfo, true, false) => GetMessage("non-null"),
            (HasInfo, false, true) => GetMessage("non-null"),
            (HasInfo, false, false) => GetMessage("non-null"),
            _ => null!
        };
    }
}
