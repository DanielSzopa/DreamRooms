using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;
using Serilog.Context;

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
        var commandName = command.GetType().Name.Underscore();

        using (LogContext.Push(new ModuleEnricher(command.GetModuleName())))
        {
            _logger.LogInformation("Handling a command: {Command}", commandName);
            await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
            _logger.LogInformation("Handled a command: {Command}", commandName);
        }
    }
}
