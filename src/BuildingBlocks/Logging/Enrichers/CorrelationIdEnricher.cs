using BuildingBlocks.Context;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;
internal class CorrelationIdEnricher : ILogEventEnricher
{
    private readonly IContextAccessor _context;

    public CorrelationIdEnricher(IContextAccessor context)
    {
        _context = context;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(Constants.CorrelationId, _context.CorrelationId));
    }
}
