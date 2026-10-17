using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;

namespace Win10_1.Tests.Business.Validation;

public class UserValidatorTests
{
    [Theory]
    [InlineData("Ivan", true)]
    [InlineData("Иван", true)]
    [InlineData("Élodie", true)]
    [InlineData("Ivan Petrov", false)]
    [InlineData("Ivan123", false)]
    [InlineData("Ivan!", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(null, false)]
    public void IsUsernameValid_ReturnsExpectedResult(string? username, bool expected)
    {
        var result = UserValidator.IsUsernameValid(username);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsUsernameLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var username = new string('a', UserConstraints.UsernameMaxLength);

        var result = UserValidator.IsUsernameLengthValid(username);

        Assert.True(result);
    }

    [Fact]
    public void IsUsernameLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var username = new string(
            'a',
            UserConstraints.UsernameMaxLength + 1);

        var result = UserValidator.IsUsernameLengthValid(username);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public void IsUsernameLengthValid_HandlesNullOrShortUsername(string? username, bool expected)
    {
        var result = UserValidator.IsUsernameLengthValid(username);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("ivan", true)]
    [InlineData("Ivan", true)]
    [InlineData("ivan123", true)]
    [InlineData("IVAN123", true)]
    [InlineData("ivan_petrov", false)]
    [InlineData("ivan-petrov", false)]
    [InlineData("ivan petrov", false)]
    [InlineData("иван", false)]
    [InlineData("ivan!", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData(null, false)]
    public void IsLoginValid_ReturnsExpectedResult(string? login, bool expected)
    {
        var result = UserValidator.IsLoginValid(login);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsLoginLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var login = new string('a', UserConstraints.LoginMaxLength);

        var result = UserValidator.IsLoginLengthValid(login);

        Assert.True(result);
    }

    [Fact]
    public void IsLoginLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var login = new string('a', UserConstraints.LoginMaxLength + 1);

        var result = UserValidator.IsLoginLengthValid(login);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public void IsLoginLengthValid_HandlesNullOrShortLogin(string? login, bool expected)
    {
        var result = UserValidator.IsLoginLengthValid(login);

        Assert.Equal(expected, result);
    }
}
