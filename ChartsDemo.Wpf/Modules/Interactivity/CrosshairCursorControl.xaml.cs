using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/CrosshairCursorControl(.SL).xaml"),
     CodeFile("Modules/Interactivity/CrosshairCursorControl.xaml.(cs)")]
    public partial class CrosshairCursorControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public CrosshairCursorControl() {
            InitializeComponent();
            series.DataSource = CreateDataSource();
            DemoModuleControlHelper.PrepareComboBox(cbSnapMode, "Nearest Argument", "Nearest Value");
            diagram.AxisX.WholeRange.MinValue = new DateTime(2010, 1, 1);
            diagram.AxisX.WholeRange.MaxValue = new DateTime(2010, 12, 31);
        }

        List<GoldPrice> CreateDataSource() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/GoldPrices.xml");
            List<GoldPrice> goldPrices = new List<GoldPrice>();
            if (document != null) {
                foreach (XElement element in document.Element("GoldPrices").Elements()) {
                    DateTime date = Convert.ToDateTime(element.Element("Date").Value, CultureInfo.InvariantCulture);
                    double price = Convert.ToDouble(element.Element("Price").Value, CultureInfo.InvariantCulture);
                    goldPrices.Add(new GoldPrice(date, price));
                }
            }
            return goldPrices;
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }

    public class GoldPrice {
        readonly DateTime date;
        readonly double price;

        public DateTime Date { get { return date; } }
        public double Price { get { return price; } }

        public GoldPrice(DateTime date, double price) {
            this.date = date;
            this.price = price;
        }
    }
}
