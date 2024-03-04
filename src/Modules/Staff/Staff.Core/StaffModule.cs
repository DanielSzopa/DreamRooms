using BuildingBlocks.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Staff.Core;

public class StaffModule : IModule
{
    public IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        return endpointRouteBuilder;
    }

    public IServiceCollection RegisterServices(IServiceCollection services)
    {
        return services;
    }
}
