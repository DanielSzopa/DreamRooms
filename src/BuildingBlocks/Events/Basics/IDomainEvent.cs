namespace BuildingBlocks.Events.Basics;
public interface IDomainEvent
{
    Guid EventId { get; }
}
