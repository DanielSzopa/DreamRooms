using BuildingBlocks.Context;
using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Messaging.Bus;
using Microsoft.Extensions.Logging;
using Staff.Contracts;

namespace Staff.Core.Domain.Notifications;
internal class ReceptionistCreatedNotificationHandler : IDomainEventNotificationHandler<ReceptionistCreatedNotification>
{
    private readonly ILogger<ReceptionistCreatedNotificationHandler> _logger;
    private readonly IIntegrationEventsBus _bus;
    private readonly IMessageContextService _messageContextService;

    public ReceptionistCreatedNotificationHandler(ILogger<ReceptionistCreatedNotificationHandler> logger, IIntegrationEventsBus bus, IMessageContextService messageContext)
    {
        _logger = logger;
        _bus = bus;
        _messageContextService = messageContext;
    }

    public async Task HandleAsync(ReceptionistCreatedNotification notification, CancellationToken cancellationToken = default)
    {
        var integrationEventContext = _messageContextService.CreateNewContextWithValuesFromPreviousOne(notification.EventId);
        var @event = new ReceptionistCreatedIntegrationEvent(notification.DomainEvent.FullName, notification.DomainEvent.Email);
        await _bus.PublishAsync(@event, integrationEventContext, cancellationToken);
        _logger.LogInformation("Test Notifications");
    }
}
