using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.Application.Persistence;
using Dtx.Security.Identity.Abstractions;
using Dtx.Security.Identity.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Identity.Persistence;

public sealed class IdentityDbContext : DomainEventsDbContext, IIdentityDbContext
{
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : base(options, domainEventsDispatcher)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<LoginLog> LoginLogs => Set<LoginLog>();

    public DbSet<UserPasswordHistory> PasswordHistories => Set<UserPasswordHistory>();

    public DbSet<UserValidIP> ValidIPs => Set<UserValidIP>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).HasMaxLength(50).IsRequired();
            entity.Property(u => u.PasswordHash).HasMaxLength(200).IsRequired();
            entity.Property(u => u.FullName).HasMaxLength(100).IsRequired();
            entity.Property(u => u.EmailAddress).HasMaxLength(250);
            entity.Property(u => u.CellPhoneNumber).HasMaxLength(20);
            entity.Property(u => u.NationalCode).HasMaxLength(20);
            entity.Property(u => u.RegisterIP).HasMaxLength(45);

            entity.HasIndex(u => u.Username).IsUnique();

            entity.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.PasswordHistories)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.ValidIPs)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.LoginLogs)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(rt => rt.Id);
            entity.Property(rt => rt.Token).HasMaxLength(200).IsRequired();
            entity.HasIndex(rt => rt.Token).IsUnique();
        });

        modelBuilder.Entity<LoginLog>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.UserIP).HasMaxLength(45);
        });

        modelBuilder.Entity<UserPasswordHistory>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.PasswordHash).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<UserValidIP>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.IP).HasMaxLength(45).IsRequired();
        });
    }
}
