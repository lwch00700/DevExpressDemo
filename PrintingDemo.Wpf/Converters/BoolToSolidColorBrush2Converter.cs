using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PrintingDemo {
    public class BoolToSolidColorBrush2Converter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool boolValue = (bool)value;
            if(boolValue)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xEA, 0xFB));
            else
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xF1, 0xE2, 0xE4));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
