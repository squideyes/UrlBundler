using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SquidEyes.UrlBundler.Common.Helpers;

internal static class MiscExtenders
{
    private static readonly Regex isSlug = new(
        @"^[A-Za-z0-9]+(-[A-Za-z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isEmailAddress = new(
        @"(?!.*\.\.)(^[^\.][^@\s]+@[^@\s]+\.[^@\s\.]+$)", RegexOptions.Compiled);

    public static bool IsUnique<T>(this IEnumerable<T> values) =>
        values.All(new HashSet<T>().Add);

    public static ValidationResult MustBeSetTo(
       this ValidationContext context, string format, params object[] args)
    {
        var suffix = string.Format(format, args);

        return new ValidationResult(
            $"The {context.MemberName} property must be set to {suffix}.",
                new List<string> { context.MemberName! });
    }

    public static bool IsNonEmptyAndTrimmed(this string value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !char.IsWhiteSpace(value[0])
            && !char.IsWhiteSpace(value[^1]);
    }

    public static bool IsSlug(this string value, int maxLength)
    {
        if (maxLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (!value.IsNonEmptyAndTrimmed())
            return false;

        if (value.Length > maxLength)
            return false;

        return isSlug.IsMatch(value);
    }

    public static bool IsEmailAddress(this string value, int maxLength = 50)
    {
        if (maxLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (!value.IsNonEmptyAndTrimmed())
            return false;

        if (value.Length > maxLength)
            return false;

        return isEmailAddress.IsMatch(value);
    }

    public static ValidationResult GetValidationResult<T>(
        this ValidationContext context,
        object? value, Func<T, bool> isValid, string message)
    {
        if (value is null)
            return ValidationResult.Success!;
        else if (value is T v && !isValid(v))
            return context.MustBeSetTo(message);
        else
            return ValidationResult.Success!;
    }
}
