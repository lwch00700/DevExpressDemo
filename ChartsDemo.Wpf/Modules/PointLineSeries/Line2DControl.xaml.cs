using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Line2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/Line2DControl.xaml.(cs)")]
    public partial class Line2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Line2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(CrossMarker2DModel));
            foreach (LineSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Region: {S}\nPopulation: {V}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
