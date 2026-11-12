using FluentValidation;

namespace Dtx.Security.Authorization.Features.RegisterApplication;

public sealed class RegisterApplicationCommandValidator : AbstractValidator<RegisterApplicationCommand>
{
    public RegisterApplicationCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(1000);
    }
}
