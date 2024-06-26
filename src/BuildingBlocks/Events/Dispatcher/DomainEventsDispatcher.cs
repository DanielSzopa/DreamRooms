﻿using BuildingBlocks.Context;
using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.NotificationsCreator;
using BuildingBlocks.Events.NotificationsRegistery;
using BuildingBlocks.Events.Providers;
using BuildingBlocks.Events.Publishers;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Messaging.Outbox;
using BuildingBlocks.Messaging.Outbox.Repositories;
using BuildingBlocks.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BuildingBlocks.Events.Dispatcher;
internal class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsPublisher _domainEventsPublisher;
    private readonly IDomainEventNotificationsCreator _domainEventNotificationsCreator;
    private readonly IServiceProvider _serviceProvider;
    private readonly DomainEventNotificationsRegistery _domainEventNotificationsRegistery;
    private readonly IContextAccessor _contextAccessor;
    private readonly IClock _clock;
    private readonly IDomainEventNotificationOutBoxRepository _outBox;
    private readonly IMessageContextService _messageContextService;

    public DomainEventsDispatcher(IDomainEventsPublisher domainEventsPublisher, IDomainEventNotificationsCreator domainEventNotificationsCreator,
        IServiceProvider serviceProvider, DomainEventNotificationsRegistery domainEventNotificationsRegistery, IContextAccessor contextAccessor,
        IClock clock, IDomainEventNotificationOutBoxRepository outBox, IMessageContextService messageContextService)
    {
        _domainEventsPublisher = domainEventsPublisher;
        _domainEventNotificationsCreator = domainEventNotificationsCreator;
        _serviceProvider = serviceProvider;
        _domainEventNotificationsRegistery = domainEventNotificationsRegistery;
        _contextAccessor = contextAccessor;
        _clock = clock;
        _outBox = outBox;
        _messageContextService = messageContextService;
    }

    public async Task DispatchAsync(Type dbContextType, CancellationToken cancellationToken = default)
    {
        var dbContext = (DbContext)_serviceProvider.GetRequiredService(dbContextType);
        var domainEventsProvider = new DomainEventsProvider(dbContext);

        var domainEvents = domainEventsProvider.GetAllDomainEvents();
        if (domainEvents is null || !domainEvents.Any())
            return;

        List<Task> domainEventHandlingTasks = new List<Task>();
        List<IDomainEventNotification<IDomainEvent>> domainEventNotifications = new();

        foreach (var domainEvent in domainEvents)
        {
            var domainEventType = domainEvent.GetType();
            var domainEventNotificationType = _domainEventNotificationsRegistery.ResolveFromDomainEvent(domainEventType);
            if (domainEventNotificationType is not null)
            {
                var notification = _domainEventNotificationsCreator.Create(domainEventNotificationType, domainEvent);
                _messageContextService.Set(notification.EventId, new MessageContext(_contextAccessor.CorrelationId));
                domainEventNotifications.Add(notification);
            }

            var handlingTasks = _domainEventsPublisher.PublishAsync(domainEvent, cancellationToken);
            domainEventHandlingTasks.AddRange(handlingTasks);
        }

        domainEventsProvider.ClearAllDomainEvents();

        var outBoxMessages = domainEventNotifications.Select(d => new OutboxMessage()
        {
            Id = d.EventId,
            CorrelationId = _contextAccessor.CorrelationId,
            TraceId = _contextAccessor.TraceId,
            Name = nameof(IDomainEventNotification<IDomainEvent>),
            Type = d.GetType().ToString(),
            Module = d.GetModuleName(),
            CreatedAt = _clock.Now,
            Data = JsonConvert.SerializeObject(d)
        });

        await _outBox.SendAsync(outBoxMessages, dbContext, cancellationToken);

        await Task.WhenAll(domainEventHandlingTasks);
    }
}
