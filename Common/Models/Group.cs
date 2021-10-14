namespace SquidEyes.UrlBundler.Common.Models;

public class Group
{
    public static class Known
    {
        public static class MaxLengths
        {
            public const int GroupId = 25;
            public const int Title = 100;
        }
    }

    public string? GroupId { get; init; } = "default";
    public string? Title { get; init; } = "Default";
    public List<Link> Links { get; init; } = new();
}
