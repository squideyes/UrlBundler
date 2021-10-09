using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SquidEyes.UrlBundler.Common.Helpers;

public class HasNonDefaultItemsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(
        object? value, ValidationContext context)
    {
        const string ERROR_MESSAGE =
            "a collection with one or more non-default elements";

        static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
                return Activator.CreateInstance(type)!;
            else
                return null!;
        }

        if (Equals(value, null))
            return ValidationResult.Success!;

        var list = value as IEnumerable;

        if (Equals(list, null))
            return context.MustBeSetTo(ERROR_MESSAGE);

        var defaultValue = GetDefaultValue(value.GetType());

        int count = 0;

        foreach (var item in list)
        {
            count++;

            if (Equals(item, defaultValue))
                return context.MustBeSetTo(ERROR_MESSAGE);
        }

        if (count == 0)
            return context.MustBeSetTo(ERROR_MESSAGE);

        return ValidationResult.Success!;
    }
}
