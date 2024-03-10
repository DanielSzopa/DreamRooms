using System.Text.Json.Serialization;

namespace BuildingBlocks.Middlewares.ProblemDetails;

internal class DreamRoomsExProblemDetails
{
    [JsonInclude]
    internal string Message { get;}
    [JsonInclude]
    internal string TraceId { get; }
    internal DreamRoomsExProblemDetails(string traceId, string message)
    {
        TraceId = traceId;
        Message = message;
    }
}
