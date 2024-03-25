using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.NotificationsRegistery;
internal class DomainEventNotificationsRegistery
{
    internal Dictionary<Type, Type> FromDomainEventToDomainEventNotifications = new Dictionary<Type, Type>();
    internal Dictionary<string, Type> FromStringTypeToDomainEventNotifications = new Dictionary<string, Type>();

    internal void Register<TDomainEvent, TDomainEventNotification>()
        where TDomainEvent : class, IDomainEvent
        where TDomainEventNotification : class, IDomainEventNotification<IDomainEvent>
    {
        FromDomainEventToDomainEventNotifications[typeof(TDomainEvent)] = typeof(TDomainEventNotification);
        FromStringTypeToDomainEventNotifications[typeof(TDomainEventNotification).ToString()] = typeof(TDomainEventNotification);
    }

    internal Type ResolveFromDomainEvent(Type domainEventType)
    {
        var result = FromDomainEventToDomainEventNotifications.TryGetValue(domainEventType, out var domainEventNotification);
        return result ? domainEventNotification : null;
    }

    internal Type ResolveDomainEventNotificationFromStringType(string domainEventNotificationType)
    {
        var result = FromStringTypeToDomainEventNotifications.TryGetValue(domainEventNotificationType, out var domainEventNotification);
        return result ? domainEventNotification : null;
    }
}
