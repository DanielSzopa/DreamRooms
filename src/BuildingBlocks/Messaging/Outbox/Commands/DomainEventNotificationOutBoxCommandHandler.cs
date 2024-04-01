using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Events.Publishers;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Messaging.Outbox.Repositories;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Messaging.Outbox.Commands
{
    internal class DomainEventNotificationOutBoxCommandHandler : ICommandHandler<DomainEventNotificationOutBoxCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextTypeRegistery _dbContextTypeRegistery;
        private readonly IDomainEventNotificationsPublisher _publisher;
        private readonly IClock _clock;
        private readonly ILogger<DomainEventNotificationOutBoxCommandHandler> _logger;
        private readonly IDomainEventNotificationOutBoxRepository _outBox;

        public DomainEventNotificationOutBoxCommandHandler(IServiceProvider serviceProvider, DbContextTypeRegistery dbContextTypeRegistery,
            IDomainEventNotificationsPublisher publisher, IClock clock, ILogger<DomainEventNotificationOutBoxCommandHandler> logger,
            IDomainEventNotificationOutBoxRepository outbox)
        {
            _serviceProvider = serviceProvider;
            _dbContextTypeRegistery = dbContextTypeRegistery;
            _publisher = publisher;
            _clock = clock;
            _logger = logger;
            _outBox = outbox;
        }

        public async Task HandleAsync(DomainEventNotificationOutBoxCommand command, CancellationToken cancellationToken = default)
        {
            var dbContextType = _dbContextTypeRegistery.Resolve(command.Module);
            var dbContext = (DbContext)_serviceProvider.GetRequiredService(dbContextType);

            var messages = await _outBox.GetMessagesAsync(command.Module, dbContext, cancellationToken);
            if (!messages.Any())
            {
                return;
            }

            var tasksResults = new List<Task>();

            foreach (var message in messages)
            {
                var notificationHandlingTasks = _publisher.PublishAsync(message.Type, message.Data, cancellationToken);
                tasksResults.AddRange(notificationHandlingTasks);
            }

            await Task.WhenAll(tasksResults);

            _outBox.Clean(messages);

            await dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"{_clock.Now}, job notification outbox test!!!!");
        }
    }
}
