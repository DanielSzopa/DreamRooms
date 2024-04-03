using Reservations.Core.Domain.Exceptions;

namespace Reservations.Core.Domain.ValueObjects;
internal record FullName
{
    internal string Value { get; }

    internal FullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 4)
        {
            throw new InvalidFullNameException(fullName);
        }

        Value = fullName;
    }

    public static implicit operator FullName(string fullName) => new FullName(fullName);
    public static implicit operator string(FullName fullName) => fullName.Value;
}
