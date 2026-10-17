using Moq;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Models;
using Win10_1.UI.Services;
using Win10_1.UI.Services.Interfaces;

namespace Win10_1.Tests.UI.Services;

public class TaskManagerValidateServiceTests
{
    private readonly Mock<IMessageService> _messageService = new();

    private readonly ProjectValidator _projectValidator;
    private readonly TaskManagerValidateService _service;

    public TaskManagerValidateServiceTests()
    {
        _projectValidator = new ProjectValidator();

        _service = new TaskManagerValidateService(
            _messageService.Object,
            _projectValidator);
    }

    private static Project CreateProject(
        string projectName = "Valid Project",
        string? description = null,
        string? theme = null) =>
        new()
        {
            ProjectName = projectName,
            Description = description,
            Theme = theme
        };

    [Fact]
    public async Task CheckNewProjectForm_ReturnsTrue_WhenProjectIsValid()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "Valid Project",
            description: "Valid description",
            theme: "Dark");

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.True(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Never);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckNewProjectForm_ReturnsTrue_WhenOptionalFieldsAreNull()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "Valid Project",
            description: null,
            theme: null);

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.True(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                It.IsAny<object>(),
                It.IsAny<string>()),
            Times.Never);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckNewProjectForm_ReturnsFalse_AndShowsWarning_WhenProjectNameIsInvalid()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "",
            description: "Valid description",
            theme: "Dark");

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                context,
                It.IsAny<string>()),
            Times.Once);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckNewProjectForm_ReturnsFalse_AndShowsError_WhenProjectNameIsTooLong()
    {
        var context = new object();
        var project = CreateProject(
            projectName: new string(
                'a',
                ProjectConstraints.ProjectNameMaxLength + 1),
            description: "Valid description",
            theme: "Dark");

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckNewProjectForm_ReturnsFalse_AndShowsError_WhenDescriptionIsTooLong()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "Valid Project",
            description: new string(
                'a',
                ProjectConstraints.DescriptionMaxLength + 1),
            theme: "Dark");

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckNewProjectForm_ReturnsFalse_AndShowsError_WhenThemeIsTooLong()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "Valid Project",
            description: "Valid description",
            theme: new string(
                'a',
                ProjectConstraints.ThemeMaxLength + 1));

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckNewProjectForm_StopsAfterProjectNameError()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "",
            description: new string(
                'a',
                ProjectConstraints.DescriptionMaxLength + 1),
            theme: new string(
                'a',
                ProjectConstraints.ThemeMaxLength + 1));

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowWarningAsync(
                context,
                It.IsAny<string>()),
            Times.Once);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                It.IsAny<object>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public async Task CheckNewProjectForm_StopsAfterDescriptionError()
    {
        var context = new object();
        var project = CreateProject(
            projectName: "Valid Project",
            description: new string(
                'a',
                ProjectConstraints.DescriptionMaxLength + 1),
            theme: new string(
                'a',
                ProjectConstraints.ThemeMaxLength + 1));

        var result = await _service.CheckNewProjectForm(
            context,
            project);

        Assert.False(result);

        _messageService.Verify(
            ms => ms.ShowErrorAsync(
                context,
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once);
    }
}