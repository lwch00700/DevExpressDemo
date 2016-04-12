using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarLineScatterSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarLineScatterSeriesControl.xaml.(cs)")]
    public partial class RadarLineScatterSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public RadarLineScatterSeriesControl() {
            InitializeComponent();
            chart.DataSource = FunctionsPointGenerator.GenerateRadianScatterPoints(ScatterCircularFunction.ArchimedeanSpiral);
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
            chart.DataSource = FunctionsPointGenerator.GenerateRadianScatterPoints(function);
            chart.Animate();
        }
    }
}
