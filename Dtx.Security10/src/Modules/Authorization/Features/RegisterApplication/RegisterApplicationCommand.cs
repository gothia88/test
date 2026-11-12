using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Authorization.Features.RegisterApplication;

public sealed record RegisterApplicationCommand(
    string Name,
    string Title,
    string? Description = null) : ICommand<Guid>;
