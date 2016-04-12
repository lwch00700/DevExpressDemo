using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/ConstantLinesControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/ConstantLinesControl.xaml.(cs)")]
    public partial class ConstantLinesControl : ChartsDemoModule {
        public ConstantLinesControl() {
            InitializeComponent();
            Series.DataSource = CreateDataSource();
            Series.CrosshairLabelPattern = "Year: {A}\nPrice: {V}";
        }
        ConstantLineCollection ConstantLines {
            get { return ((XYDiagram2D)chart.Diagram).AxisY.ConstantLinesBehind; }
        }
        public override ChartControl ActualChart {
            get { return chart; }
        }
        void chart_BoundDataChanged(object sender, RoutedEventArgs e) {
            XYDiagram2D diagram = (XYDiagram2D)chart.Diagram;
            if (diagram.Series[0].Points.Count == 0)
                return;
            double minPrice = Double.MaxValue;
            double maxPrice = 0;
            double averagePrice = 0;
            foreach (SeriesPoint point in diagram.Series[0].Points) {
                double price = point.Value;
                if (price < minPrice)
                    minPrice = price;
                if (price > maxPrice)
                    maxPrice = price;
                averagePrice += price;
            }
            averagePrice /= diagram.Series[0].Points.Count;
            ConstantLine minConstantLine = new ConstantLine(minPrice, "Min");
            minConstantLine.Brush = new SolidColorBrush(Colors.Green);
            minConstantLine.Title.Foreground = new SolidColorBrush(Colors.Green);
            ConstantLine maxConstantLine = new ConstantLine(maxPrice, "Max");
            maxConstantLine.Brush = new SolidColorBrush(Colors.Red);
            maxConstantLine.Title.Foreground = new SolidColorBrush(Colors.Red);
            ConstantLine averageConstantLine = new ConstantLine(averagePrice, "Average");
            averageConstantLine.Brush = new SolidColorBrush(Color.FromArgb(0xFF, 0x9A, 0xCD, 0x32));
            averageConstantLine.Title.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x9A, 0xCD, 0x32));
            ConstantLines.AddRange(new ConstantLine[] { minConstantLine, maxConstantLine, averageConstantLine
            });
            foreach (ConstantLine constantLine in ConstantLines)
                constantLine.Title.Alignment = ConstantLineTitleAlignment.Far;
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbVisible_CheckedUnchecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                bool isVisible = chbVisible.IsChecked.HasValue && chbVisible.IsChecked.Value;
                foreach (ConstantLine constantLine in ConstantLines)
                    constantLine.Visible = isVisible;
                lbTitleAlignment.IsEnabled = isVisible;
                lbTitlePosition.IsEnabled = isVisible;
            }
        }
        List<OilPrice> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/OilPrices.xml");
            List<OilPrice> oilPrices = new List<OilPrice>();
            if (document != null) {
                foreach (XElement element in document.Element("OilPrices").Elements()) {
                    double year = Convert.ToDouble(element.Element("Year").Value, CultureInfo.InvariantCulture);
                    double price = Convert.ToDouble(element.Element("Price").Value, CultureInfo.InvariantCulture);
                    oilPrices.Add(new OilPrice(year, price));
                }
            }
            return oilPrices;
        }
        void lbTitleAlignment_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                ConstantLineTitleAlignment titleAlignment;
                if (lbTitleAlignment.SelectedIndex == 1)
                    titleAlignment = ConstantLineTitleAlignment.Far;
                else
                    titleAlignment = ConstantLineTitleAlignment.Near;
                foreach (ConstantLine constantLine in ConstantLines)
                    constantLine.Title.Alignment = titleAlignment;
            }
        }
        void lbTitlePosition_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                bool showBelowLine = lbTitlePosition.SelectedIndex == 1;
                foreach (ConstantLine constantLine in ConstantLines)
                    constantLine.Title.ShowBelowLine = showBelowLine;
            }
        }
    }

    public class OilPrice {
        double price;
        double year;
        public OilPrice(double year, double price) {
            this.year = year;
            this.price = price;
        }
        public double Price {
            get { return price; }
        }
        public double Year {
            get { return year; }
        }
    }
}
