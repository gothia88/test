using FluentValidation;

namespace Dtx.Security.Authorization.Features.CheckPermission;

public sealed class CheckPermissionCommandValidator : AbstractValidator<CheckPermissionCommand>
{
    public CheckPermissionCommandValidator()
    {
        RuleFor(c => c.ApplicationName).NotEmpty();
        RuleFor(c => c.EntityPath).NotEmpty();
    }
}
