using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.CreateApplicationGroup;

internal sealed class CreateApplicationGroupCommandHandler(IAuthorizationDbContext context)
    : ICommandHandler<CreateApplicationGroupCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateApplicationGroupCommand command, CancellationToken cancellationToken)
    {
        bool applicationExists = await context.Applications
            .AnyAsync(a => a.Id == command.ApplicationId, cancellationToken);

        if (!applicationExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationNotFound(command.ApplicationId));
        }

        var group = new ApplicationGroup
        {
            Id = Guid.NewGuid(),
            ApplicationId = command.ApplicationId,
            Name = command.Name,
            Description = command.Description,
        };

        context.ApplicationGroups.Add(group);

        await context.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}
