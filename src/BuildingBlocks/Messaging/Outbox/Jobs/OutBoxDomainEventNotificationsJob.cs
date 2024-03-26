using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Events.NotificationsRegistery;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Modules;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
    private readonly DomainEventNotificationsRegistery _domainEventNotificationsRegistery;

    public OutBoxDomainEventNotificationsJob(IServiceProvider serviceProvider, DbContextTypeRegistery dbContextTypeRegistery
        ,ILogger<OutBoxDomainEventNotificationsJob<IModule>> logger, IClock clock, DomainEventNotificationsRegistery domainEventNotificationsRegistery)
    {
        _serviceProvider = serviceProvider;
        _dbContextTypeRegistery = dbContextTypeRegistery;
        _logger = logger;
        _clock = clock;
        _domainEventNotificationsRegistery = domainEventNotificationsRegistery;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var dbContextType = _dbContextTypeRegistery.Resolve<TModule>();
        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = (DbContext)scope.ServiceProvider.GetRequiredService(dbContextType);
        var outBox = new OutBox(dbContext);

        var messages = await outBox.GetDomainEventNotificationsAsync(ModuleName, context.CancellationToken);
        if (!messages.Any())
        {
            return;
        }

        foreach( var message in messages)
        {
            var notificationType = _domainEventNotificationsRegistery.ResolveDomainEventNotificationFromStringType(message.Type);
            var notification = (IDomainEventNotification<IDomainEvent>)JsonConvert.DeserializeObject(message.Data, notificationType);
            var handlerType = typeof(IDomainEventNotificationHandler<>).MakeGenericType(notificationType);
            var notificationHandlers = scope.ServiceProvider.GetServices(handlerType);
        }

        _logger.LogInformation($"{_clock.Now}, job outbox {Key.Name} test!!!!");
    }
}
