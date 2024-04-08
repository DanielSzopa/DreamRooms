using BuildingBlocks.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Events;
using BuildingBlocks.Helpers.Clock;
using BuildingBlocks.Messaging.Bus;
using BuildingBlocks.Messaging.Outbox;
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
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddSingleton<IMessageContextService, MessageContextService>()
            .AddSingleton<IContextAccessor, ContextAccessor>()
            .AddExceptionHandler<GlobalExcepionsMiddleware>()
            .AddSingleton<CorrelationMiddleware>()
            .AddSingleton<LoggingEnricherMiddleware>()
            .AddDomainEvents()
            .AddCommands()
            .AddSingleton(new UnitOfWorkTypeRegistery())
            .AddSingleton(new DbContextTypeRegistery())
            .AddSingleton<IClock, Clock>()
            .AddScoped<IIntegrationEventsBus, IntegrationEventsBus>()
            .AddOutbox();
    }
}
