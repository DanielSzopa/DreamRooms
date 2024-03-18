using BuildingBlocks.Domain.ValueObjects;

namespace Staff.Core.Domain.ValueObjects;

internal record EmployeeId : TypedIdValueBase
{
    internal EmployeeId(Guid id) : base(id)
    {
    }

    public static implicit operator EmployeeId(Guid id) => new(id);
    public static implicit operator Guid(EmployeeId id) => id.Value;
}
