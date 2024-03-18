using BuildingBlocks.Events.Basics;
using Staff.Core.Domain.Events;

namespace Staff.Core;
internal record ReceptionistCreatedNotification : DomainEventNotificationBase<ReceptionistCreated>
{
    internal ReceptionistCreatedNotification(ReceptionistCreated @event, Guid eventId) : base(@event, eventId)
    {
    }
}
