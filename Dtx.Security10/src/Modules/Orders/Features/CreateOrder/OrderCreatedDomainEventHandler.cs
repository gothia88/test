using Dtx.Orders.Domain;
using Dtx.Security.SharedKernel;
using Microsoft.Extensions.Logging;

namespace Dtx.Orders.Features.CreateOrder;

/// <summary>
/// Demonstrates the domain-event pipeline: raised by the aggregate and dispatched
/// after <see cref="DomainEventsDbContext.SaveChangesAsync"/> completes.
/// </summary>
internal sealed class OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
    : IDomainEventHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Order {OrderId} was created.", domainEvent.OrderId);

        return Task.CompletedTask;
    }
}
