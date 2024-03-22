
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Messaging.Outbox
{
    internal class OutBox : IOutbox
    {
        private readonly DbContext _dbContext;

        public OutBox(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendAsync(IEnumerable<OutboxMessage> outboxMessages, CancellationToken cancellationToken = default)
        {
            var dbSet = _dbContext.Set<OutboxMessage>();
            if (dbSet is null)
                return;

            await dbSet.AddRangeAsync(outboxMessages, cancellationToken);
        }
    }
}
