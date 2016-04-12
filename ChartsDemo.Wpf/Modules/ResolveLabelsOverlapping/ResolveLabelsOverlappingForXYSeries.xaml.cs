using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries(.SL).xaml"),
     CodeFile("Modules/ResolveLabelsOverlapping/ResolveLabelsOverlappingForXYSeries.xaml.(cs)")]
    public partial class ResolveLabelsOverlappingForXYSeries : ChartsDemoModule {
        SeriesLabel Label { get { return chart != null ? chart.Diagram.Series[0].Label : null; } }
        XYDiagram2D Diagram { get { return chart != null ? chart.Diagram as XYDiagram2D : null; } }
        public override ChartControl ActualChart { get { return chart; } }

        public ResolveLabelsOverlappingForXYSeries() {
            InitializeComponent();
            ResolveOverlappingModeHelper.PrepareListBox(lbMode, 4);
            UpdateControls();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void UpdateControls() {
            if (Label != null) {
                gfIndent.IsEnabled = Label.ResolveOverlappingMode != ResolveOverlappingMode.None;
                gfAngle.IsEnabled = Label.ResolveOverlappingMode != ResolveOverlappingMode.JustifyAllAroundPoint;
            }
        }
        void lbMode_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (Label != null)
                Label.ResolveOverlappingMode = ResolveOverlappingModeHelper.GetMode(lbMode);
            UpdateControls();
        }
    }
}
