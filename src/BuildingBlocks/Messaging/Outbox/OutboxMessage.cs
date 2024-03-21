namespace BuildingBlocks.Messaging.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public Guid CorrelationId { get; set; }
    public string TraceId { get; set; }
    public string Data { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ProcessedAt { get; set; }
}
