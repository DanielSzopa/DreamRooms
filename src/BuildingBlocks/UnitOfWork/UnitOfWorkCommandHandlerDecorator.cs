using BuildingBlocks.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.UnitOfWork;

public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly UnitOfWorkTypeRegistery _unitOfWorkTypeRegistery;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, IServiceProvider serviceProvider, UnitOfWorkTypeRegistery unitOfWorkTypeRegistery)
    {
        _decoratedCommandHandler = decoratedCommandHandler;
        _serviceProvider = serviceProvider;
        _unitOfWorkTypeRegistery = unitOfWorkTypeRegistery;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        await _decoratedCommandHandler.HandleAsync(command, cancellationToken);

        var unitOfWorkType = _unitOfWorkTypeRegistery.Resolve<TCommand>();
        if (unitOfWorkType is null)
            return;

        var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
