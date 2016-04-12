using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarAreaSeriesControl.xaml.(cs)")]
    public partial class RadarAreaSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public RadarAreaSeriesControl() {
            InitializeComponent();
            series.ToolTipPointPattern = "Direction: {A}\nSpeed: {V}";
        }
    }
}
