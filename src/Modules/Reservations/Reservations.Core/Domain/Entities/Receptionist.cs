using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.ValueObjects;
using Reservations.Core.Domain.ValueObjects;

namespace Reservations.Core.Domain.Entities;
internal class Receptionist : Entity, IAgregateRoot
{
    internal EmployeeId EmployeeId { get; }
    internal Email Email { get; }
    internal FullName FullName { get; }

    private Receptionist(EmployeeId employeeId, FullName fullName, Email email)
    {
        EmployeeId = employeeId;
        FullName = fullName;
        Email = email;
    }

    internal static Receptionist Create(EmployeeId employeeId, FullName fullName, Email email)
    {
        return new Receptionist(employeeId, fullName, email);
    }
}
