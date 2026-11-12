using Dtx.Security.Application.Abstractions.Behaviors;
using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.Application.Abstractions.Messaging;
using Dtx.Security.Application.DomainEvents;
using Dtx.Security.SharedKernel;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Dtx.Security.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the command/query handlers, domain-event handlers, validation
    /// decorators and logging decorators for the module containing
    /// <typeparamref name="TMarker"/>.
    /// </summary>
    public static IServiceCollection AddModuleHandlers<TMarker>(this IServiceCollection services)
        where TMarker : class
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(TMarker))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.Scan(scan => scan.FromAssembliesOf(typeof(TMarker))
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

        services.AddValidatorsFromAssemblyContaining<TMarker>(includeInternalTypes: true);

        return services;
    }
}
