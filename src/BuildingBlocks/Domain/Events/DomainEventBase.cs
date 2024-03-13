using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Domain.Events;
public record DomainEventBase : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
}
