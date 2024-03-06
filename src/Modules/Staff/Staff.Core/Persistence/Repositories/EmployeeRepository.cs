using Staff.Core.Domain.Entities;
using Staff.Core.Domain.Repositories;

namespace Staff.Core.Persistence.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly StaffDbContext _staffDbContext;

    public EmployeeRepository(StaffDbContext staffDbContext)
    {
        _staffDbContext = staffDbContext;
    }

    public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _staffDbContext.AddAsync(employee, cancellationToken);
    }
}
