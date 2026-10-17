using Win10_1.Business.Validation;
using Win10_1.Domain.Constraints;

namespace Win10_1.Tests.Business.Validation;

public class ProjectValidatorTests
{
    private readonly ProjectValidator _validator = new();

    [Theory]
    [InlineData("Мой проект", true)]
    [InlineData(" Project ", true)]
    [InlineData("a", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("\t", false)]
    public async Task IsProjectNameValid_ReturnsExpectedResult(
        string? projectName,
        bool expected)
    {
        var result = await _validator.IsProjectNameValid(projectName!);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsProjectNameLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var projectName = new string('a', ProjectConstraints.ProjectNameMaxLength);

        var result = await _validator.IsProjectNameLengthValid(projectName);

        Assert.True(result);
    }

    [Fact]
    public async Task IsProjectNameLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var projectName = new string(
            'a',
            ProjectConstraints.ProjectNameMaxLength + 1);

        var result = await _validator.IsProjectNameLengthValid(projectName);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public async Task IsProjectNameLengthValid_HandlesNullOrShortValue(
        string? projectName,
        bool expected)
    {
        var result = await _validator.IsProjectNameLengthValid(projectName);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsThemeLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var theme = new string('a', ProjectConstraints.ThemeMaxLength);

        var result = await _validator.IsThemeLengthValid(theme);

        Assert.True(result);
    }

    [Fact]
    public async Task IsThemeLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var theme = new string('a', ProjectConstraints.ThemeMaxLength + 1);

        var result = await _validator.IsThemeLengthValid(theme);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public async Task IsThemeLengthValid_HandlesNullOrShortValue(
        string? theme,
        bool expected)
    {
        var result = await _validator.IsThemeLengthValid(theme);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task IsDescriptionLengthValid_ReturnsTrue_WhenLengthEqualsMaxLength()
    {
        var description = new string('a', ProjectConstraints.DescriptionMaxLength);

        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.True(result);
    }

    [Fact]
    public async Task IsDescriptionLengthValid_ReturnsFalse_WhenLengthExceedsMaxLength()
    {
        var description = new string(
            'a',
            ProjectConstraints.DescriptionMaxLength + 1);

        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.False(result);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", true)]
    public async Task IsDescriptionLengthValid_HandlesNullOrShortValue(
        string? description,
        bool expected)
    {
        var result = await _validator.IsDescriptionLengthValid(description);

        Assert.Equal(expected, result);
    }
}
