using BuildingBlocks.Abstractions.Modules;
using Reservations.Core;
using Staff.Core;

namespace BuildingBlocks.Modules;

internal static class ModulesRegistrator
{
    private static readonly List<IModule> _modules =
    [
        new StaffModule(),
        new ReservationsModule()
    ];

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
