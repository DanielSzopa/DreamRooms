using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.Providers;
internal interface IDomainEventsProvider
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}
