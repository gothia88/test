using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public static class AuthorizationErrors
{
    public static Error ApplicationNotFound(Guid id) =>
        Error.NotFound("Authorization.ApplicationNotFound", $"Application with Id = '{id}' was not found.");

    public static Error ApplicationNotFound(string name) =>
        Error.NotFound("Authorization.ApplicationNotFound", $"Application '{name}' was not found.");

    public static Error ApplicationNameAlreadyExists(string name) =>
        Error.Conflict("Authorization.ApplicationNameAlreadyExists", $"Application name '{name}' is already taken.");

    public static Error ApplicationGroupNotFound(Guid id) =>
        Error.NotFound("Authorization.ApplicationGroupNotFound", $"Application group with Id = '{id}' was not found.");

    public static Error ApplicationEntityNotFound(Guid id) =>
        Error.NotFound("Authorization.ApplicationEntityNotFound", $"Application entity with Id = '{id}' was not found.");

    public static Error ApplicationEntityNotFound(string applicationName, string path) =>
        Error.NotFound("Authorization.ApplicationEntityNotFound", $"Entity '{path}' was not found in application '{applicationName}'.");

    public static Error PermissionAlreadyExists() =>
        Error.Conflict("Authorization.PermissionAlreadyExists", "The permission already exists.");

    public static Error MembershipAlreadyExists() =>
        Error.Conflict("Authorization.MembershipAlreadyExists", "The user is already a member of this group.");
}
