namespace BuildingBlocks.Context;
public class MessageContext
{
    public Guid CorrelationId { get; }
    public MessageContext(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
}
