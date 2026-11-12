using Dtx.Orders.Abstractions;
using Dtx.Orders.Features.GetOrderById;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Application.Abstractions.Pagination;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Orders.Features.GetOrders;

internal sealed class GetOrdersQueryHandler(IOrdersDbContext context)
    : IQueryHandler<GetOrdersQuery, PagedList<OrderResponse>>
{
    public async Task<Result<PagedList<OrderResponse>>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        IQueryable<OrderResponse> orders = context.Orders
            .OrderByDescending(o => o.CreatedOnUtc)
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
                    .ToList()));

        PagedList<OrderResponse> paged = await PagedList<OrderResponse>.CreateAsync(
            orders, query.Page, query.PageSize, cancellationToken);

        return paged;
    }
}
