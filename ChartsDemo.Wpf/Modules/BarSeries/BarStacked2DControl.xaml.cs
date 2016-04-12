using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarStacked2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/BarStacked2DControl.xaml.(cs)")]
    public partial class BarStacked2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BarStacked2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Bar2DModelKindHelper.FindActualBar2DModelKind(typeof(FlatGlassBar2DModel));
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
