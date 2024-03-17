using BuildingBlocks.Domain.Events.Abstractions;
using BuildingBlocks.Events.DomainEventsHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.Publishers;
public class DomainEventsPublisher : IDomainEventsPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<Task> PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : class, IDomainEvent
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);

        var tasks = handlers.Select(x => (Task) handlerType
        .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
        ?.Invoke(x, new object[] { @event, cancellationToken }));

        return tasks;
    }
}
