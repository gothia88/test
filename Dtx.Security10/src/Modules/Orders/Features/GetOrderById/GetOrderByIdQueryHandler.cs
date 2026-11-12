using Dtx.Orders.Abstractions;
using Dtx.Orders.Domain;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Orders.Features.GetOrderById;

internal sealed class GetOrderByIdQueryHandler(IOrdersDbContext context)
    : IQueryHandler<GetOrderByIdQuery, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        OrderResponse? order = await context.Orders
            .Where(o => o.Id == query.OrderId)
            .Select(o => new OrderResponse(
                o.Id,
                o.OrderNumber,
                o.CustomerName,
                o.CustomerEmail,
                o.Status,
                o.TotalAmount,
                o.CreatedOnUtc,
                o.Items
                    .Select(i => new OrderItemResponse(i.ProductName, i.Quantity, i.UnitPrice))
                    .ToList()))
            .SingleOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound(query.OrderId));
        }

        return order;
    }
}
