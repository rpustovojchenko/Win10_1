using System.Globalization;
using System.Windows;
using Win10_1.UI.Converters;

namespace Win10_1.Tests.UI.Converters;

public class BoolToVisibilityConverterTests
{
    private readonly BoolToVisibilityConverter _converter = new();

    [Theory]
    [InlineData(true, Visibility.Visible)]
    [InlineData(false, Visibility.Collapsed)]
    public void Convert_ReturnsExpectedVisibility(
        bool value,
        Visibility expected)
    {
        var result = _converter.Convert(
            value,
            typeof(Visibility),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "Inverted", Visibility.Collapsed)]
    [InlineData(false, "Inverted", Visibility.Visible)]
    public void Convert_InvertsResult_WhenParameterIsInverted(
        bool value,
        string parameter,
        Visibility expected)
    {
        var result = _converter.Convert(
            value,
            typeof(Visibility),
            parameter,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, Visibility.Collapsed)]
    [InlineData("not a bool", Visibility.Collapsed)]
    [InlineData(1, Visibility.Collapsed)]
    public void Convert_ReturnsCollapsed_WhenValueIsNotBoolean(
        object? value,
        Visibility expected)
    {
        var result = _converter.Convert(
            value!,
            typeof(Visibility),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Inverted", Visibility.Visible)]
    [InlineData(null, Visibility.Collapsed)]
    [InlineData("Other", Visibility.Collapsed)]
    public void Convert_ReturnsExpectedVisibility_ForNonBooleanValue(
        string? parameter,
        Visibility expected)
    {
        var result = _converter.Convert(
            "not a bool",
            typeof(Visibility),
            parameter!,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Visibility.Visible, true)]
    [InlineData(Visibility.Collapsed, false)]
    [InlineData(Visibility.Hidden, false)]
    public void ConvertBack_ReturnsExpectedBoolean(
        Visibility value,
        bool expected)
    {
        var result = _converter.ConvertBack(
            value,
            typeof(bool),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("Visible")]
    [InlineData(1)]
    public void ConvertBack_ReturnsFalse_WhenValueIsNotVisibility(object? value)
    {
        var result = _converter.ConvertBack(
            value!,
            typeof(bool),
            null!,
            CultureInfo.InvariantCulture);

        Assert.False((bool)result!);
    }
}