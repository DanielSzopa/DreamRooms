using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;
public class RemoveUnnecessaryPropertiesEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        //Remove default properties added by Serilog
        logEvent.RemovePropertyIfPresent("RequestId");
        logEvent.RemovePropertyIfPresent("ConnectionId");
    }
}
