using Dtx.Security.SharedKernel;

namespace Dtx.Orders.Domain;

public sealed record OrderCreatedDomainEvent(Guid OrderId) : IDomainEvent;
