using FluentValidation;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Helpers;

namespace SquidEyes.UrlBundler.Common.Validators;

public class LinkValidator : AbstractValidator<Link>
{
    public LinkValidator()
    {
        RuleFor(l => l.Alias).SetValidator(new SlugValidator(
            nameof(Link.Alias), Link.Known.MaxLengths.Alias)!);


        //RuleFor(l => l.Alias)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .Slug(nameof(Link.Alias), Link.Known.MaxLengths.Alias);

        //RuleFor(l => l.Excerpt)
        //    .NonEmptyAndTrimmed(Link.Known.MaxLengths.Excerpt, true);

        //RuleFor(l => l.Title)
        //    .NonEmptyAndTrimmed(Link.Known.MaxLengths.Title, true);

        //RuleFor(l => l)
        //    .Cascade(CascadeMode.Stop)
        //    .NotNull()
        //    .GoodStatusLinkAndExcerpt();

        //RuleFor(l => l.Uri)
        //    .Cascade(CascadeMode.Stop)
        //    .NotNull()
        //    .Must(v => v!.IsAbsoluteUri)
        //    .WithMessage("'{PropertyName}' must be an absolute URI.");
    }
}
