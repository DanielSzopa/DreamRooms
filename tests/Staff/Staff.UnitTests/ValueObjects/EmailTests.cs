using BuildingBlocks.Domain.ValueObjects;
using Staff.Core.Domain.Exceptions;
using Staff.Core.Domain.ValueObjects;

namespace Staff.UnitTests.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    internal void CreateEmail_WhenValueIsEmpty_ShouldThrowInvalidEmailException(string value)
    {
        Assert.Throws<InvalidEmailException>(() =>
        {
            new Email(value);
        });
    }

    [Theory]
    [InlineData("test@gmail.com")]
    [InlineData("test1.23@GMAIL.COM")]
    [InlineData("test.test2@gmail.com")]
    [InlineData("0test012@onet.pl")]
    internal void CreateEmail_WhenValueIsValid_ShouldCreateEmail(string value)
    {
        //act
        var email = new Email(value);

        //arrange
        email.Value.Should().Be(value);
    }

    [Theory]
    [InlineData(".test@gmail.com")]
    [InlineData("test.@gmail.com")]
    [InlineData("tes#@gmail.com")]
    [InlineData("tes#@gmail.cm")]
    [InlineData("tes#@gmail.comm")]
    [InlineData("tes#@gmail.c")]
    [InlineData("test@gmail,c")]
    internal void CreateEmail_WhenValueIsInValid_ShouldThrowInvalidEmailException(string value)
    {
        Assert.Throws<InvalidEmailException>(() =>
        {
            new Email(value);
        });
    }
}
