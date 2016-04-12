using System;
using System.Windows;
using System.Windows.Data;

namespace GridDemo.ModuleResources {
    public class ResourceKeyThemeConverter: IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension temp = (DevExpress.Xpf.Grid.Themes.GridRowThemeKeyExtension)values[1];
            temp.ThemeName = values[2].ToString()!= "DeepBlue" ? values[2].ToString() : null ;
            return ((FrameworkElement)values[0]).FindResource(temp);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class EnumBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string parameterString = parameter as string;
            if(parameterString == null)
                return DependencyProperty.UnsetValue;
            if(Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;
            object parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string parameterString = parameter as string;
            if(parameterString == null)
                return DependencyProperty.UnsetValue;
            return Enum.Parse(targetType, parameterString);
        }
    }

    public class BoolToIndexConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)value != -1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value ? 0 : -1;
        }
    }

    public class IncrementInt : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)value + 20;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
