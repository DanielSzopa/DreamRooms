using BuildingBlocks.Abstractions.Commands;

namespace BuildingBlocks.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand;
}
