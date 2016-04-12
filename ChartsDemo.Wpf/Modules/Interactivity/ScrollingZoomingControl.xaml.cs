using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Interactivity/ScrollingZoomingControl(.SL).xaml"),
     CodeFile("Modules/Interactivity/ScrollingZoomingControl.xaml.(cs)")]
    public partial class ScrollingZoomingControl : ChartsDemoModule {
        const double initialZoomFactor = 0.1;
        const int pointCount = 1500;

        public override ChartControl ActualChart {
            get { return chart; }
        }
        public ScrollingZoomingControl() {
            InitializeComponent();
        }

        void ChartsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            DataContext = new PricesModel(pointCount);
            AxisX.ActualVisualRange.MinValue = DateTime.Now.Subtract(new TimeSpan((int)(pointCount * initialZoomFactor), 0, 0, 0));
        }
        void UpdateAxisXScrollBarPositionEnabled(object sender, RoutedEventArgs e) {
            cbeAxisXScrollBarPosition.IsEnabled = chbAxisXScrollBarVisible.IsChecked.Value && chbAxisXNavigation.IsChecked.Value;
            if (!chbAxisXNavigation.IsChecked.Value)
                ((XYDiagram2D)chart.Diagram).ActualAxisX.ActualVisualRange.SetAuto();
        }
        void UpdateAxisYScrollBarPositionEnabled(object sender, RoutedEventArgs e) {
            cbeAxisYScrollBarPosition.IsEnabled = chbAxisYScrollBarVisible.IsChecked.Value && chbAxisYNavigation.IsChecked.Value;
            if (!chbAxisYNavigation.IsChecked.Value)
                ((XYDiagram2D)chart.Diagram).ActualAxisY.ActualVisualRange.SetAuto();
        }
    }

    public class PriceByDate {
        double price;
        DateTime tradeDate;

        public double Price {
            get { return price; }
        }
        public DateTime TradeDate {
            get { return tradeDate; }
        }

        public PriceByDate(DateTime date, double price) {
            this.tradeDate = date;
            this.price = price;
        }
    }

    public class PricesModel {
        readonly List<PriceByDate> product1 = new List<PriceByDate>();
        readonly List<PriceByDate> product2 = new List<PriceByDate>();
        readonly List<PriceByDate> product3 = new List<PriceByDate>();

        public List<PriceByDate> Product1 {
            get { return product1; }
        }
        public string Product1Title {
            get { return "Product 1"; }
        }
        public List<PriceByDate> Product2 {
            get { return product2; }
        }
        public string Product2Title {
            get { return "Product 2"; }
        }
        public List<PriceByDate> Product3 {
            get { return product3; }
        }
        public string Product3Title {
            get { return "Product 3"; }
        }
        public string Title {
            get { return "Sales History"; }
        }

        public PricesModel(int pointsCount) {
            Random random = new Random();
            DateTime date = DateTime.Now.Subtract(new TimeSpan(pointsCount, 0, 0, 0));
            double price1 = GenerateStartValue(random);
            double price2 = GenerateStartValue(random);
            double price3 = GenerateStartValue(random);
            for (int i = 0; i < pointsCount; i++) {
                product1.Add(new PriceByDate(date, price1));
                product2.Add(new PriceByDate(date, price2));
                product3.Add(new PriceByDate(date, price3));
                price1 += GenerateAddition(random);
                price2 += GenerateAddition(random);
                price3 += GenerateAddition(random);
                date = date.AddDays(1);
            }
        }

        double GenerateAddition(Random random) {
            double factor = random.NextDouble();
            if (factor == 1)
                factor = 5;
            else if (factor == 0)
                factor = -5;
            return (factor - 0.475) * 10;
        }
        double GenerateStartValue(Random random) {
            return random.NextDouble() * 100;
        }
    }
}
