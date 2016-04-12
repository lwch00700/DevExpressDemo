using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomPie2DControl(.SL).xaml"),
     CodeFile("Modules/Appearance/CustomPie2DControl.xaml.(cs)")]
    public partial class CustomPie2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public CustomPie2DControl() {
            InitializeComponent();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
