using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/SideBySideRangeBar2DControl.xaml.(cs)")]
    public partial class SideBySideRangeBar2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public SideBySideRangeBar2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = RangeBar2DModelKindHelper.FindActualRangeBar2DModelKind(typeof(OutsetRangeBar2DModel));
            foreach (Series series in chart.Diagram.Series)
                series.ToolTipPointPattern = "Month: {A:MMMM}\nMin Price: ${V1:0.00}\nMax Price: ${V2:0.00}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
