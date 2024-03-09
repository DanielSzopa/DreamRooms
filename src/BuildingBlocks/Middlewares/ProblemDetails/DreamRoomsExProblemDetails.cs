namespace BuildingBlocks.Middlewares.ProblemDetails;

internal class DreamRoomsExProblemDetails
{
    internal string TraceId { get; }
    internal string Message { get;}
    internal DreamRoomsExProblemDetails(string traceId, string message)
    {
        TraceId = traceId;
        Message = message;
    }
}
