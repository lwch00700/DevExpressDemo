using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl(.SL).xaml"),
     CodeFile("Modules/DataAnalysis/BasicFinancialIndicatorsControl.xaml.(cs)")]
    public partial class BasicFinancialIndicatorsControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BasicFinancialIndicatorsControl() {
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
        void ComboBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            switch (cbeIndicatorKind.SelectedIndex) {
                case 0:
                    ShowTrendLines();
                    break;
                case 1:
                    ShowRegressionLine();
                    break;
                case 2:
                    ShowFibonacciRetracement();
                    break;
                case 3:
                    ShowFibonacciFans();
                    break;
                case 4:
                    ShowFibonacciArcs();
                    break;
            }
        }
        void ShowFibonacciArcs() {
            HideAllIndicators();
            fibonacciArcs.Visible = true;
        }
        void ShowFibonacciFans() {
            HideAllIndicators();
            fibonacciFans.Visible = true;
        }
       void ShowFibonacciRetracement() {
            HideAllIndicators();
            fibonacciRetracement.Visible = true;
        }
        void ShowRegressionLine() {
            HideAllIndicators();
            regressionLine.Visible = true;
        }
        void ShowTrendLines() {
            HideAllIndicators();
            trendLine1.Visible = true;
            trendLine2.Visible = true;
        }
        void HideAllIndicators() {
            foreach (Indicator indicator in Dell.Indicators)
                indicator.Visible = false;
        }

        public override bool SupportSidebarContent() {
            return false;
        }
    }
}
