using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

public sealed record Role
{
    public static Role Owner = new Role(nameof(Owner));
    public static Role Receptionist = new Role(nameof(Receptionist));
    public static Role RoomServicer = new Role(nameof(RoomServicer));

    public string Value { get; }
    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidRoleException();

        Value = value;
    }

    public static implicit operator Role(string value) => new Role(value);
    public static implicit operator string(Role role) => role.Value;
}
