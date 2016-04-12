using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/BarSideBySideStacked2DControl.xaml.(cs)")]
    public partial class BarSideBySideStacked2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BarSideBySideStacked2DControl() {
            InitializeComponent();
            chart.DataSource = AgePopulationDataLoader.CreateDataSource();
            GroupSeries();
            lbModel.SelectedItem = Bar2DModelKindHelper.FindActualBar2DModelKind(typeof(SimpleBar2DModel));
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void lbGroupBy_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(chart != null) {
                GroupSeries();
                chart.Animate();
            }
        }
        void GroupSeries() {
            foreach (Series series in chart.Diagram.Series) {
                BarSideBySideStackedSeries2D stackedSeries = series as BarSideBySideStackedSeries2D;
                AgePopulation population = series.Points[0].Tag as AgePopulation;
                if (stackedSeries != null && population != null)
                    stackedSeries.StackedGroup = lbGroupBy.SelectedIndex == 0 ? population.Sex : population.Age;
            }
        }
    }
}
