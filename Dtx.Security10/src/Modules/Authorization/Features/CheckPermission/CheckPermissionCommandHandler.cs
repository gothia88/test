using Dtx.Security.Application.Abstractions.Authentication;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.CheckPermission;

internal sealed class CheckPermissionCommandHandler(
    IAuthorizationDbContext context,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CheckPermissionCommand, CheckPermissionResponse>
{
    public async Task<Result<CheckPermissionResponse>> Handle(CheckPermissionCommand command, CancellationToken cancellationToken)
    {
        ClientApplication? application = await context.Applications
            .SingleOrDefaultAsync(a => a.Name == command.ApplicationName, cancellationToken);

        if (application is null)
        {
            return Result.Failure<CheckPermissionResponse>(AuthorizationErrors.ApplicationNotFound(command.ApplicationName));
        }

        ApplicationEntity? entity = await context.ApplicationEntities
            .SingleOrDefaultAsync(e =>
                e.ApplicationId == application.Id &&
                e.Path == command.EntityPath, cancellationToken);

        if (entity is null)
        {
            return Result.Failure<CheckPermissionResponse>(
                AuthorizationErrors.ApplicationEntityNotFound(command.ApplicationName, command.EntityPath));
        }

        bool granted = await context.Memberships
            .AnyAsync(m =>
                m.UserId == userContext.UserId &&
                m.ApplicationGroup!.Permissions.Any(p => p.ApplicationEntityId == entity.Id), cancellationToken);

        context.CheckPermissionLogs.Add(new CheckPermissionLog
        {
            Id = Guid.NewGuid(),
            UserId = userContext.UserId,
            ApplicationEntityId = entity.Id,
            IsGranted = granted,
            CheckDateTime = dateTimeProvider.UtcNow,
        });

        await context.SaveChangesAsync(cancellationToken);

        return new CheckPermissionResponse(granted);
    }
}
