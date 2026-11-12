using System.Collections.Concurrent;
using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.SharedKernel;
using Microsoft.Extensions.DependencyInjection;

namespace Dtx.Security.Application.DomainEvents;

internal sealed class DomainEventsDispatcher(IServiceProvider serviceProvider) : IDomainEventsDispatcher
{
    private static readonly ConcurrentDictionary<Type, Type> HandlerTypeDictionary = new();
    private static readonly ConcurrentDictionary<Type, Type> WrapperTypeDictionary = new();

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            Type domainEventType = domainEvent.GetType();
            Type handlerType = HandlerTypeDictionary.GetOrAdd(
                domainEventType,
                et => typeof(IDomainEventHandler<>).MakeGenericType(et));

            IEnumerable<object?> handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (object? handler in handlers)
            {
                if (handler is null)
                {
                    continue;
                }

                HandlerWrapper handlerWrapper = HandlerWrapper.Create(handler, domainEventType);
                await handlerWrapper.Handle(domainEvent, cancellationToken);
            }
        }
    }

    private abstract class HandlerWrapper
    {
        public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);

        public static HandlerWrapper Create(object handler, Type domainEventType)
        {
            Type wrapperType = WrapperTypeDictionary.GetOrAdd(
                domainEventType,
                et => typeof(HandlerWrapper<>).MakeGenericType(et));

            return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
        }
    }

    private sealed class HandlerWrapper<TDomainEvent>(object handler) : HandlerWrapper
        where TDomainEvent : IDomainEvent
    {
        private readonly IDomainEventHandler<TDomainEvent> _handler =
            (IDomainEventHandler<TDomainEvent>)handler;

        public override Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken) =>
            _handler.Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
