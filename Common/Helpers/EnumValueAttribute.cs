using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SquidEyes.UrlBundler.Common.Helpers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class EnumValueAttribute : ValidationAttribute
{
    private readonly Type type;

    public EnumValueAttribute(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (!type.GetTypeInfo().IsEnum)
            throw new ArgumentOutOfRangeException(nameof(type));

        this.type = type;
    }

    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        return context.GetValidationResult<Enum>(value,
            v => Enum.IsDefined(type, v), $"a defined {type.FullName}");
    }
}
