using BuildingBlocks.Context;
using BuildingBlocks.Events.Basics;
using MassTransit;
using MessageContext = BuildingBlocks.Context.MessageContext;

namespace BuildingBlocks.Messaging.Bus;
internal class IntegrationEventsBus : IIntegrationEventsBus
{
    private readonly IBus _bus;
    private readonly IMessageContextService _messageContextService;

    public IntegrationEventsBus(IBus bus, IMessageContextService messageContextService)
    {
        _bus = bus;
        _messageContextService = messageContextService;
    }

    public async Task PublishAsync<T>(T @event, MessageContext context, CancellationToken cancellationToken)
        where T: class, IIntegrationEvent
    {
        _messageContextService.Set(@event.EventId, context);
        await _bus.Publish(@event, c => c.CorrelationId = context.CorrelationId, cancellationToken);
    }
}
