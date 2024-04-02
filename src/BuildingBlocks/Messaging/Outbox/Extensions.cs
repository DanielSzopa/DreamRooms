using BuildingBlocks.Messaging.Outbox.Commands;
using BuildingBlocks.Messaging.Outbox.Processors;
using BuildingBlocks.Messaging.Outbox.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.Outbox;
internal static class Extensions
{
    internal static IServiceCollection AddOutbox(this IServiceCollection services)
    {
        return services
            .AddScoped<IDomainEventNotificationOutBoxProcessor, DomainEventNotificationOutBoxProcessor>()
            .AddSingleton<IDomainEventNotificationOutBoxRepository, DomainEventNotificationOutBoxRepository>();
    }
}
