using BuildingBlocks.Modules;
using MassTransit;

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


}
