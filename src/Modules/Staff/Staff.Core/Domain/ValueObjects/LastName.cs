using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

public record LastName
{
    public string Value { get; }

    public LastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2)
            throw new InvalidLastNameException(lastName);

        Value = lastName;
    }

    public static implicit operator LastName(string lastName) => new(lastName);
    public static implicit operator string(LastName lastName) => lastName.Value;
}
