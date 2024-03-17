namespace BuildingBlocks.Domain.Events.Abstractions;
public interface IDomainEventNotification<out TEvent>
    where TEvent : class, IDomainEvent
{
    TEvent DomainEvent { get; }
    Guid EventId { get; }
}

