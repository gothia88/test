using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Authorization.Features.CheckPermission;

public sealed record CheckPermissionCommand(
    string ApplicationName,
    string EntityPath) : ICommand<CheckPermissionResponse>;
