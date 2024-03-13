namespace BuildingBlocks.Domain.Events.Abstractions;
public interface IDomainEvent
{
    Guid EventId { get; }
}
