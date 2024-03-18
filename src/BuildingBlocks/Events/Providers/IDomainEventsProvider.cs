using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.Providers;
public interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}
