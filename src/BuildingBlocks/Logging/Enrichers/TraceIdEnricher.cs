using BuildingBlocks.Context;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;
internal class TraceIdEnricher : ILogEventEnricher
{
    private readonly IContextAccessor _context;

    public TraceIdEnricher(IContextAccessor context)
    {
        _context = context;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent("RequestId");
        logEvent.RemovePropertyIfPresent("ConnectionId");
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(Constants.TraceId, _context.TraceId));
    }
}
