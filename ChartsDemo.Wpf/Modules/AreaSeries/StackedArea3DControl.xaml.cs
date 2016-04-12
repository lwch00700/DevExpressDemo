using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/StackedArea3DControl(.SL).xaml"),
     CodeFile("Modules/AreaSeries/StackedArea3DControl.xaml.(cs)")]
    public partial class StackedArea3DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public StackedArea3DControl() {
            InitializeComponent();
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            if (chart != null)
                foreach (AreaStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = true;
        }
        void chbVisible_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null)
                foreach (AreaStackedSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = false;
        }
    }
}
