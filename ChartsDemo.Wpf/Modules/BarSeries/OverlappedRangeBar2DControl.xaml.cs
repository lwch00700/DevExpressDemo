using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/OverlappedRangeBar2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/OverlappedRangeBar2DControl.xaml.(cs)")]
    public partial class OverlappedRangeBar2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public OverlappedRangeBar2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = RangeBar2DModelKindHelper.FindActualRangeBar2DModelKind(typeof(OutsetRangeBar2DModel));
            foreach (Series series in chart.Diagram.Series)
                series.ToolTipPointPattern = "{S}\nMonth: {A:MMMM}\nMin Price: ${V1:0.00}\nMax Price: ${V2:0.00}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
