using SquidEyes.UrlBundler.Common.Models;
using System.Text.RegularExpressions;

namespace SquidEyes.UrlBundler.Common.Helpers;

internal static class MiscExtenders
{
    private static readonly Regex isAlphaDigitDash = new(
        @"^[A-Za-z][A-Za-z0-9]*(-[A-Za-z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isUpperDigitDash = new(
        @"^[A-Z][A-Z0-9]*(-[A-Z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isLowerDigitDash = new(
        @"^[a-z][a-z0-9]*(-[a-z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isAlphaDash = new(
        @"^[A-Za-z]+(-[A-Za-z]+)*$", RegexOptions.Compiled);

    private static readonly Regex isUpperDash = new(
        @"^[A-Z][A-Z0-9]*(-[A-Z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isLowerDash = new(
        @"^[a-z][a-z0-9]*(-[a-z0-9]+)*$", RegexOptions.Compiled);

    private static readonly Regex isAlphaDigit = new(
        @"^[A-Za-z][A-Za-z0-9]*$", RegexOptions.Compiled);

    private static readonly Regex isUpperDigit = new(
        @"^[A-Z][A-Z0-9]*$", RegexOptions.Compiled);

    private static readonly Regex isLowerDigit = new(
        @"^[a-z][a-z0-9]*$", RegexOptions.Compiled);

    private static readonly Regex isEmailAddress = new(
        @"(?!.*\.\.)(^[^\.][^@\s]+@[^@\s]+\.[^@\s\.]+$)", RegexOptions.Compiled);

    public static bool IsUnique<T>(this IEnumerable<T> values) =>
        values.All(new HashSet<T>().Add);

    public static bool IsTrimmed(this string value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !char.IsWhiteSpace(value[0])
            && !char.IsWhiteSpace(value[^1]);
    }

    public static bool IsSlug(this string value, int maxLength, SlugCase slugCase)
    {
        if (maxLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (value.Length > maxLength)
            return false;

        return slugCase switch
        {
            SlugCase.AlphaDigitDash => isAlphaDigitDash.IsMatch(value),
            SlugCase.UpperDigitDash => isUpperDigitDash.IsMatch(value),
            SlugCase.LowerDigitDash => isLowerDigitDash.IsMatch(value),
            SlugCase.AlphaDash => isAlphaDash.IsMatch(value),
            SlugCase.UpperDash => isUpperDash.IsMatch(value),
            SlugCase.LowerDash => isLowerDash.IsMatch(value),
            SlugCase.AlphaDigit => isAlphaDigit.IsMatch(value),
            SlugCase.UpperDigit => isUpperDigit.IsMatch(value),
            SlugCase.LowerDigit => isLowerDigit.IsMatch(value),
            _ => throw new ArgumentOutOfRangeException(nameof(slugCase))
        };
    }

    public static bool IsEmailAddress(this string value, int maxLength = 50)
    {
        if (maxLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (value!.Length > maxLength)
            return false;

        return isEmailAddress.IsMatch(value);
    }
}
