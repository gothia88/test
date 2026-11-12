using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public sealed class RefreshToken : Entity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string Token { get; set; } = string.Empty;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime ExpiresOnUtc { get; set; }

    public DateTime? RevokedOnUtc { get; set; }

    public DateTime? UsedOnUtc { get; set; }

    public bool IsRevoked => RevokedOnUtc is not null;
}
