using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public sealed class UserValidIP : Entity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string IP { get; set; } = string.Empty;
}
