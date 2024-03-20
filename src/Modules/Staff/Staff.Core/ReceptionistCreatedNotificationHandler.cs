using BuildingBlocks.Events.DomainEventNotificationHandlers;
using Microsoft.Extensions.Logging;

namespace Staff.Core;
internal class ReceptionistCreatedNotificationHandler : IDomainEventNotificationHandler<ReceptionistCreatedNotification>
{
    private readonly ILogger<ReceptionistCreatedNotificationHandler> _logger;

    public ReceptionistCreatedNotificationHandler(ILogger<ReceptionistCreatedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(ReceptionistCreatedNotification notification, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Test Notifications");
        return Task.CompletedTask;
    }
}
