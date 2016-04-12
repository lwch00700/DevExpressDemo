using System;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/FunnelSeries/Funnel2DControl(.SL).xaml"),
     CodeFile("Modules/FunnelSeries/Funnel2DControl.xaml.(cs)")]
    public partial class Funnel2DControl : ChartsDemoModule {
        public Funnel2DControl() {
            InitializeComponent();
        }
        public override ChartControl ActualChart { get { return chart; } }
    }
    public class TextPatternConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value.GetType().Equals(typeof(bool)) && (bool)value)
                return "{A}: {VP:P0}";
            else
                return "{A}: {V:G}";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class LabelValueConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Funnel2DLabelPosition position = (Funnel2DLabelPosition)value;
            return !position.Equals(Funnel2DLabelPosition.Center);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
