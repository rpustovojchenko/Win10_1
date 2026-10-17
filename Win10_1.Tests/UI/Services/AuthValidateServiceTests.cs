using System.Windows;
using Moq;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;
using Win10_1.UI.ViewModels.ModelsVM;

namespace Win10_1.Tests.UI.Services;

[Collection("WpfApplication")]
public class AuthValidateServiceTests
{
    private readonly Mock<IMessageService> _messageService = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IPasswordHasher> _passwordHasher = new();

    private readonly SignInValidator _signInValidator;
    private readonly SignUpValidator _signUpValidator;

    public AuthValidateServiceTests()
    {
        if (Application.Current is null)
            _ = new Application();

        _signInValidator = new SignInValidator(
            _userRepository.Object,
            _passwordHasher.Object);

        _signUpValidator = new SignUpValidator(
            _userRepository.Object);
    }

    private AuthValidateService CreateService() =>
        new(
            _messageService.Object,
            _signInValidator,
            _signUpValidator);

    private static UserVM CreateUser(
        string username = "ValidUser",
        string login = "validlogin") =>
        new()
        {
            Username = username,
            Login = login
        };

    private static PasswordRequirementsVM CreatePassword(
        string password = "Password1",
        string confirmPassword = "Password1") =>
        new()
        {
            Password = password,
            ConfirmPassword = confirmPassword
        };

    [Fact]
    public async Task CheckSignInForm_ReturnsTrue_WhenLoginAndPasswordAreValid()
    {
        var service = CreateService();

        var result = await service.CheckSignInForm(
            new object(),
            CreateUser(),
            CreatePassword());

        Assert.True(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckSignInForm_ShowsWarning_AndReturnsFalse_WhenLoginIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignInForm(
            new object(),
            CreateUser(login: ""),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignInForm_ShowsWarning_AndReturnsFalse_WhenLoginIsWhitespace()
    {
        var service = CreateService();

        var result = await service.CheckSignInForm(
            new object(),
            CreateUser(login: "   "),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignInForm_ShowsWarning_AndReturnsFalse_WhenPasswordIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignInForm(
            new object(),
            CreateUser(),
            CreatePassword(password: ""));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignInForm_ShowsError_AndReturnsFalse_WhenPasswordIsWeak()
    {
        var service = CreateService();

        var result = await service.CheckSignInForm(
            new object(),
            CreateUser(),
            CreatePassword(
                password: "weak",
                confirmPassword: "weak"));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ReturnsTrue_WhenAllFieldsAreValid()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(),
            CreatePassword());

        Assert.True(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsWarning_AndReturnsFalse_WhenUsernameIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(username: ""),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenUsernameIsInvalid()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(username: "User123"),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenUsernameIsTooLong()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(
                username: new string('A', UserConstraints.UsernameMaxLength + 1)),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsWarning_AndReturnsFalse_WhenLoginIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(login: ""),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenLoginIsInvalid()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(login: "invalid login!"),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenLoginIsTooLong()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(
                login: new string('a', UserConstraints.LoginMaxLength + 1)),
            CreatePassword());

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsWarning_AndReturnsFalse_WhenPasswordIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(),
            CreatePassword(password: ""));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenPasswordIsWeak()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(),
            CreatePassword(
                password: "weak",
                confirmPassword: "weak"));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsWarning_AndReturnsFalse_WhenConfirmPasswordIsEmpty()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(),
            CreatePassword(confirmPassword: ""));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckSignUpForm_ShowsError_AndReturnsFalse_WhenPasswordsDoNotMatch()
    {
        var service = CreateService();

        var result = await service.CheckSignUpForm(
            new object(),
            CreateUser(),
            CreatePassword(
                password: "Password1",
                confirmPassword: "Password2"));

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Once);
    }
}