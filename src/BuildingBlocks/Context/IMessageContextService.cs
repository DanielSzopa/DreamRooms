namespace BuildingBlocks.Context;
public interface IMessageContextService
{
    MessageContext CreateNewContextWithValuesFromPreviousOne(Guid previousContextKey);

    void Set<TContext>(Guid key, TContext messageContext)
        where TContext : MessageContext;

    MessageContext Get(Guid messageId);
}
