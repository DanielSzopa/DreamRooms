using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.NotificationsRegistery;
public class DomainEventNotificationsRegistery
{
    internal Dictionary<Type, Type> DomainEventsMapper = new Dictionary<Type, Type>();

    internal void Register<TDomainEvent, TDomainEventNotification>()
        where TDomainEvent : class, IDomainEvent
        where TDomainEventNotification : class, IDomainEventNotification<IDomainEvent>
    {
        DomainEventsMapper[typeof(TDomainEvent)] = typeof(TDomainEventNotification);
    }

    internal Type Resolve(Type domainEventType)
    {
        return DomainEventsMapper[domainEventType];
    }
}
