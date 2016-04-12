using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace CarouselTutorial {
    public partial class TutorialPage3 : Page {
        public TutorialPage3() {
            InitializeComponent();
        }
    }
    public class ParameterToCharConverter : MarkupExtension, IValueConverter {
        string[] chars = { "C", "A", "R", "O", "U", "S", "E", "L" };
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return chars[(int)Math.Round((double)value)];
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class ParameterToColorConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return new SolidColorBrush(new Color() { A = 255, B = (byte)(double)value });
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
