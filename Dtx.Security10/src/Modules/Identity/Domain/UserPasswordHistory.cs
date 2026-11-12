using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public sealed class UserPasswordHistory : Entity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; }
}
