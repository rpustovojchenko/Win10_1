using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;

namespace Win10_1.UI.Converters;

public class UtcDateToLocalStringConverter : IValueConverter
{
    public string Format { get; set; } = "dd.MM.yyyy HH:mm";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Utc)
                dt = dt.ToLocalTime();
            else if (dt.Kind == DateTimeKind.Unspecified)
                dt = DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToLocalTime();

            return dt.ToString(Format, culture);
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string s && DateTime.TryParse(s, culture, DateTimeStyles.None, out var dt))
            return dt.ToUniversalTime();

        return Binding.DoNothing;
    }
}
