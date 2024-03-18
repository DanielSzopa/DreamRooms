using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

internal record LastName
{
    internal string Value { get; }

    internal LastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2)
            throw new InvalidLastNameException(lastName);

        Value = lastName;
    }

    public static implicit operator LastName(string lastName) => new(lastName);
    public static implicit operator string(LastName lastName) => lastName.Value;
}
