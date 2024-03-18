using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

internal record PasswordHash
{
    internal string Value { get; }

    internal PasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidPasswordHashException();

        Value = passwordHash;
    }

    public static implicit operator PasswordHash(string passwordHash) => new(passwordHash);
    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
}
