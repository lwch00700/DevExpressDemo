using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PrintingDemo {
    public class BoolToSolidColorBrush1Converter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool boolValue = (bool)value;
            if(boolValue)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0x7B, 0x99, 0xC9));
            else
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xBB, 0x8A, 0x92));
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
