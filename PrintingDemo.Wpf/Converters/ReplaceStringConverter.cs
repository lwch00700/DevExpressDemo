using System;
using System.Windows.Data;

namespace PrintingDemo {
    public class ReplaceStringConverter : IValueConverter {
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public ReplaceStringConverter() {
            OldValue = Environment.NewLine;
            NewValue = " ";
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return ((string)value).Replace(OldValue, NewValue);
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
