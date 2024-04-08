using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Modules;
using Humanizer;
using MassTransit;
using Microsoft.Extensions.Logging;
using LogContext = Serilog.Context.LogContext;

namespace BuildingBlocks.Messaging.ConsumersMiddlewares;


public class ConsumerLoggingMiddleware<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly ILogger<ConsumerLoggingMiddleware<T>> _logger;

    public ConsumerLoggingMiddleware(ILogger<ConsumerLoggingMiddleware<T>> logger)
    {
        _logger = logger;
    }

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope(nameof(ConsumerLoggingMiddleware<T>));
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var type = typeof(T);
        var integrationEvent = type.Name.Underscore();
        var module = type.GetModuleName();

        using (LogContext.Push(new CorrelationIdEnricher((Guid)context.CorrelationId)))
        {
            _logger.LogInformation("Handling a integration event: {IntegrationEvent} from {Module} module", integrationEvent, module);
            await next.Send(context);
            _logger.LogInformation("Handled a integration event: {IntegrationEvent} from {Module} module", integrationEvent, module);
        }
    }
}
