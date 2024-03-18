namespace BuildingBlocks.Events.Basics;
public interface IDomainEventNotification<out TEvent>
    where TEvent : class, IDomainEvent
{
    TEvent DomainEvent { get; }
    Guid EventId { get; }
}

