//using BuildingBlocks.Modules;
//using Quartz;

//namespace BuildingBlocks.Messaging.Outbox.Jobs;
//public static class OutBoxDomainEventNotificationsJobExtensions<TModule>
//    where TModule : class, IModule
//{
//    public static IServiceCollectionQuartzConfigurator Create(this IServiceCollectionQuartzConfigurator config)
//    {
//        return config.AddJob<OutBoxDomainEventNotificationsJob<TModule>>(OutBoxDomainEventNotificationsJob<TModule>.Key)
//            .AddTrigger(trigger =>
//            {
//                trigger.ForJob(OutBoxDomainEventNotificationsJob<TModule>.Key)
//                .WithSimpleSchedule(schedule =>
//                {
//                    schedule.WithIntervalInSeconds(1)
//                    .RepeatForever();
//                });
//            });
//    }
//}
