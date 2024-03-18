using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.DomainEventsHandlers;
public interface IDomainEventHandler<TEvent>
    where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
