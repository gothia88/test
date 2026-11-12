using Dtx.Security.SharedKernel;

namespace Dtx.Security.Authorization.Domain;

public sealed class CheckPermissionLog : Entity
{
    public Guid Id { get; set; }

    /// <summary>References a user in the Identity module by id only.</summary>
    public Guid UserId { get; set; }

    public Guid ApplicationEntityId { get; set; }

    public ApplicationEntity? ApplicationEntity { get; set; }

    public bool IsGranted { get; set; }

    public DateTime CheckDateTime { get; set; }
}
