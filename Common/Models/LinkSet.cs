using SquidEyes.UrlBundler.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Models;

public class LinkSet
{
    public static class Known
    {
        public static class MaxLengths
        {
            public const int EmailAddress = 50;
            public const int LinkSetId = 25;
            public const int Title = 50;
            public const int UserName = 50;
        }
    }

    public string? UserName { get; init; }
    public string? LinkSetId { get; init; }
    public string? Title { get; init; }
    public List<Group> Groups { get; init; } = new();

    public static ValidationResult ValidateGroups(
        List<Group> groups, ValidationContext context)
    {
        if (groups == null || groups.Count == 0)
        {
            return context.MustBeSetTo(
                "a collection with one or more non-default Groups");
        }

        return ValidationResult.Success!;
    }
}
