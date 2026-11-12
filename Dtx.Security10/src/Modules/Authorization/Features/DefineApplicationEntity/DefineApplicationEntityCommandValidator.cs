using FluentValidation;

namespace Dtx.Security.Authorization.Features.DefineApplicationEntity;

public sealed class DefineApplicationEntityCommandValidator : AbstractValidator<DefineApplicationEntityCommand>
{
    public DefineApplicationEntityCommandValidator()
    {
        RuleFor(c => c.ApplicationId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        RuleFor(c => c.Path).NotEmpty().MaximumLength(500);
        RuleFor(c => c.Type).IsInEnum();
        RuleFor(c => c.AccessType).IsInEnum();
        RuleFor(c => c.RequestType).IsInEnum();
    }
}
