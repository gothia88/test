using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Security.Identity.Features.Register;

public sealed record RegisterUserCommand(
    string Username,
    string Password,
    string FullName,
    string? EmailAddress = null,
    string? CellPhoneNumber = null,
    string? NationalCode = null) : ICommand<Guid>;
