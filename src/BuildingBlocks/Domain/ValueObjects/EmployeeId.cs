namespace BuildingBlocks.Domain.ValueObjects;

public record EmployeeId : TypedIdValueBase
{
    public EmployeeId(Guid id) : base(id)
    {
    }

    public static implicit operator EmployeeId(Guid id) => new(id);
    public static implicit operator Guid(EmployeeId id) => id.Value;
}
