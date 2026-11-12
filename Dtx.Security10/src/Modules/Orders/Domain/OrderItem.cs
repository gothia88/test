using Dtx.Security.SharedKernel;

namespace Dtx.Orders.Domain;

public sealed class OrderItem : Entity
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Order? Order { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
