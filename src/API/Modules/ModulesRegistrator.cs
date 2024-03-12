using BuildingBlocks.Abstractions.Modules;
using Reservations.Core;
using Staff.Core;
using System.Reflection;

namespace BuildingBlocks.Modules;

internal static class ModulesRegistrator
{
    private static readonly List<IModule> _modules;

    internal static List<Assembly> ModulesAssemblies = new List<Assembly>();

    static ModulesRegistrator()
    {
        _modules = new List<IModule>
        {
            new StaffModule(),
            new ReservationsModule()
        };

        _modules.ForEach(m => ModulesAssemblies.Add(m.GetType().Assembly));
    }

    internal static IServiceCollection RegisterModulesServices(this IServiceCollection services, IConfiguration configuration)
    {
        foreach(var module in _modules)
        {
            module.RegisterServices(services, configuration);
        }

        return services;
    }

    internal static IEndpointRouteBuilder ExposeModulesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        foreach(var module in _modules)
        {
            module.ExposeEndpoints(endpoints);
        }

        return endpoints;
    }
}
