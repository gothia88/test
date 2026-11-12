using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

/// <summary>
/// A client application registered with the security service.
/// </summary>
public sealed class ClientApplication : Entity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsIPRestricted { get; set; }

    public bool IsUrlRestricted { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public List<ApplicationGroup> Groups { get; set; } = [];

    public List<ApplicationEntity> Entities { get; set; } = [];

    public List<ApplicationValidIP> ValidIPs { get; set; } = [];

    public List<ApplicationValidUrl> ValidUrls { get; set; } = [];
}
