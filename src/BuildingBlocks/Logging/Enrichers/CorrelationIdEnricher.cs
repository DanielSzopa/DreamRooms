using BuildingBlocks.Context;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;
internal class CorrelationIdEnricher : ILogEventEnricher
{
    private readonly Guid _correlationId;
    public CorrelationIdEnricher(Guid correlationId)
    {
        _correlationId = correlationId;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(Constants.CorrelationId, _correlationId));
    }
}
