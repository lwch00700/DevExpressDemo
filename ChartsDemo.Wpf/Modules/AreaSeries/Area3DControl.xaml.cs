using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/AreaSeries/Area3DControl(.SL).xaml"),
     CodeFile("Modules/AreaSeries/Area3DControl.xaml.(cs)")]
    public partial class Area3DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Area3DControl() {
            InitializeComponent();
        }
    }
}
