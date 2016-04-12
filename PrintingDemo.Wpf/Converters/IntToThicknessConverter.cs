using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PrintingDemo {
    public class IntToThicknessConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Thickness thickness = (Thickness)value;
            return thickness.Bottom;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double doubleValue = System.Convert.ToDouble(value);
            return new Thickness(doubleValue, 0, 0, doubleValue);
        }

        #endregion
    }

    public class ThicknessConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
           Thickness thickness = (Thickness)value;
            if(parameter != null && parameter.Equals("right"))
               return new Thickness(thickness.Bottom, 0, thickness.Bottom, thickness.Bottom);
            return thickness;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}
