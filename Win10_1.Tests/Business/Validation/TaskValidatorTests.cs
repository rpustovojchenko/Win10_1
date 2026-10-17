using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;

namespace Win10_1.Tests.Business.Validation;

public class TaskValidatorTests
{
    private readonly TaskValidator _validator = new();

    [Theory]
    [InlineData("Task", true)]
    [InlineData(" Task ", true)]
    [InlineData("a", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    public async Task IsTitleValid_ReturnsExpectedResult(
    string? title,
    bool expected)
    {
        var result = await _validator.IsTitleValid(title!);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsTitleLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var title = new string('a', TaskConstraints.TitleMaxLength);

        var result = await _validator.IsTitleLengthValid(title);

        Assert.True(result);
    }

    [Fact]
    public async Task IsTitleLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var title = new string('a', TaskConstraints.TitleMaxLength + 1);

        var result = await _validator.IsTitleLengthValid(title);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public async Task IsTitleLengthValid_HandlesNullOrShortTitle(
        string? title,
        bool expected)
    {
        var result = await _validator.IsTitleLengthValid(title);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(" Task description", true)]
    [InlineData(" Description ", true)]
    [InlineData("a", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    [InlineData("\n", false)]
    public async Task IsDescriptionValid_ReturnsExpectedResult(
        string? description,
        bool expected)
    {
        var result = await _validator.IsDescriptionValid(description!);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsDescriptionLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var description = new string('a', TaskConstraints.DescriptionMaxLength);

        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.True(result);
    }

    [Fact]
    public async Task IsDescriptionLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var description = new string(
            'a',
            TaskConstraints.DescriptionMaxLength + 1);

        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public async Task IsDescriptionLengthValid_HandlesNullOrShortDescription(
        string? description,
        bool expected)
    {
        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.Equal(expected, result);
    }
}
