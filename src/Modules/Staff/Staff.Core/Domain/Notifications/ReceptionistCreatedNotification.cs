using BuildingBlocks.Events.Basics;
using Newtonsoft.Json;
using Staff.Core.Domain.Events;

namespace Staff.Core.Domain.Notifications;
internal record ReceptionistCreatedNotification : DomainEventNotificationBase<ReceptionistCreated>
{
    // Have to be public, in order to allow DomainEventNotificationsCreator create it
    public ReceptionistCreatedNotification([JsonProperty(nameof(IDomainEventNotification<IDomainEvent>.DomainEvent))] ReceptionistCreated @event, Guid eventId) : base(@event, eventId)
    {
    }
}
