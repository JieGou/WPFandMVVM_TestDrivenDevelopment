using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using FriendStorage.UI.DataProvider.Lookups;

namespace FriendStorage.UI.Converters
{
    public class ComboBoxOriginalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int id)) return null;

            if (parameter is ComboBox comboBox && comboBox.ItemsSource != null)
            {
                return comboBox.ItemsSource.OfType<LookupItem>().SingleOrDefault(i => i.Id == id)?.DisplayValue;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
