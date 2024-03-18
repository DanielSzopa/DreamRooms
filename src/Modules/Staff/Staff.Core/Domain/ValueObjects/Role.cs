using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

internal sealed record Role
{
    internal static Role Owner = new Role(nameof(Owner));
    internal static Role Receptionist = new Role(nameof(Receptionist));
    internal static Role RoomServicer = new Role(nameof(RoomServicer));

    internal string Value { get; }
    internal Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidRoleException();

        Value = value;
    }

    public static implicit operator Role(string value) => new Role(value);
    public static implicit operator string(Role role) => role.Value;
}
