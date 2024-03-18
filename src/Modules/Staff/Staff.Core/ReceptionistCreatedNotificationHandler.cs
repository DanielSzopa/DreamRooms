using BuildingBlocks.Events.DomainEventNotificationHandlers;

namespace Staff.Core;
internal class ReceptionistCreatedNotificationHandler : IDomainEventNotificationHandler<ReceptionistCreatedNotification>
{
    public Task HandleAsync(ReceptionistCreatedNotification notification, CancellationToken cancellationToken = default)
    {
        throw new Exception("tEST Notifications");
    }
}
