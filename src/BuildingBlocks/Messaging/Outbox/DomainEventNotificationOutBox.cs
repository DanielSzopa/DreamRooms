
using BuildingBlocks.Events.Basics;
using BuildingBlocks.Helpers.Clock;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Messaging.Outbox
{
    internal class DomainEventNotificationOutBox : IDomainEventNotificationOutBox
    {
        private readonly IClock _clock;

        public DomainEventNotificationOutBox(IClock clock)
        {
            _clock = clock;
        }

        public async Task<List<OutboxMessage>> GetMessagesAsync(string module, DbContext dbContext, CancellationToken cancellationToken = default)
        {
            var messagesDbSet = OutboxMessages(dbContext);
            if (messagesDbSet is null)
                return new List<OutboxMessage>(0);

            var messages = await messagesDbSet.Where(m => m.Name == nameof(IDomainEventNotification<IDomainEvent>)
                && m.Module == module
                && m.ProcessedAt == null)
                .ToListAsync(cancellationToken);

            return messages;
        }

        public async Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, DbContext dbContext, CancellationToken cancellationToken = default)
        {
            var messagesDbSet = OutboxMessages(dbContext);
            if (messagesDbSet is null)
                return;

            await messagesDbSet.AddRangeAsync(outboxMessages, cancellationToken);
        }

        public void Clean(IEnumerable<OutboxMessage> outboxMessages)
        {
            if (outboxMessages is null || outboxMessages.Count() <= 0)
                return;

            foreach(var message in outboxMessages)
            {
                message.ProcessedAt = _clock.Now;
            }
        }

        private DbSet<OutboxMessage> OutboxMessages(DbContext dbContext)
        {
            return dbContext.Set<OutboxMessage>();
        }
    }
}
