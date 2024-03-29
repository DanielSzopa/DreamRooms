using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;
public class ModuleEnricher : ILogEventEnricher
{
    private readonly string _moduleName;
    public ModuleEnricher(string module)
    {
        _moduleName = module;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(Constants.Module, _moduleName));
    }
}
