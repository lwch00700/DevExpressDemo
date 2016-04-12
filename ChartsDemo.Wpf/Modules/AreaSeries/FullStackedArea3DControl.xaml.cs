using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/FullStackedArea3DControl(.SL).xaml"),
     CodeFile("Modules/AreaSeries/FullStackedArea3DControl.xaml.(cs)")]
    public partial class FullStackedArea3DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public FullStackedArea3DControl() {
            InitializeComponent();
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = true;
                chbPercent.IsEnabled = true;
            }
        }
        void chbVisible_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = false;
                chbPercent.IsEnabled = false;
            }
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.Label.TextPattern = "{VP:P2}";
            }
        }
        void chbPercent_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.Label.TextPattern = "{V:N0}";
            }
        }
    }
}
