using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Commands;
using BuildingBlocks.Events;
using BuildingBlocks.Logging;
using BuildingBlocks.Middlewares;
using BuildingBlocks.UnitOfWork;
using BuildingBlocks.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks;

public static class Extensions
{
    public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddExceptionHandler<GlobalExcepionsMiddleware>()
            .AddDomainEvents()
            .AddCommandHandlers()
            .AddSingleton(new UnitOfWorkTypeRegistery())
            .AddSingleton<ICommandDispatcher, CommandDispatcher>();
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<>), typeof(ValidatorCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}
