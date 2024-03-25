
using BuildingBlocks.Events.Basics;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Messaging.Outbox
{
    internal class OutBox : IOutbox
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<OutboxMessage> _messages;

        public OutBox(DbContext dbContext)
        {
            _dbContext = dbContext;
            _messages = _dbContext.Set<OutboxMessage>();
        }

        public async Task<List<OutboxMessage>> GetDomainEventNotificationsAsync(string module, CancellationToken cancellationToken = default)
        {
            if (_messages is null)
                return new List<OutboxMessage>(0);

            var messages = await _messages.Where(m => m.Name == nameof(IDomainEventNotification<IDomainEvent>)
                && m.Module == module
                && m.ProcessedAt == null)
                .ToListAsync(cancellationToken);

            return messages;
        }

        public async Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, CancellationToken cancellationToken = default)
        {
            if (_messages is null)
                return;

            await _messages.AddRangeAsync(outboxMessages, cancellationToken);
        }
    }
}
