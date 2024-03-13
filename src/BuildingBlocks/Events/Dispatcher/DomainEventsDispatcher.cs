using BuildingBlocks.Events.DomainEventsProvider;
using BuildingBlocks.Events.DomainEventsPublisher;

namespace BuildingBlocks.Events.Dispatcher;
public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsProvider _domainEventsProvider;
    private readonly IDomainEventsPublisher _domainEventsPublisher;

    public DomainEventsDispatcher(IDomainEventsProvider domainEventsProvider, IDomainEventsPublisher domainEventsPublisher)
    {
        _domainEventsProvider = domainEventsProvider;
        _domainEventsPublisher = domainEventsPublisher;
    }

    public async Task DispatchAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = _domainEventsProvider.GetAllDomainEvents();
        if (domainEvents is null || !domainEvents.Any())
            return;

        List<Task> resultTasks = new List<Task>();

        foreach (var domainEvent in domainEvents)
        {
            var publishedTasks = _domainEventsPublisher.PublishAsync(domainEvent, cancellationToken);
            resultTasks.AddRange(publishedTasks);
        }

        _domainEventsProvider.ClearAllDomainEvents();

        await Task.WhenAll(resultTasks);
    }
}
