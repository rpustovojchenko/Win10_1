using System.Globalization;
using System.Windows.Data;
using Win10_1.UI.Converters;

namespace Win10_1.Tests.UI.Converters;

public class UtcDateToLocalStringConverterTests
{
    private readonly UtcDateToLocalStringConverter _converter = new();

    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

    [Fact]
    public void Convert_FormatsUtcDateAsLocalTime()
    {
        var utcDate = new DateTime(2026, 10, 16, 12, 0, 0, DateTimeKind.Utc);
        var expected = utcDate.ToLocalTime()
            .ToString(_converter.Format, Culture);

        var result = _converter.Convert(
            utcDate,
            typeof(string),
            null!,
            Culture);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_TreatsUnspecifiedDateAsUtc()
    {
        var unspecifiedDate = new DateTime(2026, 10, 16, 12, 0, 0, DateTimeKind.Unspecified);
        var expected = DateTime.SpecifyKind(unspecifiedDate, DateTimeKind.Utc)
            .ToLocalTime()
            .ToString(_converter.Format, Culture);

        var result = _converter.Convert(
            unspecifiedDate,
            typeof(string),
            null!,
            Culture);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_DoesNotConvertAlreadyLocalDate()
    {
        var localDate = new DateTime(2026, 10, 16, 12, 0, 0, DateTimeKind.Local);
        var expected = localDate.ToString(_converter.Format, Culture);

        var result = _converter.Convert(
            localDate,
            typeof(string),
            null!,
            Culture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("2026-10-16T12:00:00Z")]
    [InlineData(123)]
    public void Convert_ReturnsEmptyString_WhenValueIsNotDateTime(object? value)
    {
        var result = _converter.Convert(
            value!,
            typeof(string),
            null!,
            Culture);

        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Convert_UsesCustomFormat()
    {
        _converter.Format = "yyyy-MM-dd HH:mm:ss";

        var utcDate = new DateTime(2026, 10, 16, 12, 0, 0, DateTimeKind.Utc);
        var expected = utcDate.ToLocalTime()
            .ToString(_converter.Format, Culture);

        var result = _converter.Convert(
            utcDate,
            typeof(string),
            null!,
            Culture);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertBack_ParsesLocalStringAndReturnsUtc()
    {
        _converter.Format = "yyyy-MM-dd HH:mm";

        var localDate = new DateTime(2026, 10, 16, 15, 0, 0, DateTimeKind.Local);
        var text = localDate.ToString(_converter.Format, Culture);

        var result = _converter.ConvertBack(
            text,
            typeof(DateTime),
            null!,
            Culture);

        var expected = localDate.ToUniversalTime();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not a date")]
    public void ConvertBack_ReturnsDoNothing_WhenStringCannotBeParsed(string? value)
    {
        var result = _converter.ConvertBack(
            value!,
            typeof(DateTime),
            null!,
            Culture);

        Assert.Equal(Binding.DoNothing, result);
    }

    [Fact]
    public void ConvertBack_ReturnsDoNothing_WhenValueIsNotString()
    {
        var result = _converter.ConvertBack(
            123,
            typeof(DateTime),
            null!,
            Culture);

        Assert.Equal(Binding.DoNothing, result);
    }
}
