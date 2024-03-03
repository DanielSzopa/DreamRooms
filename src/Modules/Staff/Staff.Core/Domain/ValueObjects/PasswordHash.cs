using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

public record PasswordHash
{
    public string Value { get; }

    public PasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new InvalidPasswordHashException();

        Value = passwordHash;
    }

    public static implicit operator PasswordHash(string passwordHash) => new(passwordHash);
    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
}
