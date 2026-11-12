using FluentValidation;

namespace Dtx.Security.Identity.Features.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}
