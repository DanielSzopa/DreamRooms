using BuildingBlocks.Events.DomainEventNotificationHandlers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Staff.Contracts;

namespace Staff.Core.Domain.Notifications;
internal class ReceptionistCreatedNotificationHandler : IDomainEventNotificationHandler<ReceptionistCreatedNotification>
{
    private readonly ILogger<ReceptionistCreatedNotificationHandler> _logger;
    private readonly IBus _bus;

    public ReceptionistCreatedNotificationHandler(ILogger<ReceptionistCreatedNotificationHandler> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task HandleAsync(ReceptionistCreatedNotification notification, CancellationToken cancellationToken = default)
    {
        var @event = new ReceptionistCreatedIntegrationEvent(notification.EventId, notification.DomainEvent.FullName, notification.DomainEvent.Email);
        await _bus.Publish(@event, cancellationToken);
        _logger.LogInformation("Test Notifications");
    }
}
 