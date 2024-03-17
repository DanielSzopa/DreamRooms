using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Modules;
using FluentValidation;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Validators;
public class ValidatorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IValidator<TCommand> _validator;
    private readonly ILogger<ValidatorCommandHandlerDecorator<TCommand>> _logger;
    private readonly IContextAccessor _context;

    public ValidatorCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IValidator<TCommand> validator,
        ILogger<ValidatorCommandHandlerDecorator<TCommand>> logger, IContextAccessor context)
    {
        _commandHandler = commandHandler;
        _validator = validator;
        _logger = logger;
        _context = context;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        if(_validator is null)
        {
            await _commandHandler.HandleAsync(command, cancellationToken);
        }

        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();
        var traceId = _context.TraceId;
        var correlationId = _context.CorrelationId;

        _logger.LogInformation("Validating a command {name} [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);
        _validator.ValidateAndThrow(command);
        _logger.LogInformation("Validation a command {name} passed [Module: {module}, TraceId: {traceId}, CorrelationId: {correlationId}]", name, module, traceId, correlationId);

        await _commandHandler.HandleAsync(command, cancellationToken);
    }
}
