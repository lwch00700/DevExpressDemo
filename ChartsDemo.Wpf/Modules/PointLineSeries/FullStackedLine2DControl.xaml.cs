using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/FullStackedLine2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/FullStackedLine2DControl.xaml.(cs)")]
    public partial class FullStackedLine2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public FullStackedLine2DControl() {
            InitializeComponent();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(CrossMarker2DModel));
            foreach (LineFullStackedSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Architecture: {S}\nAmount: {V}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (LineFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{VP:P0}";
            }
        }
        void chbPercent_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (LineFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{V:N0}";
            }
        }
    }
}
