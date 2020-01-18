using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FriendStorage.UI.Converters
{
    public class DataPickerOriginalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime datetime)
                return datetime.ToString("dd.MM.yyyy");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}