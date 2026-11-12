using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.GrantPermission;

internal sealed class GrantPermissionCommandHandler(IAuthorizationDbContext context)
    : ICommandHandler<GrantPermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(GrantPermissionCommand command, CancellationToken cancellationToken)
    {
        bool groupExists = await context.ApplicationGroups
            .AnyAsync(g => g.Id == command.ApplicationGroupId, cancellationToken);

        if (!groupExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationGroupNotFound(command.ApplicationGroupId));
        }

        bool entityExists = await context.ApplicationEntities
            .AnyAsync(e => e.Id == command.ApplicationEntityId, cancellationToken);

        if (!entityExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationEntityNotFound(command.ApplicationEntityId));
        }

        bool alreadyExists = await context.Permissions
            .AnyAsync(p =>
                p.ApplicationGroupId == command.ApplicationGroupId &&
                p.ApplicationEntityId == command.ApplicationEntityId, cancellationToken);

        if (alreadyExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.PermissionAlreadyExists());
        }

        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            ApplicationGroupId = command.ApplicationGroupId,
            ApplicationEntityId = command.ApplicationEntityId,
        };

        context.Permissions.Add(permission);

        await context.SaveChangesAsync(cancellationToken);

        return permission.Id;
    }
}
