using Dtx.Security.Identity.Domain;

namespace Dtx.Security.Identity.Features.GetUserById;

public sealed record UserResponse(
    Guid Id,
    string Username,
    string FullName,
    string? EmailAddress,
    UserType Type,
    bool IsActive);
