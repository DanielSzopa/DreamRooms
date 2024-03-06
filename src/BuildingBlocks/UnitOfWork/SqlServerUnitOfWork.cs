using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.UnitOfWork;

public class SqlServerUnitOfWork<T> : IUnitOfWork
    where T : DbContext
{
    private readonly T _dbContext;

    public SqlServerUnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
