using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/BarSideBySideFullStacked2DControl.xaml.(cs)")]
    public partial class BarSideBySideFullStacked2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BarSideBySideFullStacked2DControl() {
            InitializeComponent();
            chart.DataSource = AgePopulationDataLoader.CreateDataSource();
            GroupSeries();
            lbModel.SelectedItem = Bar2DModelKindHelper.FindActualBar2DModelKind(typeof(SimpleBar2DModel));
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            if(chart != null) {
                foreach(BarFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{VP:P0}";
            }
        }
        void chbPercent_Unchecked(object sender, RoutedEventArgs e) {
            if(chart != null) {
                foreach(BarFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{V:F3}";
            }
        }
        void lbGroupBy_SelectiedIndexChanged(object sender, RoutedEventArgs e) {
            if(chart != null) {
                GroupSeries();
                chart.Animate();
            }
        }
        void GroupSeries() {
            foreach (Series series in chart.Diagram.Series) {
                BarSideBySideFullStackedSeries2D stackedSeries = series as BarSideBySideFullStackedSeries2D;
                AgePopulation population = series.Points[0].Tag as AgePopulation;
                if (stackedSeries != null && population != null)
                    stackedSeries.StackedGroup = lbGroupBy.SelectedIndex == 0 ? population.Sex : population.Age;
            }
        }
    }
}
