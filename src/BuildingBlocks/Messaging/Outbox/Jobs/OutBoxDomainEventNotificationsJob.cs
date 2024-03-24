using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Modules;
using BuildingBlocks.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BuildingBlocks.Messaging.Outbox.Jobs;

[DisallowConcurrentExecution]
public class OutBoxDomainEventNotificationsJob<TModule> : IJob
    where TModule : class, IModule
{
    public static readonly JobKey Key = new JobKey($"{typeof(TModule).GetModuleName()}-OutBox-DomainEventNotifications-Job", "OutBox-DomainEvenNotifications-Group");


    private readonly IServiceProvider _serviceProvider;
    private readonly DbContextTypeRegistery _dbContextTypeRegistery;
    private readonly ILogger<OutBoxDomainEventNotificationsJob<IModule>> _logger;
    private readonly IClock _clock;

    public OutBoxDomainEventNotificationsJob(IServiceProvider serviceProvider, DbContextTypeRegistery dbContextTypeRegistery
        ,ILogger<OutBoxDomainEventNotificationsJob<IModule>> logger, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _dbContextTypeRegistery = dbContextTypeRegistery;
        _logger = logger;
        _clock = clock;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var dbContextType = _dbContextTypeRegistery.Resolve<TModule>();
        using var scope = _serviceProvider.CreateAsyncScope();
        var dbContext = (DbContext)scope.ServiceProvider.GetRequiredService(dbContextType);

        _logger.LogInformation($"{_clock.Now}, job outbox test!!!!");
        return Task.CompletedTask;
    }
}
