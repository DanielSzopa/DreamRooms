using BuildingBlocks.Events.Basics;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Events.NotificationsRegistery;
public static class Extensions
{
    public static IServiceCollection RegisterDomainEventNotification<TDomainEvent, TDomainEventNotification>(this IServiceCollection services)
        where TDomainEvent : class, IDomainEvent
        where TDomainEventNotification : class, IDomainEventNotification<IDomainEvent>
    {
        var serviceProvider = services.BuildServiceProvider();
        var registery = serviceProvider.GetRequiredService<DomainEventNotificationsRegistery>();
        registery.Register<TDomainEvent,TDomainEventNotification>();

        return services;
    } 
}
