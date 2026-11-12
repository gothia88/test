using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Orders.Features.CreateOrder;

public sealed record CreateOrderCommand(
    string CustomerName,
    string? CustomerEmail,
    List<CreateOrderItem> Items) : ICommand<Guid>;

public sealed record CreateOrderItem(
    string ProductName,
    int Quantity,
    decimal UnitPrice);
