using Microsoft.AspNetCore.Builder;

namespace BuildingBlocks.Middlewares;
public static class Extensions
{
    public static void UseCorrelationMiddleware(this WebApplication app)
    {
        app.UseMiddleware<CorrelationMiddleware>();
    }
}
