using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.DomainEventsProvider;
public interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}
