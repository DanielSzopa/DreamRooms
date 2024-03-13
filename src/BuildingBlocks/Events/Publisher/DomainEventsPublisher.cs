using BuildingBlocks.Domain.Events.Abstractions;
using BuildingBlocks.Events.DomainEventsHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.Publisher;
public class DomainEventsPublisher : IDomainEventsPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public List<Task> PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : class, IDomainEvent
    {
        var tasks = new List<Task>();

        var handlers = _serviceProvider.GetServices(typeof(IDomainEventHandler<TEvent>));

        foreach (var handler in handlers)
        {
            var domainEventHandler = (IDomainEventHandler<TEvent>)handler;
            tasks.Add(domainEventHandler.HandleAsync(@event, cancellationToken));
        }

        return tasks;
    }
}
