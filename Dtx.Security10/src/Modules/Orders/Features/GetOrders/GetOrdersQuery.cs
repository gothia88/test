using Dtx.Orders.Features.GetOrderById;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Application.Abstractions.Pagination;

namespace Dtx.Orders.Features.GetOrders;

public sealed record GetOrdersQuery(int Page = 1, int PageSize = 10) : IQuery<PagedList<OrderResponse>>;
