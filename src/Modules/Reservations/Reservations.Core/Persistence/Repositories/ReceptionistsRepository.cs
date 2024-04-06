using Reservations.Core.Domain.Entities;
using Reservations.Core.Domain.Repositories;

namespace Reservations.Core.Persistence.Repositories;
internal class ReceptionistsRepository : IReceptionistsRepository
{
    private readonly ReservationsDbContext _dbContext;

    public ReceptionistsRepository(ReservationsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Receptionist receptionist, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(receptionist, cancellationToken);
    }
}
