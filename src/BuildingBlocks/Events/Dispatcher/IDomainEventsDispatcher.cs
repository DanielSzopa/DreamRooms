namespace BuildingBlocks.Events.Dispatcher;
public interface IDomainEventsDispatcher
{
    Task DispatchAsync(Type dbContextType, CancellationToken cancellationToken = default);
}
