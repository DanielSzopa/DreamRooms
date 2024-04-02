using BuildingBlocks.Events.Basics;
using BuildingBlocks.Events.DomainEventNotificationHandlers;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace BuildingBlocks.Logging;

[Decorator]
internal class LoggingDomainEventNotificationHandlerDecorator<TEvent> : IDomainEventNotificationHandler<TEvent>
    where TEvent : class, IDomainEventNotification<IDomainEvent>
{
    private readonly IDomainEventNotificationHandler<TEvent> _decoratedNotificationHandler;
    private readonly ILogger<LoggingDomainEventNotificationHandlerDecorator<TEvent>> _logger;

    public LoggingDomainEventNotificationHandlerDecorator(IDomainEventNotificationHandler<TEvent> decoratedNotificationHandler,
        ILogger<LoggingDomainEventNotificationHandlerDecorator<TEvent>> logger)
    {
        _decoratedNotificationHandler = decoratedNotificationHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
    {
        var notification = @event.GetType().Name.Underscore();

        using (LogContext.Push(new ModuleEnricher(@event.GetModuleName())))
        {
            _logger.LogInformation("Handling a domain event notification: {DomainEventNotification}", notification);
            await _decoratedNotificationHandler.HandleAsync(@event, cancellationToken);
            _logger.LogInformation("Handled a domain event notification: {DomainEventNotification}", notification);
        }
    }
}
