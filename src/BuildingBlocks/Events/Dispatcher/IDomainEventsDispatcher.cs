using BuildingBlocks.Domain.Events.Abstractions;

namespace BuildingBlocks.Events.Dispatcher;
public interface IDomainEventsDispatcher
{
    Task DispatchAsync(CancellationToken cancellationToken = default);
}
