using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.Publisher;
public interface IDomainEventsPublisher
{
     List<Task> PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IDomainEvent;
}
