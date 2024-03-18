using BuildingBlocks.Abstractions.Commands;
using BuildingBlocks.Events.Dispatcher;
using BuildingBlocks.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.UnitOfWork;

internal class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    private readonly ICommandHandler<TCommand> _decoratedCommandHandler;
    private readonly IServiceProvider _serviceProvider;
    private readonly UnitOfWorkTypeRegistery _unitOfWorkTypeRegistery;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;
    private readonly DbContextTypeRegistery _dbContextTypeRegistery;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> decoratedCommandHandler, IServiceProvider serviceProvider, UnitOfWorkTypeRegistery unitOfWorkTypeRegistery,
        IDomainEventsDispatcher domainEventsDispatcher, DbContextTypeRegistery dbContextTypeRegistery)
    {
        _decoratedCommandHandler = decoratedCommandHandler;
        _serviceProvider = serviceProvider;
        _unitOfWorkTypeRegistery = unitOfWorkTypeRegistery;
        _domainEventsDispatcher = domainEventsDispatcher;
        _dbContextTypeRegistery = dbContextTypeRegistery;
    }

    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        await _decoratedCommandHandler.HandleAsync(command, cancellationToken);

        var dbContextType = _dbContextTypeRegistery.Resolve<TCommand>();
        await _domainEventsDispatcher.DispatchAsync(dbContextType, cancellationToken);

        var unitOfWorkType = _unitOfWorkTypeRegistery.Resolve<TCommand>();
        if (unitOfWorkType is null)
            return;

        var unitOfWork = (IUnitOfWork)_serviceProvider.GetRequiredService(unitOfWorkType);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
