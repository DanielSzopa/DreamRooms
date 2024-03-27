
using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Events.NotificationsRegistery;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BuildingBlocks.Events.Publishers;
internal class DomainEventNotificationPublisher : IDomainEventNotificationsPublisher
{
    private readonly DomainEventNotificationsRegistery _domainEventNotificationsRegistery;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventNotificationPublisher(DomainEventNotificationsRegistery domainEventNotificationsRegistery, IServiceProvider serviceProvider)
    {
        _domainEventNotificationsRegistery = domainEventNotificationsRegistery;
        _serviceProvider = serviceProvider;
    }

    public IEnumerable<Task> PublishAsync(string notificationType, string notifiticationData, CancellationToken cancellationToken = default)
    {
        var type = _domainEventNotificationsRegistery.ResolveDomainEventNotificationFromStringType(notificationType);
        var notification = (IDomainEventNotification<IDomainEvent>)JsonConvert.DeserializeObject(notifiticationData, type);
        var handlerType = typeof(IDomainEventNotificationHandler<>).MakeGenericType(type);
        var notificationHandlers = _serviceProvider.GetServices(handlerType);

        var tasksResults = new List<Task>();

        if (notificationHandlers.Count() > 0)
        {
            var tasks = notificationHandlers
                .Select(x => (Task)handlerType
                .GetMethod(nameof(IDomainEventNotificationHandler<IDomainEventNotification<IDomainEvent>>.HandleAsync))
                ?.Invoke(x, new object[] { notification, cancellationToken }));

            tasksResults.AddRange(tasks);
        }

        return tasksResults;
    }
}
