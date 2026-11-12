using Dtx.Security.Application.Abstractions.Messaging;

namespace Dtx.Orders.Features.GetOrderById;

public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderResponse>;
