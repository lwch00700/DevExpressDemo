using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/SeriesTitlesControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/SeriesTitlesControl.xaml.(cs)")]
    public partial class SeriesTitlesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public SeriesTitlesControl() {
            InitializeComponent();
            List<GSP> dataSource = CreateDataSource();
            foreach (Series series in ActualChart.Diagram.Series)
                series.DataSource = dataSource;
        }
        List<GSP> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/GSP.xml");
            List<GSP> result = new List<GSP>();
            if (document != null) {
                foreach (XElement element in document.Element("GSPs").Elements()) {
                    string region = element.Element("Region").Value;
                    string year = element.Element("Year").Value;
                    double product = Convert.ToDouble(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    result.Add(new GSP(region, year, product));
                }
            }
            return result;
        }
        void Chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            ChartControl chart = sender as ChartControl;
            if (chart == null)
                return;
            if (chart.Diagram.Series.Count > 0 && !chart.Diagram.Series[0].Equals(e.Series))
                e.LegendText = string.Empty;
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
