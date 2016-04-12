using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/PriceIndicatorsControl(.SL).xaml"),
     CodeFile("Modules/DataAnalysis/PriceIndicatorsControl.xaml.(cs)")]
    public partial class PriceIndicatorsControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public PriceIndicatorsControl() {
            InitializeComponent();
            chart.Diagram.Series[0].DataSource = CreateDataSource();
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

    public class SelectedPriceIndicatorToVisibleConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var movingAverage = values[0] as Indicator;
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
