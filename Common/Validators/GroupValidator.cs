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
            .Slug(Group.Known.MaxLengths.GroupId);

        RuleFor(g => g.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NonEmptyAndTrimmed(Group.Known.MaxLengths.Title, false);

        RuleFor(g => g.Links)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .ForEach(l => l.SetValidator(new LinkValidator()));
    }
}
