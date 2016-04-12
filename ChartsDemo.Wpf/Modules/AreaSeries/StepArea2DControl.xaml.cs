using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StepArea2DControl(.SL).xaml"),
     CodeFile("Modules/AreaSeries/StepArea2DControl.xaml.(cs)")]
    public partial class StepArea2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public StepArea2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(RingMarker2DModel));
            foreach (AreaStepSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Corporation: {A}\nMarket Value: {V:0.00}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
