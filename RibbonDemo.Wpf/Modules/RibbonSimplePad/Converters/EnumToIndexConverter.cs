using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
namespace RibbonDemo {
    public class EnumToIndexConverter : IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return DependencyProperty.UnsetValue;
            return (int)value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return Enum.ToObject(targetType, (int)value);
        }
        #endregion
    }
    public class ObjectsEqualityConverter : IValueConverter {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(value == null)
                return value;
            var result = string.Equals(value.ToString(), parameter);
            return Inverse ? !result : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? parameter : null;
        }
    }
}
