using Moq;
using Win10_1.Business.Data.Repository.Interfaces;
using Win10_1.Business.Services.Interfaces;
using Win10_1.Business.Validation;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.Tests.UI.Services;

public class ProfileValidateServiceTests
{
    private readonly Mock<IPasswordHasher> _passwordHasher = new();
    private readonly Mock<IMessageService> _messageService = new();
    private readonly Mock<IUserRepository> _userRepository = new();

    private readonly ProfileValidator _profileValidator;
    private readonly ProfileValidateService _service;

    public ProfileValidateServiceTests()
    {
        _profileValidator = new ProfileValidator(
            _userRepository.Object);

        _service = new ProfileValidateService(
            _passwordHasher.Object,
            _messageService.Object,
            _profileValidator);
    }

    [Fact]
    public async Task CheckUsername_ReturnsFalse_WithoutMessage_WhenUsernameIsNullOrEmpty()
    {
        var context = new object();

        var nullResult = await _service.CheckUsername(
            context,
            null,
            "OldUser");

        var emptyResult = await _service.CheckUsername(
            context,
            "",
            "OldUser");

        var whitespaceResult = await _service.CheckUsername(
            context,
            "   ",
            "OldUser");

        Assert.False(nullResult);
        Assert.False(emptyResult);
        Assert.False(whitespaceResult);

        _messageService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CheckUsername_ReturnsFalse_AndShowsError_WhenUsernameIsUnchanged()
    {
        var context = new object();

        var result = await _service.CheckUsername(
            context,
            "OldUser",
            "OldUser");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckLogin_ReturnsFalse_WithoutMessage_WhenLoginIsNullOrEmpty()
    {
        var context = new object();

        var nullResult = await _service.CheckLogin(
            context,
            null,
            "OldLogin");

        var emptyResult = await _service.CheckLogin(
            context,
            "",
            "OldLogin");

        Assert.False(nullResult);
        Assert.False(emptyResult);

        _messageService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CheckLogin_ReturnsFalse_AndShowsError_WhenLoginIsUnchanged()
    {
        var context = new object();

        var result = await _service.CheckLogin(
            context,
            "OldLogin",
            "OldLogin");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckPassword_ReturnsFalse_WithoutMessage_WhenPasswordIsNullOrWhitespace()
    {
        var context = new object();

        var nullResult = await _service.CheckPassword(
            context,
            null,
            "OldHash");

        var emptyResult = await _service.CheckPassword(
            context,
            "",
            "OldHash");

        var whitespaceResult = await _service.CheckPassword(
            context,
            "   ",
            "OldHash");

        Assert.False(nullResult);
        Assert.False(emptyResult);
        Assert.False(whitespaceResult);

        _messageService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CheckPassword_ReturnsFalse_AndShowsError_WhenPasswordIsUnchanged()
    {
        var context = new object();

        _passwordHasher
            .Setup(h => h.VerifyPassword(
                "SamePassword",
                "OldHash"))
            .Returns(true);

        var result = await _service.CheckPassword(
            context,
            "SamePassword",
            "OldHash");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckPassword_DoesNotShowUnchangedMessage_WhenPasswordIsDifferent()
    {
        var context = new object();

        _passwordHasher
            .Setup(h => h.VerifyPassword(
                "NewPassword",
                "OldHash"))
            .Returns(false);

        await _service.CheckPassword(
            context,
            "NewPassword",
            "OldHash");

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckConfirmPassword_ReturnsFalse_AndShowsInfo_WhenConfirmPasswordIsEmpty()
    {
        var context = new object();

        var result = await _service.CheckConfirmPassword(
            context,
            "NewPassword",
            "");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowInfoAsync(
                context,
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckConfirmPassword_ReturnsFalse_AndShowsInfo_WhenConfirmPasswordIsWhitespace()
    {
        var context = new object();

        var result = await _service.CheckConfirmPassword(
            context,
            "NewPassword",
            "   ");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowInfoAsync(
                context,
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckConfirmPassword_ReturnsFalse_AndShowsInfo_WhenPasswordIsEmpty()
    {
        var context = new object();

        var result = await _service.CheckConfirmPassword(
            context,
            "",
            "NewPassword");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowInfoAsync(
                context,
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckConfirmPassword_ReturnsFalse_AndShowsInfo_WhenPasswordIsWhitespace()
    {
        var context = new object();

        var result = await _service.CheckConfirmPassword(
            context,
            "   ",
            "NewPassword");

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowInfoAsync(
                context,
                It.IsAny<string>()),
            Times.Once);
    }
}