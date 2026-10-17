using System.Globalization;
using Win10_1.UI.Converters;

namespace Win10_1.Tests.UI.Converters;

public class NullableDateToStringConverterTests
{
    private readonly NullableDateToStringConverter _converter = new();

    [Theory]
    [InlineData("2026-10-16", "16.10.2026")]
    [InlineData("2026-01-01", "01.01.2026")]
    [InlineData("2026-12-31", "31.12.2026")]
    public void Convert_ReturnsDateInDayMonthYearFormat(
        string dateString,
        string expected)
    {
        var date = DateTime.Parse(
            dateString,
            CultureInfo.InvariantCulture);

        var result = _converter.Convert(
            date,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("not a date")]
    [InlineData(123)]
    public void Convert_ReturnsDash_WhenValueIsNotDateTime(object? value)
    {
        var result = _converter.Convert(
            value!,
            typeof(string),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal("-", result);
    }

    [Fact]
    public void Convert_ReturnsDash_WhenDateIsMinValue()
    {
        var result = _converter.Convert(
            DateTime.MinValue,
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
                "16.10.2026",
                typeof(DateTime?),
                null!,
                CultureInfo.InvariantCulture));
    }
}
