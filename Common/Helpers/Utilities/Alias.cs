using System.Text;

namespace SquidEyes.UrlBundler.Common.Helpers;

public struct Alias 
{
    internal const int Length = 8;

    internal static readonly char[] ValidChars =
        "RCLTKJ4BQWHEPF9ZN8AG3V2UDYS675XM".ToCharArray();

    private static readonly Random random = new();

    public static string Create()
    {
        var sb = new StringBuilder(Length);

        for (int i = 0; i < Length; i++)
            sb.Append(ValidChars[random.Next(ValidChars.Length)]);

        return sb.ToString();
    }
}
