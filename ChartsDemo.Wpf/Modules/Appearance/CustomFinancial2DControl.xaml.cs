using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomFinancial2DControl(.SL).xaml"),
     CodeFile("Modules/Appearance/CustomFinancial2DControl.xaml.(cs)")]
    public partial class CustomFinancial2DControl : ChartsDemoModule {
        bool loading;

        public override ChartControl ActualChart { get { return chart; } }

        public CustomFinancial2DControl() {
            InitializeComponent();
            loading = true;
            try {
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(StockSeries2D), "Stock", 1));
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(CandleStickSeries2D), "Candle Stick", 1));
                lbSeriesType.SelectedIndex = 0;
            }
            finally {
                loading = false;
            }
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void AddPoint(FinancialSeries2D series, int argument, double low, double high, double open, double close) {
            SeriesPoint point = new SeriesPoint(argument);
            FinancialSeries2D.SetLowValue(point, low);
            FinancialSeries2D.SetHighValue(point, high);
            FinancialSeries2D.SetOpenValue(point, open);
            FinancialSeries2D.SetCloseValue(point, close);
            series.Points.Add(point);
        }
        void lbSeriesType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            SeriesTypeItem item = lbSeriesType.SelectedItem as SeriesTypeItem;
            if (item != null) {
                FinancialSeries2D series = Activator.CreateInstance(item.SeriesType) as FinancialSeries2D;
                if (series != null) {
                    series.Label = new SeriesLabel();
                    DataTemplate labelTemplate = chart.Resources["labelTemplate"] as DataTemplate;
                    if (labelTemplate != null)
                        series.Label.ElementTemplate = labelTemplate;
                    series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
                    AddPoint(series, 1, 1.1, 2.9, 2.7, 1.6);
                    AddPoint(series, 2, 1.9, 2.6, 2.4, 2.1);
                    AddPoint(series, 3, 0.7, 2.4, 1.3, 2.1);
                    AddPoint(series, 4, 1.8, 2.8, 2.4, 1.9);
                    AddPoint(series, 5, 2.1, 3.4, 2.3, 3.1);
                    AddPoint(series, 6, 2.2, 3.2, 3.0, 2.6);
                    AddPoint(series, 7, 1.4, 2.7, 2.3, 2.5);
                    AddPoint(series, 8, 2.1, 3.6, 3.2, 2.7);
                    AddPoint(series, 9, 1.2, 3.1, 1.6, 2.6);
                    AddPoint(series, 10, 2.7, 4.1, 3.4, 4.0);
                    chart.Diagram.Series.Clear();
                    chart.Diagram.Series.Add(series);
                    if (!loading)
                        chart.Animate();
                }
            }
        }
        public override bool SupportSidebarContent() {
            return false;
        }
    }
}
