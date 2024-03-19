using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.NotificationsRegistery;
internal class DomainEventNotificationsRegistery
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
        var result = DomainEventsMapper.TryGetValue(domainEventType, out var domainEventNotification);
        return result ? domainEventNotification : null;
    }
}
