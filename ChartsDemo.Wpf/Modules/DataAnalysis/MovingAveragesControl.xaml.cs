using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/MovingAveragesControl(.SL).xaml"),
     CodeFile("Modules/DataAnalysis/MovingAveragesControl.xaml.(cs)")]
    public partial class MovingAveragesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public MovingAveragesControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CsvReader.ReadFinancialData("AUDUSDDaily.csv");
        }

        public override bool SupportSidebarContent() {
            return false;
        }
    }

    public class SelectedMovingAverageToVisibleConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var movingAverage = values[0] as MovingAverage;
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
