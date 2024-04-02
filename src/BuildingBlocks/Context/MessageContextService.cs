using Microsoft.Extensions.Caching.Memory;

namespace BuildingBlocks.Context;
internal class MessageContextService : IMessageContextService
{
    private readonly IMemoryCache _cache;

    public MessageContextService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set<TContext>(Guid key, TContext messageContext) 
        where TContext : MessageContext
    {
        if (messageContext is null)
            return;

        _cache.Set(key, messageContext, TimeSpan.FromMinutes(2));
    }

    public MessageContext Get(Guid messageId)
    {
        return _cache.Get<MessageContext>(messageId);
    }
}
