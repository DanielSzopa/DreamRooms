using BuildingBlocks.Events.Basics;
using Staff.Core.Domain.Events;

namespace Staff.Core;
internal record ReceptionistCreatedNotification : DomainEventNotificationBase<ReceptionistCreated>
{
    // Have to be public, in order to allow DomainEventNotificationsCreator create it
    public ReceptionistCreatedNotification(ReceptionistCreated @event, Guid eventId) : base(@event, eventId)
    {
    }
}
