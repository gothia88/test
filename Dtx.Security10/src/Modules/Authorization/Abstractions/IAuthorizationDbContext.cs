using Dtx.Security.Authorization.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Abstractions;

public interface IAuthorizationDbContext
{
    DbSet<ClientApplication> Applications { get; }

    DbSet<ApplicationGroup> ApplicationGroups { get; }

    DbSet<ApplicationEntity> ApplicationEntities { get; }

    DbSet<Permission> Permissions { get; }

    DbSet<Membership> Memberships { get; }

    DbSet<ApplicationValidIP> ValidIPs { get; }

    DbSet<ApplicationValidUrl> ValidUrls { get; }

    DbSet<CheckPermissionLog> CheckPermissionLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
