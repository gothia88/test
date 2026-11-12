using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class ApplicationValidIP : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public ClientApplication? Application { get; set; }

    public string IP { get; set; } = string.Empty;
}
