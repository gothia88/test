using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class Permission : Entity
{
    public Guid Id { get; set; }

    public Guid ApplicationGroupId { get; set; }

    public ApplicationGroup? ApplicationGroup { get; set; }

    public Guid ApplicationEntityId { get; set; }

    public ApplicationEntity? ApplicationEntity { get; set; }
}
