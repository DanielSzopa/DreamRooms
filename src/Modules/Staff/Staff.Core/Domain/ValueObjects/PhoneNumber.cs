using Staff.Core.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Staff.Core.Domain.ValueObjects;

public record PhoneNumber
{
    public string Value { get; }

    public PhoneNumber(string phoneNumber)
    {
        var regex = new Regex(@"^(\+\d{2}\s?)?\d{3}\s?\d{3}\s?\d{3}$");

        if (string.IsNullOrEmpty(phoneNumber) || !regex.IsMatch(phoneNumber))
            throw new InvalidPhoneNumberException(phoneNumber);

        Value = phoneNumber;
    }

    public static implicit operator PhoneNumber(string phoneNumber) => new(phoneNumber);
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}
