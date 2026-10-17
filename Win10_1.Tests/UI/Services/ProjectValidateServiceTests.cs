using System.Windows;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services;

namespace Win10_1.Tests.UI.Services;

[Collection("WpfApplication")]
public class ProjectValidateServiceTests
{
    private readonly ProjectValidator _projectValidator = new();
    private readonly ProjectValidateService _service;

    public ProjectValidateServiceTests()
    {
        if (Application.Current is null)
            _ = new Application();

        _service = new ProjectValidateService(_projectValidator);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task CheckProjectName_ReturnsRequiredMessage_WhenNameIsInvalid(
        string? name)
    {
        var result = await _service.CheckProjectName(name!);

        var expected = LocalizationService.GetString(
            LocalizationKeys.ProjectNameRequired);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckProjectName_ReturnsMaxLengthMessage_WhenNameIsTooLong()
    {
        var tooLongName = new string(
            'a',
            ProjectConstraints.ProjectNameMaxLength + 1);

        var result = await _service.CheckProjectName(tooLongName);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.ProjectNameMaxLength),
            ProjectConstraints.ProjectNameMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckProjectName_ReturnsNull_WhenNameIsValid()
    {
        var result = await _service.CheckProjectName("Valid Project Name");

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckProjectName_ReturnsNull_WhenNameLengthEqualsMaxLength()
    {
        var name = new string(
            'a',
            ProjectConstraints.ProjectNameMaxLength);

        var result = await _service.CheckProjectName(name);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckTheme_ReturnsNull_WhenThemeIsNull()
    {
        var result = await _service.CheckTheme(null);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckTheme_ReturnsNull_WhenThemeIsEmpty()
    {
        var result = await _service.CheckTheme(string.Empty);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckTheme_ReturnsMaxLengthMessage_WhenThemeIsTooLong()
    {
        var tooLongTheme = new string(
            'a',
            ProjectConstraints.ThemeMaxLength + 1);

        var result = await _service.CheckTheme(tooLongTheme);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.ProjectThemeMaxLength),
            ProjectConstraints.ThemeMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckTheme_ReturnsNull_WhenThemeIsValid()
    {
        var result = await _service.CheckTheme("Dark");

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckTheme_ReturnsNull_WhenThemeLengthEqualsMaxLength()
    {
        var theme = new string(
            'a',
            ProjectConstraints.ThemeMaxLength);

        var result = await _service.CheckTheme(theme);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionIsNull()
    {
        var result = await _service.CheckDescription(null);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionIsEmpty()
    {
        var result = await _service.CheckDescription(string.Empty);

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsMaxLengthMessage_WhenDescriptionIsTooLong()
    {
        var tooLongDescription = new string(
            'a',
            ProjectConstraints.DescriptionMaxLength + 1);

        var result = await _service.CheckDescription(tooLongDescription);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.ProjectDescriptionMaxLength),
            ProjectConstraints.DescriptionMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionIsValid()
    {
        var result = await _service.CheckDescription("Valid description");

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionLengthEqualsMaxLength()
    {
        var description = new string(
            'a',
            ProjectConstraints.DescriptionMaxLength);

        var result = await _service.CheckDescription(description);

        Assert.Null(result);
    }
}