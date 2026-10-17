using Moq;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Models;

namespace Win10_1.Tests.Business.Validation;

public class SignUpValidatorTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly SignUpValidator _validator;

    public SignUpValidatorTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _validator = new SignUpValidator(_userRepositoryMock.Object);
    }

    private static User CreateUser(string login = "new_login")
    {
        return new User
        {
            Login = login
        };
    }

    [Fact]
    public async Task CanSignUpAsync_ReturnsTrue_WhenLoginDoesNotExist()
    {
        var user = CreateUser(login: "new_login");

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync((User?)null);

        var result = await _validator.CanSignUpAsync(user);

        Assert.True(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(user.Login),
            Times.Once);
    }

    [Fact]
    public async Task CanSignUpAsync_ReturnsFalse_WhenLoginAlreadyExists()
    {
        var user = CreateUser(login: "existing_login");
        var existingUser = CreateUser(login: "existing_login");

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ReturnsAsync(existingUser);

        var result = await _validator.CanSignUpAsync(user);

        Assert.False(result);

        _userRepositoryMock.Verify(
            repo => repo.GetByLoginAsync(user.Login),
            Times.Once);
    }

    [Fact]
    public async Task CanSignUpAsync_ThrowsException_WhenRepositoryThrows()
    {
        var user = CreateUser(login: "database_error_login");

        _userRepositoryMock
            .Setup(repo => repo.GetByLoginAsync(user.Login))
            .ThrowsAsync(new InvalidOperationException("Database unavailable"));

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _validator.CanSignUpAsync(user));
    }
}
