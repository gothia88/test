using Dtx.Orders.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Orders.Abstractions;

public interface IOrdersDbContext
{
    DbSet<Order> Orders { get; }

    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
