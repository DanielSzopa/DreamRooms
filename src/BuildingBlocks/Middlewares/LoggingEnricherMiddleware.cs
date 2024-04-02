using BuildingBlocks.Context;
using BuildingBlocks.Logging.Enrichers;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace BuildingBlocks.Middlewares;
internal class LoggingEnricherMiddleware : IMiddleware
{
    private readonly IContextAccessor _context;

    public LoggingEnricherMiddleware(IContextAccessor context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        using (LogContext.Push(new TraceIdEnricher(_context), new CorrelationIdEnricher(_context.CorrelationId)))
        {
            await next.Invoke(context);
        }
    }
}
