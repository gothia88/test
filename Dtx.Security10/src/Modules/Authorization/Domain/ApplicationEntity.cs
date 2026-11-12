using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class ApplicationEntity : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ClientApplication? Application { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Path { get; set; } = "/";

    public ApplicationEntityType Type { get; set; }

    public AccessType AccessType { get; set; }

    public RequestType RequestType { get; set; }

    public string? Description { get; set; }

    public List<Permission> Permissions { get; set; } = [];

    public List<CheckPermissionLog> CheckPermissionLogs { get; set; } = [];
}
