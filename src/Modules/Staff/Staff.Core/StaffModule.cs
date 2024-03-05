using BuildingBlocks.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Staff.Core.Features.SignUpReceptionist;
using Staff.Core.Security;

namespace Staff.Core;

public class StaffModule : IModule
{
    public IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.SignUpReceptionist();

        return endpoints;
    }

    public IServiceCollection RegisterServices(IServiceCollection services)
    {
        return services
            .AddSecurity();
    }
}
