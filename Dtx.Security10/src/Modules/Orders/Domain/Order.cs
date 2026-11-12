using Dtx.Security.SharedKernel;

namespace Dtx.Orders.Domain;

public sealed class Order : Entity
{
    public Guid Id { get; set; }

    public string OrderNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string? CustomerEmail { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public List<OrderItem> Items { get; set; } = [];
}
