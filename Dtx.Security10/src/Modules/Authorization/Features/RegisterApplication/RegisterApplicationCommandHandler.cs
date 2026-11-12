using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Features.RegisterApplication;

internal sealed class RegisterApplicationCommandHandler(
    IAuthorizationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<RegisterApplicationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterApplicationCommand command, CancellationToken cancellationToken)
    {
        bool exists = await context.Applications
            .AnyAsync(a => a.Name == command.Name, cancellationToken);

        if (exists)
        {
            return Result.Failure<Guid>(AuthorizationErrors.ApplicationNameAlreadyExists(command.Name));
        }

        var application = new ClientApplication
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Title = command.Title,
            Description = command.Description,
            CreatedOnUtc = dateTimeProvider.UtcNow,
        };

        context.Applications.Add(application);

        await context.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
