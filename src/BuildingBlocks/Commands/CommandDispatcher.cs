using BuildingBlocks.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Commands;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var commandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await commandHandler.HandleAsync(command, cancellationToken);
    }
}
