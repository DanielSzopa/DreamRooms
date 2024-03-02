using Staff.Core.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Staff.Core.Domain.ValueObjects;

public record PhoneNumber
{
    private readonly Regex _regex = new Regex(@"^(\\+\\d{2}\\s?)?\\d{3}\\s?\\d{3}\\s?\\d{3}$");

    public string Value { get; }

    public PhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber) || _regex.IsMatch(phoneNumber))
            throw new InvalidPhoneNumberException(phoneNumber);

        Value = phoneNumber;
    }

    public static implicit operator PhoneNumber(string phoneNumber) => new(phoneNumber);
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}
