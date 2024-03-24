using BuildingBlocks.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Events;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Messaging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.Persistence;
using BuildingBlocks.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks;

public static class Extensions
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddSingleton<IContextAccessor, ContextAccessor>()
            .AddExceptionHandler<GlobalExcepionsMiddleware>()
            .AddSingleton<CorrelationMiddleware>()
            .AddDomainEvents()
            .AddCommands()
            .AddSingleton(new UnitOfWorkTypeRegistery())
            .AddSingleton(new DbContextTypeRegistery())
            .AddSingleton<IClock, Clock>();
    }
}
