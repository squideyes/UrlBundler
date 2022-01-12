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
}
