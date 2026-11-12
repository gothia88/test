using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class Membership : Entity
{
    public Guid Id { get; set; }

    /// <summary>
    /// References a user in the Identity module by id only (no navigation
    /// property), keeping the two modules decoupled.
    /// </summary>
    public Guid UserId { get; set; }

    public Guid ApplicationGroupId { get; set; }

    public ApplicationGroup? ApplicationGroup { get; set; }
}
