namespace BuildingBlocks.Messaging.Outbox.Processors;
public interface IDomainEventNotificationOutBoxProcessor
{
    Task ProcessAsync(string module, CancellationToken cancellationToken);
}
