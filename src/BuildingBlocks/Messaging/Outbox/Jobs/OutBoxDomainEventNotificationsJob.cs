using BuildingBlocks.Events.Publishers;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Modules;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BuildingBlocks.Messaging.Outbox.Jobs;

[DisallowConcurrentExecution]
internal class OutBoxDomainEventNotificationsJob<TModule> : IJob
    where TModule : class, IModule
{
    private static readonly string ModuleName = typeof(TModule).GetModuleName();
    internal static readonly JobKey Key = new JobKey($"{ModuleName}-OutBox-DomainEventNotifications-Job", "OutBox-DomainEvenNotifications-Group");


    private readonly IServiceProvider _serviceProvider;
    private readonly DbContextTypeRegistery _dbContextTypeRegistery;
    private readonly ILogger<OutBoxDomainEventNotificationsJob<IModule>> _logger;
    private readonly IClock _clock;
    private readonly IDomainEventNotificationsPublisher _domainEventNotificationsPublisher;
    private readonly IDomainEventNotificationOutBox _outBox;

    public OutBoxDomainEventNotificationsJob(IServiceProvider serviceProvider, DbContextTypeRegistery dbContextTypeRegistery
        , ILogger<OutBoxDomainEventNotificationsJob<IModule>> logger, IClock clock, IDomainEventNotificationsPublisher domainEventNotificationsPublisher,
        IDomainEventNotificationOutBox outBox)
    {
        _serviceProvider = serviceProvider;
        _dbContextTypeRegistery = dbContextTypeRegistery;
        _logger = logger;
        _clock = clock;
        _domainEventNotificationsPublisher = domainEventNotificationsPublisher;
        _outBox = outBox;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var dbContextType = _dbContextTypeRegistery.Resolve<TModule>();
        var dbContext = (DbContext)_serviceProvider.GetRequiredService(dbContextType);

        var messages = await _outBox.GetMessagesAsync(ModuleName, dbContext, context.CancellationToken);
        if (!messages.Any())
        {
            return;
        }

        var tasksResults = new List<Task>();

        foreach (var message in messages)
        {
            var notificationHandlingTasks = _domainEventNotificationsPublisher.PublishAsync(message.Type, message.Data, context.CancellationToken);
            tasksResults.AddRange(notificationHandlingTasks);
        }

        await Task.WhenAll(tasksResults);

        _outBox.Clean(messages);

        await dbContext.SaveChangesAsync();

        _logger.LogInformation($"{_clock.Now}, job outbox {Key.Name} test!!!!");
    }
}
