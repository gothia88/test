using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class ApplicationGroup : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ClientApplication? Application { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<Permission> Permissions { get; set; } = [];

    public List<Membership> Memberships { get; set; } = [];
}
