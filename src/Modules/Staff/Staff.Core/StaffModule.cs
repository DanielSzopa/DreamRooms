using BuildingBlocks.Abstractions.Modules;
using BuildingBlocks.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Staff.Core.Commands.Employees.SignUpReceptionist;
using Staff.Core.Security;

namespace Staff.Core;

public class StaffModule : IModule
{
    public IEndpointRouteBuilder ExposeEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/staff/receptionist", async (CancellationToken cancellationToken, SignUpReceptionistCommand command, ICommandDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command, cancellationToken);
            return Results.Created();
        });

        return endpoints;
    }

    public IServiceCollection RegisterServices(IServiceCollection services)
    {
        return services
            .AddSecurity();
    }
}
