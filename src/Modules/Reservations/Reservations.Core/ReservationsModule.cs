using BuildingBlocks.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Core.Persistence;

namespace Reservations.Core;
public class ReservationsModule : IModule
{
    public IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }

    public IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddReservationsPersistence(configuration);
    }
}
