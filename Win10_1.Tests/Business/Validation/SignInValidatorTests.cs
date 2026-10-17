using Moq;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Models;

namespace Win10_1.Tests.Business.Validation;

public class SignInValidatorTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly SignInValidator _validator;

    public SignInValidatorTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();

        _validator = new SignInValidator(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object);
    }

    private static User CreateUser(
        string login = "test_login",
        string hashPassword = "$2a$11$storedHash")
    {
        return new User
        {
            Login = login,
            HashPassword = hashPassword
        };
    }

    [Fact]
    public async Task CanSignInAsync_ReturnsTrue_WhenUserExistsAndPasswordIsValid()
    {
        var user = CreateUser();
        const string password = "Password123!";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(hasher => hasher.VerifyPassword(password, user.HashPassword))
            .Returns(true);

        var result = await _validator.CanSignInAsync(user, password);

        Assert.True(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(user.Login),
            Times.Once);

        _passwordHasherMock.Verify(
            hasher => hasher.VerifyPassword(password, user.HashPassword),
            Times.Once);
    }

    [Fact]
    public async Task CanSignInAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        var user = CreateUser(login: "missing_login");
        const string password = "Password123!";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync((User?)null);

        var result = await _validator.CanSignInAsync(user, password);

        Assert.False(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(user.Login),
            Times.Once);

        _passwordHasherMock.Verify(
            hasher => hasher.VerifyPassword(
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CanSignInAsync_ReturnsFalse_WhenPasswordIsInvalid()
    {
        var user = CreateUser();
        const string password = "WrongPassword123!";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(hasher => hasher.VerifyPassword(password, user.HashPassword))
            .Returns(false);

        var result = await _validator.CanSignInAsync(user, password);

        Assert.False(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(user.Login),
            Times.Once);

        _passwordHasherMock.Verify(
            hasher => hasher.VerifyPassword(password, user.HashPassword),
            Times.Once);
    }

    [Fact]
    public async Task CanSignInAsync_PassesStoredHash_ToPasswordHasher()
    {
        var user = CreateUser(
            login: "stored_hash_login",
            hashPassword: "$2a$11$exactStoredHash");

        const string password = "Password123!";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(hasher => hasher.VerifyPassword(password, user.HashPassword))
            .Returns(true);

        await _validator.CanSignInAsync(user, password);

        _passwordHasherMock.Verify(
            hasher => hasher.VerifyPassword(password, "$2a$11$exactStoredHash"),
            Times.Once);
    }

    [Fact]
    public async Task CanSignInAsync_DoesNotVerifyPassword_WhenRepositoryThrows()
    {
        var user = CreateUser();
        const string password = "Password123!";

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ThrowsAsync(new InvalidOperationException("Database unavailable"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _validator.CanSignInAsync(user, password));

        _passwordHasherMock.Verify(
            hasher => hasher.VerifyPassword(
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }
}
