using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging;

[Decorator]
internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    {
        _decoratedCommandHandler = decoratedCommandHandler;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();

        _logger.LogInformation("Handling a command {name} [Module: {module}]", name, module);
        await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command {name} [Module: {module}]", name, module);
    }
}
