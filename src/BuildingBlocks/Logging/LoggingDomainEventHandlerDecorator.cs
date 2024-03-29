using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventsHandlers;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace BuildingBlocks.Logging;

[Decorator]
internal class LoggingDomainEventHandlerDecorator<TEvent> : IDomainEventHandler<TEvent>
    where TEvent : class, IDomainEvent
{
    private readonly IDomainEventHandler<TEvent> _decoratedDomainEventHandler;
    private readonly ILogger<LoggingDomainEventHandlerDecorator<TEvent>> _logger;

    public LoggingDomainEventHandlerDecorator(IDomainEventHandler<TEvent> decoratedDomainEventHandler, ILogger<LoggingDomainEventHandlerDecorator<TEvent>> logger)
    {
        _decoratedDomainEventHandler = decoratedDomainEventHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        var domainEvent = @event.GetType().Name.Underscore();

        using (LogContext.Push(new ModuleEnricher(@event.GetModuleName())))
        {
            _logger.LogInformation("Handling a domain event: {DomainEvent}", domainEvent);
            await _decoratedDomainEventHandler.HandleAsync(@event, cancellationToken);
            _logger.LogInformation("Handled a domain event: {DomainEvent}", domainEvent);
        }
    }
}
