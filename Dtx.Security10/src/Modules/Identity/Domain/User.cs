using Dtx.Security.SharedKernel;

namespace Dtx.Security.Identity.Domain;

public sealed class User : Entity
{
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? EmailAddress { get; set; }

    public string? CellPhoneNumber { get; set; }

    public string? NationalCode { get; set; }

    public string? RegisterIP { get; set; }

    public UserType Type { get; set; }

    public bool IsActive { get; set; }

    public bool IsIPRestricted { get; set; }

    public bool IsExpirable { get; set; }

    public DateTime? ExpireDateTime { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public List<UserPasswordHistory> PasswordHistories { get; set; } = [];

    public List<UserValidIP> ValidIPs { get; set; } = [];

    public List<LoginLog> LoginLogs { get; set; } = [];
}
