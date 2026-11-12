using FluentValidation;

namespace Dtx.Security.Identity.Features.ChangePassword;

public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.CurrentPassword).NotEmpty();
        RuleFor(c => c.NewPassword).NotEmpty().MinimumLength(8).MaximumLength(100);
    }
}
