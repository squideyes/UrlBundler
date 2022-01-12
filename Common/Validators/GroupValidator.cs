using FluentValidation;
using SquidEyes.UrlBundler.Common.Models;
using SquidEyes.UrlBundler.Common.Helpers;

namespace SquidEyes.UrlBundler.Common.Validators;

public class GroupValidator : AbstractValidator<Group>
{
    public GroupValidator()
    {
        RuleFor(g => g.GroupId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Slug(Group.Known.MaxLengths.GroupId, SlugCase.AlphaDigitDash);

        RuleFor(g => g.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Trimmed(Group.Known.MaxLengths.Title);

        RuleFor(g => g.Links)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .ForEach(l => l.SetValidator(new LinkValidator()));
    }
}
