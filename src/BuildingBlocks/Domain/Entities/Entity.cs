using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Domain.Entities;

public abstract class Entity
{
    private List<IDomainEvent> _events = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _events;

    protected void AddDomainEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}
