using Staff.Core.Domain.Entities;

namespace Staff.Core.Domain.Repositories;

internal interface IEmployeeRepository
{
    Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);
}
