using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.Application.Persistence;
using Dtx.Security.Authorization.Abstractions;
using Dtx.Security.Authorization.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Authorization.Persistence;

public sealed class AuthorizationDbContext : DomainEventsDbContext, IAuthorizationDbContext
{
    public AuthorizationDbContext(
        DbContextOptions<AuthorizationDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : base(options, domainEventsDispatcher)
    {
    }

    public DbSet<ClientApplication> Applications => Set<ClientApplication>();

    public DbSet<ApplicationGroup> ApplicationGroups => Set<ApplicationGroup>();

    public DbSet<ApplicationEntity> ApplicationEntities => Set<ApplicationEntity>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<Membership> Memberships => Set<Membership>();

    public DbSet<ApplicationValidIP> ValidIPs => Set<ApplicationValidIP>();

    public DbSet<ApplicationValidUrl> ValidUrls => Set<ApplicationValidUrl>();

    public DbSet<CheckPermissionLog> CheckPermissionLogs => Set<CheckPermissionLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("authorization");

        modelBuilder.Entity<ClientApplication>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Name).HasMaxLength(50).IsRequired();
            entity.Property(a => a.Title).HasMaxLength(100).IsRequired();
            entity.Property(a => a.Description).HasMaxLength(1000);
            entity.HasIndex(a => a.Name).IsUnique();

            entity.HasMany(a => a.Groups)
                .WithOne(g => g.Application)
                .HasForeignKey(g => g.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Entities)
                .WithOne(e => e.Application)
                .HasForeignKey(e => e.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.ValidIPs)
                .WithOne(v => v.Application)
                .HasForeignKey(v => v.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.ValidUrls)
                .WithOne(v => v.Application)
                .HasForeignKey(v => v.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApplicationGroup>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).HasMaxLength(100).IsRequired();
            entity.Property(g => g.Description).HasMaxLength(1000);
            entity.HasIndex(g => new { g.ApplicationId, g.Name }).IsUnique();

            entity.HasMany(g => g.Permissions)
                .WithOne(p => p.ApplicationGroup)
                .HasForeignKey(p => p.ApplicationGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(g => g.Memberships)
                .WithOne(m => m.ApplicationGroup)
                .HasForeignKey(m => m.ApplicationGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ApplicationEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Path).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.HasIndex(e => new { e.ApplicationId, e.Path }).IsUnique();

            entity.HasMany(e => e.Permissions)
                .WithOne(p => p.ApplicationEntity)
                .HasForeignKey(p => p.ApplicationEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.CheckPermissionLogs)
                .WithOne(l => l.ApplicationEntity)
                .HasForeignKey(l => l.ApplicationEntityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => new { p.ApplicationGroupId, p.ApplicationEntityId }).IsUnique();
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.HasIndex(m => new { m.UserId, m.ApplicationGroupId }).IsUnique();
        });

        modelBuilder.Entity<ApplicationValidIP>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.IP).HasMaxLength(45).IsRequired();
            entity.HasIndex(v => new { v.ApplicationId, v.IP }).IsUnique();
        });

        modelBuilder.Entity<ApplicationValidUrl>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Url).HasMaxLength(500).IsRequired();
            entity.HasIndex(v => new { v.ApplicationId, v.Url }).IsUnique();
        });

        modelBuilder.Entity<CheckPermissionLog>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.HasIndex(l => l.UserId);
            entity.HasIndex(l => l.ApplicationEntityId);
        });
    }
}
