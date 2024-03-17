namespace BuildingBlocks.Context;
public interface IContextAccessor
{
    Guid CorrelationId { get; }
    string TraceId { get; }
}
