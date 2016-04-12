using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/Area2DControl(.SL).xaml"),
     CodeFile("Modules/AreaSeries/Area2DControl.xaml.(cs)")]
    public partial class Area2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Area2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(RingMarker2DModel));
            foreach (AreaSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Year: {S}\nMarket Value: {V:0.00}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
