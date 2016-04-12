using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/RadarPointSeriesControl.xaml.(cs)")]
    public partial class RadarPointSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public RadarPointSeriesControl() {
            InitializeComponent();
            seriesDayTemperature.ToolTipPointPattern = "Date: {A:D}\nTemperature: {V}";
            seriesNightTempertaure.ToolTipPointPattern = "Date: {A:D}\nTemperature: {V}";
        }
    }
}
