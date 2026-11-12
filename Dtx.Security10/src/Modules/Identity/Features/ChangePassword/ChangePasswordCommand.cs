using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Identity.Features.ChangePassword;

public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword) : ICommand;
