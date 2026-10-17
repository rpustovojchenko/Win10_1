using System.Globalization;
using System.Windows.Data;

namespace Win10_1.UI.Converters;

public class NullableThemeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string theme && !string.IsNullOrWhiteSpace(theme))
            return theme;

        return "-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
