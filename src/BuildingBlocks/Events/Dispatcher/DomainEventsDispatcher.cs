using BuildingBlocks.Context;
using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.NotificationsCreator;
using BuildingBlocks.Events.NotificationsRegistery;
using BuildingBlocks.Events.Providers;
using BuildingBlocks.Events.Publishers;
using BuildingBlocks.Messaging.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BuildingBlocks.Events.Dispatcher;
internal class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsPublisher _domainEventsPublisher;
    private readonly IDomainEventNotificationsCreator _domainEventNotificationsCreator;
    private readonly IServiceProvider _serviceProvider;
    private readonly DomainEventNotificationsRegistery _domainEventNotificationsRegistery;
    private readonly IContextAccessor _contextAccessor;

    public DomainEventsDispatcher(IDomainEventsPublisher domainEventsPublisher, IDomainEventNotificationsCreator domainEventNotificationsCreator,
        IServiceProvider serviceProvider, DomainEventNotificationsRegistery domainEventNotificationsRegistery, IContextAccessor contextAccessor)
    {
        _domainEventsPublisher = domainEventsPublisher;
        _domainEventNotificationsCreator = domainEventNotificationsCreator;
        _serviceProvider = serviceProvider;
        _domainEventNotificationsRegistery = domainEventNotificationsRegistery;
        _contextAccessor = contextAccessor;
    }

    public async Task DispatchAsync(Type dbContextType, CancellationToken cancellationToken = default)
    {
        var dbContext = (DbContext)_serviceProvider.GetRequiredService(dbContextType);
        var domainEventsProvider = new DomainEventsProvider(dbContext);

        var domainEvents = domainEventsProvider.GetAllDomainEvents();
        if (domainEvents is null || !domainEvents.Any())
            return;

        List<Task> domainEventHandlingTasks = new List<Task>();
        List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = new ();

        foreach (var domainEvent in domainEvents)
        {
            var domainEventType = domainEvent.GetType();
            var domainEventNotificationType = _domainEventNotificationsRegistery.Resolve(domainEventType);
            if(domainEventNotificationType is not null)
            {
                domainEventNotifications.Add(_domainEventNotificationsCreator.Create(domainEventNotificationType, domainEvent));
            }

            var handlingTasks = _domainEventsPublisher.PublishAsync(domainEvent, cancellationToken);
            domainEventHandlingTasks.AddRange(handlingTasks);
        }

        domainEventsProvider.ClearAllDomainEvents();

        var outBoxDbSet = dbContext.Set<OutboxMessage>();

        var outBoxMessages = domainEventNotifications.Select(d => new OutboxMessage()
        {
            Id = d.EventId,
            CorrelationId = _contextAccessor.CorrelationId,
            TraceId = _contextAccessor.TraceId,
            Name = nameof(IDomainEventNotification<IDomainEvent>),
            Type = d.GetType().ToString(),
            CreatedAt = DateTime.Now,
            Data = JsonSerializer.Serialize(d)
        });

        outBoxDbSet.AddRange(outBoxMessages);

        await Task.WhenAll(domainEventHandlingTasks);
    }
}
