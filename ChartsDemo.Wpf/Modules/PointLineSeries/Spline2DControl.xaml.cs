using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Spline2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/Spline2DControl.xaml.(cs)")]
    public partial class Spline2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Spline2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(CrossMarker2DModel));
            foreach (SplineSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Date: {A:d}\nCents per Gallon: {V:0.0}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
