using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/LogarithmicScaleControl(.SL).xaml"),
     CodeFile("Modules/DataRepresentation/LogarithmicScaleControl.xaml.(cs)")]
    public partial class LogarithmicScaleControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public LogarithmicScaleControl() {
            InitializeComponent();
            foreach (XYSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Region: {S}\nPopulation: {V}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void AnimateLogarithmic(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
    }
}
