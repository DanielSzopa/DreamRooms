using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.Publishers;
internal interface IDomainEventsPublisher
{
     IEnumerable<Task> PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
        where TEvent : class, IDomainEvent;
}
