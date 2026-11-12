using Dtx.Orders.Abstractions;
using Dtx.Orders.Domain;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.SharedKernel;

namespace Dtx.Orders.Features.CreateOrder;

internal sealed class CreateOrderCommandHandler(
    IOrdersDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        if (command.Items.Count == 0)
        {
            return Result.Failure<Guid>(OrderErrors.EmptyItems());
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNumber = GenerateOrderNumber(),
            CustomerName = command.CustomerName,
            CustomerEmail = command.CustomerEmail,
            Status = OrderStatus.Pending,
            TotalAmount = command.Items.Sum(item => item.Quantity * item.UnitPrice),
            CreatedOnUtc = dateTimeProvider.UtcNow,
            Items = command.Items.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
            }).ToList(),
        };

        foreach (OrderItem orderItem in order.Items)
        {
            orderItem.OrderId = order.Id;
        }

        order.Raise(new OrderCreatedDomainEvent(order.Id));

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    private static string GenerateOrderNumber() =>
        $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpperInvariant()}";
}
