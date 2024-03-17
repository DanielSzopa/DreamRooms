using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.Providers;
public interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}
