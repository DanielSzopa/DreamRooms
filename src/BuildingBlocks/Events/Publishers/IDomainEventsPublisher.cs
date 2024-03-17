using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.Publishers;
public interface IDomainEventsPublisher
{
     IEnumerable<Task> PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IDomainEvent;
}
