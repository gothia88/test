using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.AddMembership;

internal sealed class AddMembershipCommandHandler(IAuthorizationDbContext context)
    : ICommandHandler<AddMembershipCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddMembershipCommand command, CancellationToken cancellationToken)
    {
        bool groupExists = await context.ApplicationGroups
            .AnyAsync(g => g.Id == command.ApplicationGroupId, cancellationToken);

        if (!groupExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationGroupNotFound(command.ApplicationGroupId));
        }

        bool alreadyExists = await context.Memberships
            .AnyAsync(m =>
                m.UserId == command.UserId &&
                m.ApplicationGroupId == command.ApplicationGroupId, cancellationToken);

        if (alreadyExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.MembershipAlreadyExists());
        }

        var membership = new Membership
        {
            Id = Guid.NewGuid(),
            UserId = command.UserId,
            ApplicationGroupId = command.ApplicationGroupId,
        };

        context.Memberships.Add(membership);

        await context.SaveChangesAsync(cancellationToken);

        return membership.Id;
    }
}
