using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services
            .AddSingleton<ICommandDispatcher, CommandDispatcher>();
    }
}
