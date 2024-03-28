using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Context;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Modules;
using FluentValidation;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Validators;

[Decorator]
internal class ValidatorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IValidator<TCommand> _validator;
    private readonly ILogger<ValidatorCommandHandlerDecorator<TCommand>> _logger;

    public ValidatorCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IValidator<TCommand> validator,
        ILogger<ValidatorCommandHandlerDecorator<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _validator = validator;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        if(_validator is null)
        {
            await _commandHandler.HandleAsync(command, cancellationToken);
        }

        var module = command.GetModuleName();
        var name = command.GetType().Name.Underscore();

        _logger.LogInformation("Validating a command {name} [Module: {module}]", name, module);
        _validator.ValidateAndThrow(command);
        _logger.LogInformation("Validation a command {name} passed [Module: {module}]", name, module);

        await _commandHandler.HandleAsync(command, cancellationToken);
    }
}
