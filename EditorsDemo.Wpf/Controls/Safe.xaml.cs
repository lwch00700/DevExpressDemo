using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.Windows.Media.Animation;

namespace EditorsDemo {
    public partial class Safe : UserControl {
        public Safe() {
            InitializeComponent();
            DigitTemplatesHelper.SetTemplates((ObjectList)Resources["digits"]);
            firstDigit.EditValue = 0d;
            secondDigit.EditValue = 0d;
            thirdDigit.EditValue = 0d;
        }
        void trackBarButtonClick(object sender, RoutedEventArgs e) {
            Button button = sender as Button;
            TrackBarEdit trackBar = (button.GetValue(BaseEdit.OwnerEditProperty)) as TrackBarEdit;
            StartAnimation(Convert.ToDouble(button.Tag), trackBar);
        }
        void StartAnimation(double value, TrackBarEdit trackBar) {
            DoubleAnimation animation = new DoubleAnimation() { From = trackBar.Value, To = value, Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500)), AccelerationRatio = 0.5, DecelerationRatio = 0.5, FillBehavior = FillBehavior.HoldEnd };
            Storyboard storyboard = new Storyboard();
            storyboard.SetValue(Storyboard.TargetProperty, trackBar);
            storyboard.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath("Value"));
            storyboard.Children.Add(animation);
            storyboard.Begin(this, true);
        }

    }
    public class ToDigitConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return null;
            if(value as string == null)
                return DigitTemplatesHelper.GetTemplate((int)((double)value));
            else
                return DigitTemplatesHelper.GetTemplate((int)Convert.ToDouble((string)value));
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public static class DigitTemplatesHelper {
        static WeakReference templates;

        public static ControlTemplate GetTemplate(int digit) {
            return (ControlTemplate)((ObjectList)DigitTemplatesHelper.templates.Target)[digit];
        }

        public static void SetTemplates(ObjectList templates) {
            DigitTemplatesHelper.templates = new WeakReference(templates);
        }
    }
}
