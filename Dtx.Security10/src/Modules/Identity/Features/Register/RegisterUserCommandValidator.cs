using FluentValidation;

namespace Dtx.Security.Identity.Features.Register;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8).MaximumLength(100);
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.EmailAddress).EmailAddress().When(c => !string.IsNullOrWhiteSpace(c.EmailAddress));
        RuleFor(c => c.CellPhoneNumber).MaximumLength(20);
        RuleFor(c => c.NationalCode).MaximumLength(20);
    }
}
