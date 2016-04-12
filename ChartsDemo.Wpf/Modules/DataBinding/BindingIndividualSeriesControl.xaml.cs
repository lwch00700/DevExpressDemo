using System.Collections.ObjectModel;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataBinding/BindingIndividualSeriesControl(.SL).xaml"),
     CodeFile("Modules/DataBinding/BindingIndividualSeriesControl.xaml.(cs)")]
    public partial class BindingIndividualSeriesControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BindingIndividualSeriesControl() {
            InitializeComponent();
            series.ToolTipPointPattern = "X = {A}\nY = {V}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }

    }
    public class PointCollection : ObservableCollection<Point> {
    }
}
