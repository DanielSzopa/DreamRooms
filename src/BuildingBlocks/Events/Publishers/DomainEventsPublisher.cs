using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventsHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.Publishers;
internal class DomainEventsPublisher : IDomainEventsPublisher
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

        if(handlers.Count() == 0)
            return Enumerable.Empty<Task>();

        var tasks = handlers.Select(x => (Task) handlerType
        .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
        ?.Invoke(x, new object[] { @event, cancellationToken }));

        return tasks;
    }
}
