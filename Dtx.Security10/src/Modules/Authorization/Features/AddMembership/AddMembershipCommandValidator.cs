using FluentValidation;

namespace Dtx.Security.Authorization.Features.AddMembership;

public sealed class AddMembershipCommandValidator : AbstractValidator<AddMembershipCommand>
{
    public AddMembershipCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.ApplicationGroupId).NotEmpty();
    }
}
