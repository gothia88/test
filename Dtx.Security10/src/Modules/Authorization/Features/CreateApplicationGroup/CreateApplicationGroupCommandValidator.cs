using FluentValidation;

namespace Dtx.Security.Authorization.Features.CreateApplicationGroup;

public sealed class CreateApplicationGroupCommandValidator : AbstractValidator<CreateApplicationGroupCommand>
{
    public CreateApplicationGroupCommandValidator()
    {
        RuleFor(c => c.ApplicationId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(1000);
    }
}
