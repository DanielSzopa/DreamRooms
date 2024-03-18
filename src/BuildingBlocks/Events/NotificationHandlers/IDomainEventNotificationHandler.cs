using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Events.DomainEventNotificationHandlers;
public interface IDomainEventNotificationHandler<in TNotification>
    where TNotification : class, IDomainEventNotification<IDomainEvent>
{
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken = default);
}
