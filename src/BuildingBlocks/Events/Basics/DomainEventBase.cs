namespace BuildingBlocks.Events.Basics;
public record DomainEventBase : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
}
