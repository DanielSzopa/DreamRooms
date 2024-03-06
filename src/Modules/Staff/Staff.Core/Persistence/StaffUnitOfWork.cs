using BuildingBlocks.UnitOfWork;

namespace Staff.Core.Persistence;

internal class StaffUnitOfWork : SqlServerUnitOfWork<StaffDbContext>
{
    public StaffUnitOfWork(StaffDbContext staffDbContext) : base(staffDbContext)
    {
        
    }
}
