using Dtx.Security.Application.Abstractions.DomainEvents;
using Dtx.Security.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Dtx.Security.Application.Persistence;

/// <summary>
/// Base <see cref="DbContext"/> that extracts and dispatches domain events after
/// <see cref="SaveChangesAsync"/> succeeds. Module DbContexts derive from it.
/// </summary>
public abstract class DomainEventsDbContext : DbContext
{
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    protected DomainEventsDbContext(
        DbContextOptions options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : base(options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<IDomainEvent> domainEvents = ExtractDomainEvents();

        int result = await base.SaveChangesAsync(cancellationToken);

        await _domainEventsDispatcher.DispatchAsync(domainEvents, cancellationToken);

        return result;
    }

    private List<IDomainEvent> ExtractDomainEvents()
    {
        return ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();
    }
}
