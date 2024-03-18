using Staff.Core.Domain.Exceptions;

namespace Staff.Core.Domain.ValueObjects;

internal record Password
{
    internal string Value { get; }

    internal Password(string password)
    {
        if (string.IsNullOrWhiteSpace(password) ||
            password.Length < 8 || password.Length > 20)
            throw new InvalidPasswordException();

        Value = password;
    }

    public static implicit operator Password(string password) => new(password);
    public static implicit operator string(Password password) => password.Value;
}
