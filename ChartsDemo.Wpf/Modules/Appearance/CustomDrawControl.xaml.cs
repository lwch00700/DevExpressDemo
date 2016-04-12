using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomDrawControl(.SL).xaml"),
     CodeFile("Modules/Appearance/CustomDrawControl.xaml.(cs)")]
    public partial class CustomDrawControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public CustomDrawControl() {
            InitializeComponent();
            InitSeries();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void InitSeries() {
            Random random = new Random();
            chart.BeginInit();
            try {
                if (chart != null && chart.Diagram.Series.Count > 0) {
                    Series series = chart.Diagram.Series[0];
                    series.Points.Clear();
                    for (int i = 0; i < 20; i++) {
                        double randomValue = Math.Round(random.NextDouble() * 3, 1);
                        double value = randomValue == 0 ? 0.1 : randomValue;
                        series.Points.Add(new SeriesPoint(i, value));
                    }
                }
            }
            finally {
                chart.EndInit();
            }
        }
        void chart_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e) {
            if ((bool)chbCustomDraw.IsChecked) {
                string color = CorrectDrawOptions(e.SeriesPoint.Value, e.DrawOptions);
                if (!String.IsNullOrEmpty(color))
                    e.LabelText = color + ": " + e.LabelText;
            }
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            InitSeries();
            chart.Animate();
        }
        void chbCustomDraw_Checked(object sender, RoutedEventArgs e) {
            if (chart != null)
                chart.UpdateData();
        }
        void chbCustomDraw_Unchecked(object sender, RoutedEventArgs e) {
            if (chart != null)
                chart.UpdateData();
        }
        string CorrectDrawOptions(double val, DrawOptions drawOptions) {
            if (drawOptions == null)
                return "";
            if (val < 1) {
                drawOptions.Color = Color.FromArgb(0xFF, 0x51, 0x89, 0x03);
                return "Green";
            }
            else if (val < 2) {
                drawOptions.Color = Color.FromArgb(0xFF, 0xF9, 0xAA, 0x0F);
                return "Yellow";
            }
            else {
                drawOptions.Color = Color.FromArgb(0xFF, 0xC7, 0x39, 0x0C);
                return "Red";
            }
        }
    }
}
