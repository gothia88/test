using Dtx.Security.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Identity.Abstractions;

public interface IIdentityDbContext
{
    DbSet<User> Users { get; }

    DbSet<RefreshToken> RefreshTokens { get; }

    DbSet<LoginLog> LoginLogs { get; }

    DbSet<UserPasswordHistory> PasswordHistories { get; }

    DbSet<UserValidIP> ValidIPs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
