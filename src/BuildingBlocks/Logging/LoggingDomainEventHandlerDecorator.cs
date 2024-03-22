using BuildingBlocks.Context;
using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventsHandlers;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging;

[Decorator]
internal class LoggingDomainEventHandlerDecorator<TEvent> : IDomainEventHandler<TEvent>
    where TEvent : class, IDomainEvent
{
    private readonly IDomainEventHandler<TEvent> _decoratedDomainEventHandler;
    private readonly ILogger<LoggingDomainEventHandlerDecorator<TEvent>> _logger;
    private readonly IContextAccessor _context;

    public LoggingDomainEventHandlerDecorator(IDomainEventHandler<TEvent> decoratedDomainEventHandler, ILogger<LoggingDomainEventHandlerDecorator<TEvent>> logger,
        IContextAccessor contextAccessor)
    {
        _decoratedDomainEventHandler = decoratedDomainEventHandler;
        _logger = logger;
        _context = contextAccessor;
    }

    public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        var correlationId = _context.CorrelationId;
        var traceId = _context.TraceId;

        var module = @event.GetModuleName();
        var name = @event.GetType().Name.Underscore();

        _logger.LogInformation("Handling a domain event {name} [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);
        await _decoratedDomainEventHandler.HandleAsync(@event, cancellationToken);
        _logger.LogInformation("Handled a domain event {name} [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);
    }
}
