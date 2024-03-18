using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Events.Basics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuildingBlocks.Events.Providers;
public class DomainEventsProvider : IDomainEventsProvider
{
    private readonly DbContext _dbContext;

    public DomainEventsProvider(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ClearAllDomainEvents()
    {
        ListOfEntries()
            .ForEach(e => e.Entity.ClearEvents());
    }

    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
    {
        return ListOfEntries()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
    }

    private List<EntityEntry<Entity>> ListOfEntries()
    {
        return _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any())
            .ToList();
    }
}
