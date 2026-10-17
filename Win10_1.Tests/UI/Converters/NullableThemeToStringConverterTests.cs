using System.Globalization;
using Win10_1.UI.Converters;

namespace Win10_1.Tests.UI.Converters;

public class NullableThemeToStringConverterTests
{
    private readonly NullableThemeToStringConverter _converter = new();

    [Theory]
    [InlineData("Dark")]
    [InlineData("Light")]
    [InlineData("System")]
    public void Convert_ReturnsTheme_WhenValueIsNonEmptyString(string theme)
    {
        var result = _converter.Convert(
            theme,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(theme, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void Convert_ReturnsDash_WhenThemeIsNullOrWhitespace(string? theme)
    {
        var result = _converter.Convert(
            theme!,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal("-", result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(1)]
    [InlineData(true)]
    public void Convert_ReturnsDash_WhenValueIsNotString(object? value)
    {
        var result = _converter.Convert(
            value!,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal("-", result);
    }

    [Fact]
    public void Convert_ReturnsDash_WhenValueIsNull()
    {
        var result = _converter.Convert(
            null!,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal("-", result);
    }

    [Fact]
    public void ConvertBack_ThrowsNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(
            () => _converter.ConvertBack(
                "Dark",
                typeof(string),
                null!,
                CultureInfo.InvariantCulture));
    }
}
