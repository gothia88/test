using FluentValidation;

namespace Dtx.Security.Authorization.Features.GrantPermission;

public sealed class GrantPermissionCommandValidator : AbstractValidator<GrantPermissionCommand>
{
    public GrantPermissionCommandValidator()
    {
        RuleFor(c => c.ApplicationGroupId).NotEmpty();
        RuleFor(c => c.ApplicationEntityId).NotEmpty();
    }
}
