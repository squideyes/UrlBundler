namespace SquidEyes.UrlBundler.Common.Models;

public class Link
{
    public static class Known
    {
        public static class MaxLengths
        {
            public const int Alias = 25;
            public const int Title = 100;
            public const int Excerpt = 500;
        }
    }

    public Uri? Uri { get; init; }
    public string? Alias { get; init; }

    public string? Title { get; set; }
    public string? Excerpt { get; set; }
    public LinkStatus Status { get; set; } = LinkStatus.New;
}
