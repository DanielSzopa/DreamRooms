using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Events.NotificationsRegistery;
using BuildingBlocks.Events.Providers;
using BuildingBlocks.Events.Publishers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.Dispatcher;
public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsPublisher _domainEventsPublisher;
    private readonly IServiceProvider _serviceProvider;
    private readonly DomainEventNotificationsRegistery _domainEventNotificationsRegistery;

    public DomainEventsDispatcher(IDomainEventsPublisher domainEventsPublisher,
        IServiceProvider serviceProvider, DomainEventNotificationsRegistery domainEventNotificationsRegistery)
    {
        _domainEventsPublisher = domainEventsPublisher;
        _serviceProvider = serviceProvider;
        _domainEventNotificationsRegistery = domainEventNotificationsRegistery;
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
            var domainEventType = domainEvent.GetType();
            var domainEventNotificationType = _domainEventNotificationsRegistery.Resolve(domainEventType);

            var notificationHandlerType = typeof(IDomainEventNotificationHandler<>).MakeGenericType(domainEventNotificationType);
            var notificationHandlers = _serviceProvider.GetServices(notificationHandlerType);

            var publishedTasks = _domainEventsPublisher.PublishAsync(domainEvent, cancellationToken);
            resultTasks.AddRange(publishedTasks);
        }

        domainEventsProvider.ClearAllDomainEvents();

        await Task.WhenAll(resultTasks);
    }
}
