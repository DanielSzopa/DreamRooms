using BuildingBlocks.Events.Dispatcher;
using BuildingBlocks.Events.DomainEventsHandlers;
using BuildingBlocks.Events.Publisher;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events;
internal static class Extensions
{
    internal static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services
            .AddDomainEventHandlers()
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddSingleton<IDomainEventsPublisher,DomainEventsPublisher>();

        return services;
    }

    private static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        services.Scan(s => s.FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        return services;
    }
}
