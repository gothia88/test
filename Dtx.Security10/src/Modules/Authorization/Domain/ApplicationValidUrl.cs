using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class ApplicationValidUrl : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ClientApplication? Application { get; set; }

    public string Url { get; set; } = string.Empty;
}
