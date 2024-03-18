using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Logging;
using BuildingBlocks.UnitOfWork;
using BuildingBlocks.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Commands;
internal static class Extensions
{
    internal static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

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
