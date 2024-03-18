using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Modules;

public interface IModule
{
    IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration);

    IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpoints);
}
