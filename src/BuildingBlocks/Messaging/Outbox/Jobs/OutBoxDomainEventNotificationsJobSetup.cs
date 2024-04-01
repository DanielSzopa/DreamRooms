using BuildingBlocks.Modules;
using Quartz;

namespace BuildingBlocks.Messaging.Outbox.Jobs;
public static class OutBoxDomainEventNotificationsJobSetup
{
    public static IServiceCollectionQuartzConfigurator AddDomainEventNotificationsJob<TJob>(this IServiceCollectionQuartzConfigurator config)
        where TJob : class, IJob
    {
        var module = typeof(TJob).GetModuleName();
        var key = new JobKey($"{module}-OutBox-DomainEventNotifications-Job", OutBoxConstants.OutBoxDomainEventNotificationsGroup);

        return config.AddJob<TJob>(key)
            .AddTrigger(trigger =>
            {
                trigger.ForJob(key)
                .WithSimpleSchedule(schedule =>
                {
                    schedule.WithIntervalInSeconds(2)
                    .RepeatForever();
                });
            });
    }
}
