using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

internal sealed record FirstName
{
    internal string Value { get; }

    internal FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
            throw new InvalidFirstNameException(value);

        Value = value;
    }

    public static implicit operator FirstName(string value) => new(value);
    public static implicit operator string(FirstName value) => value.Value;
}
