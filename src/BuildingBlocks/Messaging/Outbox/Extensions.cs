using BuildingBlocks.Messaging.Outbox.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.Outbox;
internal static class Extensions
{
    internal static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        return services.AddSingleton<IDomainEventNotificationOutBoxRepository, DomainEventNotificationOutBoxRepository>();
    }
}
