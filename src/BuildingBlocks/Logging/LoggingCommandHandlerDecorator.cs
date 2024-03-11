using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Modules;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging;
public class LoggingCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;
    private readonly ILogger<LoggingCommandHandlerDecorator<TCommand>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggingCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, ILogger<LoggingCommandHandlerDecorator<TCommand>> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _decoratedCommandHandler = decoratedCommandHandler;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var traceId = _httpContextAccessor.HttpContext.TraceIdentifier;

        _logger.LogInformation("Handling a command {name} [Module: {module}, TraceId: {traceId}]", name, module, traceId);
        await _decoratedCommandHandler.HandleAsync(command, cancellationToken);
        _logger.LogInformation("Handled a command {name} [Module: {module}, TraceId: {traceId}]", name, module, traceId);
    }
}
