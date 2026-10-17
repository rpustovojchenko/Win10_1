using System.Windows;
using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;
using Win10_1.Domain.Localization;
using Win10_1.UI.Services;

namespace Win10_1.Tests.UI.Services;

[Collection("WpfApplication")]
public class TaskValidateServiceTests
{
    private readonly TaskValidator _taskValidator = new();
    private readonly TaskValidateService _service;

    public TaskValidateServiceTests()
    {
        if (Application.Current is null)
            _ = new Application();

        _service = new TaskValidateService(_taskValidator);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public async Task CheckTitle_ReturnsRequiredMessage_WhenTitleIsInvalid(
        string? title)
    {
        var result = await _service.CheckTitle(title!);

        var expected = LocalizationService.GetString(
            LocalizationKeys.TaskTitleRequired);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckTitle_ReturnsMaxLengthMessage_WhenTitleIsTooLong()
    {
        var tooLongTitle = new string(
            'a',
            TaskConstraints.TitleMaxLength + 1);

        var result = await _service.CheckTitle(tooLongTitle);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.TaskTitleMaxLength),
            TaskConstraints.TitleMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckTitle_ReturnsNull_WhenTitleIsValid()
    {
        var result = await _service.CheckTitle("Valid Task Title");

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckTitle_ReturnsNull_WhenTitleLengthEqualsMaxLength()
    {
        var title = new string(
            'a',
            TaskConstraints.TitleMaxLength);

        var result = await _service.CheckTitle(title);

        Assert.Null(result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public async Task CheckDescription_ReturnsRequiredMessage_WhenDescriptionIsInvalid(
        string? description)
    {
        var result = await _service.CheckDescription(description!);

        var expected = LocalizationService.GetString(
            LocalizationKeys.TaskDescriptionRequired);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsMaxLengthMessage_WhenDescriptionIsTooLong()
    {
        var tooLongDescription = new string(
            'a',
            TaskConstraints.DescriptionMaxLength + 1);

        var result = await _service.CheckDescription(tooLongDescription);

        var expected = string.Format(
            LocalizationService.GetString(
                LocalizationKeys.TaskDescriptionMaxLength),
            TaskConstraints.DescriptionMaxLength);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionIsValid()
    {
        var result = await _service.CheckDescription("Valid task description");

        Assert.Null(result);
    }

    [Fact]
    public async Task CheckDescription_ReturnsNull_WhenDescriptionLengthEqualsMaxLength()
    {
        var description = new string(
            'a',
            TaskConstraints.DescriptionMaxLength);

        var result = await _service.CheckDescription(description);

        Assert.Null(result);
    }
}