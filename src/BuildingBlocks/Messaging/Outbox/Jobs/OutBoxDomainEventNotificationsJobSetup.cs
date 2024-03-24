using BuildingBlocks.Modules;
using Quartz;

namespace BuildingBlocks.Messaging.Outbox.Jobs;
public static class OutBoxDomainEventNotificationsJobSetup
{
    public static IServiceCollectionQuartzConfigurator AddDomainEventNotificationsJob<TModule>(this IServiceCollectionQuartzConfigurator config)
        where TModule : class, IModule
    {
        return config.AddJob<OutBoxDomainEventNotificationsJob<TModule>>(OutBoxDomainEventNotificationsJob<TModule>.Key)
            .AddTrigger(trigger =>
            {
                trigger.ForJob(OutBoxDomainEventNotificationsJob<TModule>.Key)
                .WithSimpleSchedule(schedule =>
                {
                    schedule.WithIntervalInSeconds(2)
                    .RepeatForever();
                });
            });
    }
}
