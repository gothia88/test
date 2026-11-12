using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Authorization.Domain;

namespace Dtx.Security.Authorization.Features.DefineApplicationEntity;

public sealed record DefineApplicationEntityCommand(
    Guid ApplicationId,
    string Name,
    string Path,
    ApplicationEntityType Type,
    AccessType AccessType,
    RequestType RequestType,
    string? Title = null,
    string? Description = null) : ICommand<Guid>;
