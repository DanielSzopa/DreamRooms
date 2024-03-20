using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.NotificationsCreator;
internal class DomainEventNotificationsCreator : IDomainEventNotificationsCreator
{
    public IDomainEventNotification<IDomainEvent> Create(Type domainEventNotificationType, IDomainEvent domainEvent)
    {
        object[] args = new object[] { domainEvent, domainEvent.EventId };
        object obj = Activator.CreateInstance(domainEventNotificationType, args);
        return obj as IDomainEventNotification<IDomainEvent>;
    }
}
