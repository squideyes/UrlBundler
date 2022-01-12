using FluentValidation;
using SquidEyes.UrlBundler.Common.Helpers;
using SquidEyes.UrlBundler.Common.Models;

namespace SquidEyes.UrlBundler.Common.Validators;

public class LinkValidator : AbstractValidator<Link>
{
    public LinkValidator()
    {
        RuleFor(l => l.Alias).SetValidator(new SlugValidator(
            nameof(Link.Alias), Link.Known.MaxLengths.Alias, SlugCase.LowerDigitDash)!);

        RuleFor(l => l.Excerpt)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Trimmed(Link.Known.MaxLengths.Excerpt);

        RuleFor(l => l.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Trimmed(Link.Known.MaxLengths.Title);

        RuleFor(l => l)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .StatusLinkAndExcerpt();

        RuleFor(l => l.Uri)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(v => v!.IsAbsoluteUri)
            .WithMessage("'{PropertyName}' must be an absolute URI.");
    }
}
