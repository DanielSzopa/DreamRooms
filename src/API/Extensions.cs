using BuildingBlocks.Messaging.Outbox.Jobs;
using BuildingBlocks.Modules;
using MassTransit;
using Quartz;
using Staff.Core;

namespace Api;

internal static class Extensions
{
    internal static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        return services.AddMassTransit(cfg =>
        {
            cfg.AddConsumers(ModulesRegistrator.ModulesAssemblies.ToArray());

            cfg.UsingInMemory((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }

    internal static IServiceCollection AddMessagingJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.AddJob<OutBoxDomainEventNotificationsJob<StaffModule>>(OutBoxDomainEventNotificationsJob<StaffModule>.Key)
            .AddTrigger(trigger =>
            {
                trigger.ForJob(OutBoxDomainEventNotificationsJob<StaffModule>.Key)
                .WithSimpleSchedule(schedule =>
                {
                    schedule.WithIntervalInSeconds(3)
                    .RepeatForever();
                });
            });
        })
        .AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

}
