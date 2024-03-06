namespace BuildingBlocks.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
