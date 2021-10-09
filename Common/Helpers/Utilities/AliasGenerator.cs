using System.Text;

namespace SquidEyes.UrlBundler.Common.Helpers;

public struct AliasGenerator : IEquatable<AliasGenerator>
{
    internal const int Length = 8;

    internal static readonly char[] ValidChars =
        "RCLTKJ4BQWHEPF9ZN8AG3V2UDYS675XM".ToCharArray();

    private static readonly Random random = new();

    public AliasGenerator(string value)
    {
        if (!value.IsAlias())
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }

    public string Value { get; private set; }

    public bool Equals(AliasGenerator other) => Value.Equals(other.Value);

    public override bool Equals(object? other) =>
        other is AliasGenerator v && Equals(v);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;

    public static AliasGenerator Create()
    {
        var sb = new StringBuilder(Length);

        for (int i = 0; i < Length; i++)
            sb.Append(ValidChars[random.Next(32)]);

        return new AliasGenerator(sb.ToString());
    }

    public static implicit operator AliasGenerator(string value) => new(value);

    public static implicit operator string(AliasGenerator value) => value.ToString();

    public static bool operator ==(AliasGenerator left, AliasGenerator right) => left.Equals(right);

    public static bool operator !=(AliasGenerator left, AliasGenerator right) => !(left == right);
}
