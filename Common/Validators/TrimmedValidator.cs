using FluentValidation;
using SquidEyes.UrlBundler.Common.Helpers;

namespace SquidEyes.UrlBundler.Common.Validators
{
    public class TrimmedValidator : AbstractValidator<string>
    {
        public TrimmedValidator(string displayName, int maxLength)
        {
            if (!displayName.IsTrimmed())
                throw new ArgumentOutOfRangeException(nameof(displayName));

            RuleFor(v => v)
                .Configure(v => v.SetDisplayName(displayName))
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Trimmed(maxLength);
        }
    }
}
