namespace BuildingBlocks.Messaging.Outbox;

internal interface IOutbox
{
    Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, CancellationToken cancellationToken = default);

    Task<List<OutboxMessage>> GetDomainEventNotificationsAsync(string module, CancellationToken cancellationToken = default);
}
