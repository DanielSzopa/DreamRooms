namespace BuildingBlocks.Events.Basics;
public abstract record DomainEventNotificationBase<TEvent> : IDomainEventNotification<TEvent>
    where TEvent : class, IDomainEvent
{
    public TEvent DomainEvent { get; }

    public Guid EventId { get; }

    public DomainEventNotificationBase(TEvent @event, Guid eventId)
    {
        DomainEvent = @event;
        EventId = eventId;
    }
}
