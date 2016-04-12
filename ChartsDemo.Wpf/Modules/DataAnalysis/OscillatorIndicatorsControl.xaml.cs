using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl(.SL).xaml"),
     CodeFile("Modules/DataAnalysis/OscillatorIndicatorsControl.xaml.(cs)")]
    public partial class OscillatorIndicatorsControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public OscillatorIndicatorsControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CsvReader.ReadFinancialData("USDJPYDaily.csv");
        }

        public override bool SupportSidebarContent() {
            return false;
        }
    }

    public class SelectedSeparatePaneIndicatorToVisibleConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var movingAverage = values[0] as SeparatePaneIndicator;
            var listBoxItem = values[1] as ListBoxEditItem;
            if (movingAverage != null && listBoxItem != null && listBoxItem.Tag is Type && movingAverage.GetType() == (Type)listBoxItem.Tag)
                return true;
            else
                return false;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
