using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/StackedLine2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/StackedLine2DControl.xaml.(cs)")]
    public partial class StackedLine2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public StackedLine2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(CrossMarker2DModel));
            foreach (LineStackedSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Year: {S}\nGSP: {V:0.000}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
