using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarLineSeriesControl.xaml.(cs)")]
    public partial class RadarLineSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public RadarLineSeriesControl() {
            InitializeComponent();
            series.ToolTipPointPattern = "Direction: {A}\nSpeed: {V}";
        }
    }
}
