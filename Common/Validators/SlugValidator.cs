using FluentValidation;
using SquidEyes.UrlBundler.Common.Helpers;
using SquidEyes.UrlBundler.Common.Models;

namespace SquidEyes.UrlBundler.Common.Validators;

public class SlugValidator : AbstractValidator<string>
{
    public SlugValidator(string displayName, int maxLength, SlugCase slugCase)
    {
        if (!displayName.IsTrimmed())
            throw new ArgumentOutOfRangeException(nameof(displayName));

        RuleFor(v => v)
            .Configure(v => v.SetDisplayName(displayName))
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Slug(maxLength, slugCase);
    }
}
