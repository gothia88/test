using Dtx.Orders.Abstractions;
using Dtx.Orders.Domain;
using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Orders.Persistence;

public sealed class OrdersDbContext : DomainEventsDbContext, IOrdersDbContext
{
    public OrdersDbContext(
        DbContextOptions<OrdersDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : base(options, domainEventsDispatcher)
    {
    }

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.OrderNumber).HasMaxLength(50).IsRequired();
            entity.Property(o => o.CustomerName).HasMaxLength(100).IsRequired();
            entity.Property(o => o.CustomerEmail).HasMaxLength(250);
            entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
            entity.HasIndex(o => o.OrderNumber).IsUnique();

            entity.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.ProductName).HasMaxLength(200).IsRequired();
            entity.Property(i => i.UnitPrice).HasPrecision(18, 2);
        });
    }
}
