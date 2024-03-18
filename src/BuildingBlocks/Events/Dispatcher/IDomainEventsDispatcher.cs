namespace BuildingBlocks.Events.Dispatcher;
internal interface IDomainEventsDispatcher
{
    Task DispatchAsync(Type dbContextType, CancellationToken cancellationToken = default);
}
