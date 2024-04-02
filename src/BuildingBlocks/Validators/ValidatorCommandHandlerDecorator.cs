using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Helpers.Decorators;
using BuildingBlocks.Logging.Enrichers;
using BuildingBlocks.Modules;
using FluentValidation;
using Humanizer;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace BuildingBlocks.Validators;

[Decorator]
internal class ValidatorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ValidatorCommandHandlerDecorator<TCommand>> _logger;

    public ValidatorCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IServiceProvider serviceProvider,
        ILogger<ValidatorCommandHandlerDecorator<TCommand>> logger)
    {
        _commandHandler = commandHandler;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var validator = (IValidator<TCommand>)_serviceProvider.GetService(typeof(IValidator<TCommand>));
        if(validator is null)
        {
            await _commandHandler.HandleAsync(command, cancellationToken);
            return;
        }

        var commandName = command.GetType().Name.Underscore();

        using(LogContext.Push(new ModuleEnricher(command.GetModuleName())))
        {
            _logger.LogInformation("Validating a Command: {command}", commandName);
            validator.ValidateAndThrow(command);
            _logger.LogInformation("Validation a Command: {command} passed", commandName);
        }
        
        await _commandHandler.HandleAsync(command, cancellationToken);
    }
}
