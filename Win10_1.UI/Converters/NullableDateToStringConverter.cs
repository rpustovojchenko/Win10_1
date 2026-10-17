using System.Globalization;
using System.Windows.Data;

namespace Win10_1.UI.Converters;

public class NullableDateToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date && date != DateTime.MinValue)
            return date.ToString("dd.MM.yyyy", CultureInfo.CurrentCulture);

        return "-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
