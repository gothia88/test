using Dtx.Orders.Domain;

namespace Dtx.Orders.Features.GetOrderById;

public sealed record OrderResponse(
    Guid Id,
    string OrderNumber,
    string CustomerName,
    string? CustomerEmail,
    OrderStatus Status,
    decimal TotalAmount,
    DateTime CreatedOnUtc,
    IReadOnlyList<OrderItemResponse> Items);

public sealed record OrderItemResponse(
    string ProductName,
    int Quantity,
    decimal UnitPrice);
