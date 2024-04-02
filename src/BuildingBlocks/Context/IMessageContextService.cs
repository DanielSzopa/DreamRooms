namespace BuildingBlocks.Context;
public interface IMessageContextService
{
    void Set<TContext>(Guid key, TContext messageContext)
        where TContext : MessageContext;

    MessageContext Get(Guid messageId);
}
