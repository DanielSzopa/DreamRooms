using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Middlewares;
internal class CorrelationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = Guid.NewGuid();
        context.Request?.Headers?.Append(Constants.CorrelationIdHeader, correlationId.ToString());

        await next.Invoke(context);
    }
}
