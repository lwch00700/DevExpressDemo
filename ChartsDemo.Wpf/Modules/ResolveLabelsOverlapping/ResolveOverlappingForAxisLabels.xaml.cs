using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels(.SL).xaml"),
     CodeFile("Modules/ResolveLabelsOverlapping/ResolveOverlappingForAxisLabels.xaml.(cs)")]
    public partial class ResolveOverlappingForAxisLabels : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public ResolveOverlappingForAxisLabels() {
            InitializeComponent();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
