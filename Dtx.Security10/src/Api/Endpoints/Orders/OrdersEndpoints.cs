using Dtx.Orders.Features.CreateOrder;
using Dtx.Orders.Features.GetOrderById;
using Dtx.Orders.Features.GetOrders;
using Dtx.Security.Api.Infrastructure;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Application.Abstractions.Pagination;
using Dtx.Security.SharedKernel;

namespace Dtx.Security.Api.Endpoints.Orders;

public static class OrdersEndpoints
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("api/orders");

        group.MapPost("/", async (
            CreateOrderCommand command,
            ICommandHandler<CreateOrderCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(id => Results.Ok(id), CustomResults.Problem);
        }).WithTags("Orders");

        group.MapGet("{orderId:guid}", async (
            Guid orderId,
            IQueryHandler<GetOrderByIdQuery, OrderResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<OrderResponse> result = await handler.Handle(new GetOrderByIdQuery(orderId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Orders");

        group.MapGet("/", async (
            int? page,
            int? pageSize,
            IQueryHandler<GetOrdersQuery, PagedList<OrderResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetOrdersQuery(page ?? 1, pageSize ?? 10);

            Result<PagedList<OrderResponse>> result = await handler.Handle(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Orders");

        return app;
    }
}
