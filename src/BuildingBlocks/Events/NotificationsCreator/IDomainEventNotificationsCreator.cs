using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.NotificationsCreator;
internal interface IDomainEventNotificationsCreator
{
    IDomainEventNotification<IDomainEvent> Create(Type domainEventNotificationType, IDomainEvent domainEvent);
}
