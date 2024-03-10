using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Middlewares.ProblemDetails;

internal class InternalServerErrorProblemDetails
{
    [JsonInclude]
    internal string Title { get; } = "Internal Server Error";
    [JsonInclude]
    internal string TraceId { get; }
    [JsonInclude]
    internal int Status { get; } = (int)HttpStatusCode.InternalServerError;
    [JsonInclude]
    internal string Type { get; } = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500";
    [JsonInclude]
    internal string Instance { get; }

    internal InternalServerErrorProblemDetails(string traceId, string instace)
    {
        TraceId = traceId;
        Instance = instace;
    }
}
