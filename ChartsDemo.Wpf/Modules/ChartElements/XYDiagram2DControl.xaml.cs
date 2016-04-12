using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/XYDiagram2DControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/XYDiagram2DControl.xaml.(cs)")]
    public partial class XYDiagram2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public XYDiagram2DControl() {
            InitializeComponent();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
