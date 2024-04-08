using BuildingBlocks.Messaging.ConsumersMiddlewares;
using BuildingBlocks.Messaging.Outbox.Jobs;
using BuildingBlocks.Modules;
using MassTransit;
using MassTransit.Metadata;
using Quartz;
using Staff.Core.Outbox;
using System.Reflection;

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
            cfg.AddConsumersIncludingInternalTypes(ModulesRegistrator.ModulesAssemblies.ToArray());

            cfg.UsingInMemory((ctx, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(ConsumerLoggingMiddleware<>), ctx);
                cfg.ConfigureEndpoints(ctx);
            });
        });
    }

    private static void AddConsumersIncludingInternalTypes(this IBusRegistrationConfigurator cfg, Assembly[] assemblies)
    {
        var types = assemblies
            .SelectMany(a => a.GetTypes().Where(t => RegistrationMetadata.IsConsumerOrDefinition(t)))
            ?.ToArray();
        cfg.AddConsumers(types);
    }

    private static IServiceCollection AddMessagingJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            options.AddDomainEventNotificationsJob<StaffOutBoxDomainEventNotificationsJob>();
        })
        .AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }

}
