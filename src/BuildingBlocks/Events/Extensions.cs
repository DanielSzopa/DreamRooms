using BuildingBlocks.Events.Dispatcher;
using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Events.DomainEventsHandlers;
using BuildingBlocks.Events.NotificationsCreator;
using BuildingBlocks.Events.NotificationsRegistery;
using BuildingBlocks.Events.Publishers;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events;
internal static class Extensions
{
    internal static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services
            .AddDomainEventHandlers()
            .AddDomainEventNotificationHandlers()
            .AddSingleton<IDomainEventNotificationsCreator, DomainEventNotificationsCreator>()
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddScoped<IDomainEventsPublisher,DomainEventsPublisher>()
            .AddSingleton(new DomainEventNotificationsRegistery());

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

    private static IServiceCollection AddDomainEventNotificationHandlers(this IServiceCollection services)
    {
        services.Scan(s => s.FromApplicationDependencies()
        .AddClasses(c => c.AssignableTo(typeof(IDomainEventNotificationHandler<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        return services;
    }
}
