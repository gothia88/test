using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Authorization.Features.CreateApplicationGroup;

public sealed record CreateApplicationGroupCommand(
    Guid ApplicationId,
    string Name,
    string? Description = null) : ICommand<Guid>;
