using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging;
internal class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;
    private readonly IContextAccessor _context;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, ILogger<LoggingCommandHandlerDecorator<TCommand>> logger, IContextAccessor context
)
    {
        _decoratedCommandHandler = decoratedCommandHandler;
        _logger = logger;
        _context = context;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var traceId = _context.TraceId;
        var correlationId = _context.CorrelationId;

        _logger.LogInformation("Handling a command {name} [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);
        await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command {name} [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);
    }
}
