using BuildingBlocks.Events.Provider;
using BuildingBlocks.Events.Publisher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.Dispatcher;
public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsPublisher _domainEventsPublisher;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsDispatcher(IDomainEventsPublisher domainEventsPublisher,
        IServiceProvider serviceProvider)
    {
        _domainEventsPublisher = domainEventsPublisher;
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(Type dbContextType, CancellationToken cancellationToken = default)
    {
        var dbContext = (DbContext)_serviceProvider.GetRequiredService(dbContextType);
        var domainEventsProvider = new DomainEventsProvider(dbContext);

        var domainEvents = domainEventsProvider.GetAllDomainEvents();
        if (domainEvents is null || !domainEvents.Any())
            return;

        List<Task> resultTasks = new List<Task>();

        foreach (var domainEvent in domainEvents)
        {
            var publishedTasks = _domainEventsPublisher.PublishAsync(domainEvent, cancellationToken);
            resultTasks.AddRange(publishedTasks);
        }

        domainEventsProvider.ClearAllDomainEvents();

        await Task.WhenAll(resultTasks);
    }
}
