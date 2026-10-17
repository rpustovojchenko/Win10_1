using Moq;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;

namespace Win10_1.Tests.Business.Validation;

public class ProfileValidatorTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly ProfileValidator _validator;

    public ProfileValidatorTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _validator = new ProfileValidator(_userRepositoryMock.Object);
    }

    [Theory]
    [InlineData("Ivan", true)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("Ivan123", false)]
    public async Task IsUsernameValid_ReturnsExpectedResult(
        string username,
        bool expected)
    {
        var result = await _validator.IsUsernameValid(username);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Ivan", true)]
    [InlineData("", true)]
    [InlineData("IvanIvanIvan", true)]
    public async Task IsUsernameLengthValid_ReturnsExpectedResult(
        string username,
        bool expected)
    {
        var result = await _validator.IsUsernameLengthValid(username);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Ivan", true)]
    [InlineData("Ivan123", true)]
    [InlineData("", false)]
    [InlineData("ivan login", false)]
    [InlineData("иван", false)]
    [InlineData("ivan_login", false)]
    public async Task IsLoginValid_ReturnsExpectedResult(
        string login,
        bool expected)
    {
        var result = await _validator.IsLoginValid(login);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Ivan", true)]
    [InlineData("", true)]
    [InlineData("Ivan123", true)]
    [InlineData("IvanIvanIvan", true)]
    public async Task IsLoginLengthValid_ReturnsExpectedResult(
        string login,
        bool expected)
    {
        var result = await _validator.IsLoginLengthValid(login);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsLoginExists_ReturnsTrue_WhenUserIsFound()
    {
        const string login = "existinglogin";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(login))
            .ReturnsAsync(new User { Login = login });

        var result = await _validator.IsLoginExists(login);

        Assert.True(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(login),
            Times.Once);
    }

    [Fact]
    public async Task IsLoginExists_ReturnsFalse_WhenUserIsNotFound()
    {
        const string login = "missinglogin";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(login))
            .ReturnsAsync((User?)null);

        var result = await _validator.IsLoginExists(login);

        Assert.False(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(login),
            Times.Once);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("abc", false)]
    [InlineData("abcdef", false)]
    [InlineData("abcdef1", false)]
    [InlineData("abcdef!", false)]
    [InlineData("abcdefg1", true)]
    [InlineData("abcdefg!", true)]
    [InlineData("Абвгде1", false)]
    [InlineData("abcdefАбв1", false)]
    public async Task IsPasswordValid_ReturnsExpectedResult(
        string? password,
        bool expected)
    {
        var result = await _validator.IsPasswordValid(password!);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsPasswordValid_ReturnsTrue_WhenLengthEqualsMinLength()
    {
        var password = new string('a', UserConstraints.PasswordMinLength - 1) + "1";

        var result = await _validator.IsPasswordValid(password);

        Assert.True(result);
    }

    [Fact]
    public async Task IsPasswordValid_ReturnsFalse_WhenLengthIsLessThanMinLength()
    {
        var password = new string('a', UserConstraints.PasswordMinLength - 2) + "1";

        var result = await _validator.IsPasswordValid(password);

        Assert.False(result);
    }

    [Theory]
    [InlineData("Password123!", "Password123!", true)]
    [InlineData("Password123!", "password123!", false)]
    [InlineData("Password123!", "", false)]
    [InlineData(null, "Password123!", false)]
    [InlineData("Password123!", null, false)]
    [InlineData(null, null, true)]
    public async Task IsConfirmPasswordValid_ReturnsExpectedResult(
        string? password,
        string? confirmPassword,
        bool expected)
    {
        var result = await _validator.IsConfirmPasswordValid(
            password,
            confirmPassword);

        Assert.Equal(expected, result);
    }
}