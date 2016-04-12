using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/PanesControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/PanesControl.xaml.(cs)")]
    public partial class PanesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public PanesControl() {
            InitializeComponent();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
