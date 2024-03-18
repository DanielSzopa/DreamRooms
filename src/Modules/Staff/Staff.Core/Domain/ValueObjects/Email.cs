using Staff.Core.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Staff.Core.Domain.ValueObjects;

internal record Email
{
    internal string Value { get; }

    internal Email(string email)
    {
        var regex = new Regex(@"^[a-z0-9]+\.?[a-z0-9]+@[a-z]+\.[a-z]{2,3}$", RegexOptions.IgnoreCase);

        if (string.IsNullOrWhiteSpace(email) || !regex.IsMatch(email))
            throw new InvalidEmailException(email);

        Value = email;
    }

    public static implicit operator Email(string email) => new(email);
    public static implicit operator string(Email email) => email.Value;
}
