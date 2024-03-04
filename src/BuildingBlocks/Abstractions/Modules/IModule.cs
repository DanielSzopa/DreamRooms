using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Abstractions.Modules;

public interface IModule
{
    IServiceCollection RegisterServices(IServiceCollection services);

    IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpointRouteBuilder);
}
