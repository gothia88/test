using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        Error.NotFound("Identity.UserNotFound", $"User with Id = '{userId}' was not found.");

    public static Error NotFoundByUsername(string username) =>
        Error.NotFound("Identity.UserNotFound", $"User with username = '{username}' was not found.");

    public static Error UsernameAlreadyExists(string username) =>
        Error.Conflict("Identity.UsernameAlreadyExists", $"Username '{username}' is already taken.");

    public static Error InvalidCredentials() =>
        Error.Problem("Identity.InvalidCredentials", "Invalid username or password.");

    public static Error InactiveAccount() =>
        Error.Problem("Identity.InactiveAccount", "The account is inactive.");

    public static Error InvalidRefreshToken() =>
        Error.Problem("Identity.InvalidRefreshToken", "The refresh token is invalid or expired.");

    public static Error IncorrectCurrentPassword() =>
        Error.Problem("Identity.IncorrectCurrentPassword", "The current password is incorrect.");

    public static Error PasswordReused() =>
        Error.Conflict("Identity.PasswordReused", "The new password must differ from previous passwords.");
}
