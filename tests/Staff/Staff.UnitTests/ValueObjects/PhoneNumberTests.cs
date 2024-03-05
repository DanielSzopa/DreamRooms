using Staff.Core.Domain.Exceptions;
using Staff.Core.Domain.ValueObjects;

namespace Staff.UnitTests.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    internal void CreatePhoneNumber_WhenValueIsEmpty_ShouldThrowInvalidPhoneNumberException(string value)
    {
        Assert.Throws<InvalidPhoneNumberException>(() =>
        {
            new PhoneNumber(value);
        });
    }

    [Theory]
    [InlineData("+49535535535")]
    [InlineData("+49 535 535 535")]
    [InlineData("+48 535 535 535")]
    [InlineData("535 535 535")]
    [InlineData("535535535")]
    internal void CreatePhoneNumber_WhenValueIsValid_ShouldCreatePhoneNumber(string value)
    {
        //act
        var phoneNumber = new PhoneNumber(value);

        //arrange
        phoneNumber.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("49535535535")]
    [InlineData("+49 535 535 53")]
    [InlineData("49 535 535 533")]
    [InlineData("+48 535 535 5359")]
    [InlineData("535 535 5359")]
    [InlineData("5355355359")]
    [InlineData("53553553")]
    internal void CreatePhoneNumber_WhenValueIsInValid_ShouldThrowInvalidPhoneNumberException(string value)
    {
        Assert.Throws<InvalidPhoneNumberException>(() =>
        {
            new PhoneNumber(value);
        });
    }
}
