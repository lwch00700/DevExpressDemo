using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarLineSeriesControl.xaml.(cs)")]
    public partial class PolarLineSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public PolarLineSeriesControl() {
            InitializeComponent();
            chart.DataSource = FunctionsPointGenerator.GeneratePoints(CircularFunction.Lemniskate);
        }
        void lbFunction_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            CircularFunction f;
            if ((string)lbFunction.SelectedItem == "Taubin's Heart")
                f = CircularFunction.TaubinsHeart;
            else if ((string)lbFunction.SelectedItem == "Cardioid")
                f = CircularFunction.Cardioid;
            else
                f = CircularFunction.Lemniskate;
            chart.DataSource = FunctionsPointGenerator.GeneratePoints(f);
            chart.Animate();
        }
    }
}
