using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/FinancialSeries/Stock2DControl(.SL).xaml"),
     CodeFile("Modules/FinancialSeries/Stock2DControl.xaml.(cs)")]
    public partial class Stock2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Stock2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Stock2DModelKindHelper.FindActualStock2DModelKind(typeof(ThinStock2DModel));
            chart.Diagram.Series[0].DataSource = CreateDataSource();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void cbStockType_SelectionChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                StockSeries2D series = ((StockSeries2D)chart.Diagram.Series[0]);
                series.ShowOpenClose = (StockType)cbStockType.SelectedIndex;
            }
        }
        void cbReductionLevel_SelectionChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                StockSeries2D series = (StockSeries2D)chart.Diagram.Series[0];
                series.ReductionOptions.Level = (StockLevel)cbReductionLevel.SelectedIndex;
            }
        }
        List<FinancialPoint> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Dell.xml");
            List<FinancialPoint> result = new List<FinancialPoint>();
            if (document != null) {
                foreach (XElement element in document.Element("Dell").Elements()) {
                    FinancialPoint point = new FinancialPoint();
                    point.Argument = element.Element("Argument").Value;
                    point.OpenValue = Convert.ToDouble(element.Element("OpenValue").Value, CultureInfo.InvariantCulture);
                    point.CloseValue = Convert.ToDouble(element.Element("CloseValue").Value, CultureInfo.InvariantCulture);
                    point.LowValue = Convert.ToDouble(element.Element("LowValue").Value, CultureInfo.InvariantCulture);
                    point.HighValue = Convert.ToDouble(element.Element("HighValue").Value, CultureInfo.InvariantCulture);
                    result.Add(point);
                }
            }
            return result;
        }
        public override bool SupportSidebarContent() {
            return false;
        }
    }

    public class ComboBoxItem {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
