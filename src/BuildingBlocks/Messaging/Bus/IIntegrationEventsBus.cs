using BuildingBlocks.Context;
using BuildingBlocks.Events.Basics;

namespace BuildingBlocks.Messaging.Bus;
public interface IIntegrationEventsBus
{
    Task PublishAsync<T>(T @event, MessageContext context, CancellationToken cancellationToken)
        where T : class, IIntegrationEvent;
}
