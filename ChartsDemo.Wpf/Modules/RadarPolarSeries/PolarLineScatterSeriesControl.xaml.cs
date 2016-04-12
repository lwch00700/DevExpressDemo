using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/PolarLineScatterSeriesControl.xaml.(cs)")]
    public partial class PolarLineScatterSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public PolarLineScatterSeriesControl() {
            InitializeComponent();
            chart.DataSource = FunctionsPointGenerator.GenerateDegreeScatterPoints(ScatterCircularFunction.ArchimedeanSpiral);
        }
        void lbFunction_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ScatterCircularFunction function;
            switch (lbFunction.SelectedIndex) {
                case 0:
                    function = ScatterCircularFunction.ArchimedeanSpiral;
                    break;
                case 1:
                    function = ScatterCircularFunction.Rose;
                    break;
                case 2:
                default:
                    function = ScatterCircularFunction.Folium;
                    break;
            }
            chart.DataSource = FunctionsPointGenerator.GenerateDegreeScatterPoints(function);
            chart.Animate();
        }
    }
}
