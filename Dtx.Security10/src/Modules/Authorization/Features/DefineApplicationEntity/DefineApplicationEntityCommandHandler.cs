using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.DefineApplicationEntity;

internal sealed class DefineApplicationEntityCommandHandler(IAuthorizationDbContext context)
    : ICommandHandler<DefineApplicationEntityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DefineApplicationEntityCommand command, CancellationToken cancellationToken)
    {
        bool applicationExists = await context.Applications
            .AnyAsync(a => a.Id == command.ApplicationId, cancellationToken);

        if (!applicationExists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationNotFound(command.ApplicationId));
        }

        var entity = new ApplicationEntity
        {
            Id = Guid.NewGuid(),
            ApplicationId = command.ApplicationId,
            Name = command.Name,
            Title = command.Title ?? command.Name,
            Path = command.Path,
            Type = command.Type,
            AccessType = command.AccessType,
            RequestType = command.RequestType,
            Description = command.Description,
        };

        context.ApplicationEntities.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
