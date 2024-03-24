using BuildingBlocks.Messaging.Outbox.Jobs;
using BuildingBlocks.Modules;
using MassTransit;
using Quartz;
using Staff.Core;

namespace Api;

internal static class Extensions
{
    internal static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        return services
            .AddMessageBroker()
            .AddMessagingJobs();
    }

    private static IServiceCollection AddMessageBroker(this IServiceCollection services)
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

    private static IServiceCollection AddMessagingJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.AddDomainEventNotificationsJob<StaffModule>();
        })
        .AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

}
