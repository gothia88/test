using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Authorization.Features.AddMembership;

public sealed record AddMembershipCommand(
    Guid UserId,
    Guid ApplicationGroupId) : ICommand<Guid>;
