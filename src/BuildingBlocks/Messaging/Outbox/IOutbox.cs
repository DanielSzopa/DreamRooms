using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Messaging.Outbox;

internal interface IOutbox
{
    Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, DbContext dbContext, CancellationToken cancellationToken = default);

    Task<List<OutboxMessage>> GetMessagesAsync(string module, DbContext dbContext, CancellationToken cancellationToken = default);

    void Clean(IEnumerable<OutboxMessage> outboxMessages);
}
