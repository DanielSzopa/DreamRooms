namespace BuildingBlocks.Messaging.Outbox;

internal interface IOutbox
{
    Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, CancellationToken cancellationToken = default);
}
