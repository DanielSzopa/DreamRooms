using BuildingBlocks.Abstractions.Commands;
using FluentValidation;

namespace BuildingBlocks.Validators;
public class ValidatorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;
    private readonly IValidator<TCommand> _validator;

    public ValidatorCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IValidator<TCommand> validator)
    {
        _commandHandler = commandHandler;
        _validator = validator;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        if(_validator is null)
        {
            await _commandHandler.HandleAsync(command, cancellationToken);
        }

        _validator.ValidateAndThrow(command);

        await _commandHandler.HandleAsync(command, cancellationToken);
    }
}
