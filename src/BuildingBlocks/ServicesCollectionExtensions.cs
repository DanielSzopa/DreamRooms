using BuildingBlocks.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        return services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
    }
}
