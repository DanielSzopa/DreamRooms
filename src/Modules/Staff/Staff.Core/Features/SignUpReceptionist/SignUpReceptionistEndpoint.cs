using BuildingBlocks.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Staff.Core.Features.SignUpReceptionist;

internal static class SignUpReceptionistEndpoint
{
    internal static void SignUpReceptionist(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/staff/receptionist", async (CancellationToken cancellationToken, SignUpReceptionistCommand command, ICommandDispatcher dispatcher) =>
        {
            await dispatcher.SendAsync(command, cancellationToken);
            return Results.Created();
        });
    }
}
