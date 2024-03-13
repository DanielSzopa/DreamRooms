using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.Provider;
public interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}
