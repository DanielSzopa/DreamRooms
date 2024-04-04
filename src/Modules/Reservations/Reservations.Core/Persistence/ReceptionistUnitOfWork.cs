using BuildingBlocks.UnitOfWork;

namespace Reservations.Core.Persistence;
internal class ReservationsUnitOfWork : SqlServerUnitOfWork<ReservationsDbContext>
{
    public ReservationsUnitOfWork(ReservationsDbContext dbContext) : base(dbContext)
    {
    }
}
