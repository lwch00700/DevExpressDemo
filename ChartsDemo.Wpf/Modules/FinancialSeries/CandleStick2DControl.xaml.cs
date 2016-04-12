using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/FinancialSeries/CandleStick2DControl(.SL).xaml"),
     CodeFile("Modules/FinancialSeries/CandleStick2DControl.xaml.(cs)")]
    public partial class CandleStick2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }
        ImageSource positiveDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ArrowUp.png", UriKind.Relative));
        ImageSource negativeDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ArrowDown.png", UriKind.Relative));
        ImageSource zeroDynamic = new BitmapImage(new Uri("/ChartsDemo;component/Images/ZeroDynamic.png", UriKind.Relative));

        public CandleStick2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = CandleStick2DModelKindHelper.FindActualCandleStick2DModelKind(typeof(SimpleCandleStick2DModel));
            chart.Diagram.Series[0].DataSource = CreateDataSource();
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);
        }
        public override bool SupportSidebarContent() {
            return false;
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        List<StockDataPoint> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Dell.xml");
            List<StockDataPoint> result = new List<StockDataPoint>();
            if (document != null) {
                IEnumerable<XElement> elements = document.Element("Dell").Elements();
                IEnumerator enumerator = elements.GetEnumerator();
                enumerator.MoveNext();
                StockDataPoint previousPoint = ReadDataPointFrpmXML((XElement)(enumerator.Current));

                foreach (XElement element in elements) {
                    StockDataPoint point = ReadDataPointFrpmXML(element);
                    point.ToolTipData.OpenDynamic  = GetStockDynamic(previousPoint.Open, point.Open).ImageSource;
                    point.ToolTipData.CloseDynamic = GetStockDynamic(previousPoint.Close, point.Close).ImageSource;
                    point.ToolTipData.HighDynamic  = GetStockDynamic(previousPoint.High, point.High).ImageSource;
                    point.ToolTipData.LowDynamic   = GetStockDynamic(previousPoint.Low, point.Low).ImageSource;
                    point.ToolTipData.OpenFontBrush  = GetStockDynamic(previousPoint.Open, point.Open).Brush;
                    point.ToolTipData.CloseFontBrush = GetStockDynamic(previousPoint.Close, point.Close).Brush;
                    point.ToolTipData.HighFontBrush  = GetStockDynamic(previousPoint.High, point.High).Brush;
                    point.ToolTipData.LowFontBrush = GetStockDynamic(previousPoint.Low, point.Low).Brush;
                    result.Add(point);
                    previousPoint = point;
                }
            }
            return result;
        }

        StockDynamic GetStockDynamic(decimal previousPointValue, decimal currentPointValue) {
            if (previousPointValue < currentPointValue)
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 63, 171, 0)), positiveDynamic);
            else if (previousPointValue > currentPointValue)
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 213, 50, 35)), negativeDynamic);
            else
                return new StockDynamic(new SolidColorBrush(Color.FromArgb(255, 161, 161, 161)), zeroDynamic);
        }

        StockDataPoint ReadDataPointFrpmXML(XElement element) {
            StockDataPoint point = new StockDataPoint();
            point.TradeDate = Convert.ToDateTime(element.Element("Argument").Value, CultureInfo.InvariantCulture);
            point.Open = Convert.ToDecimal(element.Element("OpenValue").Value, CultureInfo.InvariantCulture);
            point.Close = Convert.ToDecimal(element.Element("CloseValue").Value, CultureInfo.InvariantCulture);
            point.Low = Convert.ToDecimal(element.Element("LowValue").Value, CultureInfo.InvariantCulture);
            point.High = Convert.ToDecimal(element.Element("HighValue").Value, CultureInfo.InvariantCulture);
            point.ToolTipData = new ToolTipStockData();
            point.ToolTipData.Owner = point;
            return point;
        }
        void cbReductionLevel_SelectionChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                CandleStickSeries2D series = (CandleStickSeries2D)chart.Diagram.Series[0];
                series.ReductionOptions.Level = (StockLevel)cbReductionLevel.SelectedIndex;
            }
        }
    }

    public class StockDataPoint {
        public ToolTipStockData ToolTipData { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
    }

    public class ToolTipStockData {
        public StockDataPoint Owner { get; set; }
        public ImageSource HighDynamic { get; set; }
        public ImageSource LowDynamic { get; set; }
        public ImageSource OpenDynamic { get; set; }
        public ImageSource CloseDynamic { get; set; }
        public Brush HighFontBrush { get; set; }
        public Brush LowFontBrush { get; set; }
        public Brush OpenFontBrush { get; set; }
        public Brush CloseFontBrush { get; set; }
    }

    public class StockDynamic {
        public Brush Brush { get; private set; }
        public ImageSource ImageSource { get; private set; }

        public StockDynamic(Brush brush, ImageSource imageSource) {
            Brush = brush;
            ImageSource = imageSource;
        }
    }
}
