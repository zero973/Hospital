using Hospital.Core.Enums;
using Hospital.UI.Extensions;
using System.Globalization;
using System.Windows.Data;

namespace Hospital.UI.ValueConverters;

public class PeriodTypeToStringConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is PeriodTypes type)
            return type.AsString();

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();

}