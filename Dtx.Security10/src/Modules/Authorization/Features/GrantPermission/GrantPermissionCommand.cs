using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Authorization.Features.GrantPermission;

public sealed record GrantPermissionCommand(
    Guid ApplicationGroupId,
    Guid ApplicationEntityId) : ICommand<Guid>;
