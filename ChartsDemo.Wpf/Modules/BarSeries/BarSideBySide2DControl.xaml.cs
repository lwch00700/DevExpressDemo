using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySide2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/BarSideBySide2DControl.xaml.(cs)")]
    public partial class BarSideBySide2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BarSideBySide2DControl() {
            InitializeComponent();
            foreach (BarSideBySideSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Year: {S}\nGSP: {V:0.000}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
