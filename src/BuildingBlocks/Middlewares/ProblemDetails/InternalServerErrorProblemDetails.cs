using System.Diagnostics;
using System.Net;

namespace BuildingBlocks.Middlewares.ProblemDetails;

internal class InternalServerErrorProblemDetails
{
    internal string TraceId { get; }
    internal string Title { get; } = "Internal Server Error";
    internal int Status { get; } = (int)HttpStatusCode.InternalServerError;
    internal string Type { get; } = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500"
    internal string Instance { get; }

    internal InternalServerErrorProblemDetails(string traceId, string instace)
    {
        TraceId = traceId;
        Instance = instace;
    }
}
