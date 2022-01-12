using FluentValidation;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Helpers;

namespace SquidEyes.UrlBundler.Common.Validators
{
    internal class LinkSetValidator : AbstractValidator<LinkSet>
    {
        public LinkSetValidator()
        {
            RuleFor(ls => ls.LinkSetId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Slug(LinkSet.Known.MaxLengths.LinkSetId, SlugCase.AlphaDigitDash);

            RuleFor(ls => ls.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .UserName(LinkSet.Known.MaxLengths.UserName);

            RuleFor(ls => ls.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Trimmed(LinkSet.Known.MaxLengths.Title);
        }
    }
}
