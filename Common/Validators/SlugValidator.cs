using FluentValidation;
using SquidEyes.UrlBundler.Common.Helpers;

namespace SquidEyes.UrlBundler.Common.Validators;

public class SlugValidator : AbstractValidator<string>
{
    public SlugValidator(string displayName, int maxLength)
    {
        if (!displayName.IsNonEmptyAndTrimmed())
            throw new ArgumentOutOfRangeException(nameof(displayName));

        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        RuleFor(v => v)
            .Configure(v => v.SetDisplayName(displayName))
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Slug(maxLength);
    }
}
