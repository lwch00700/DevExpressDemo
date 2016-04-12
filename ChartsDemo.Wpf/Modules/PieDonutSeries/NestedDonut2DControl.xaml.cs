using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/NestedDonut2DControl(.SL).xaml"),
     CodeFile("Modules/PieDonutSeries/NestedDonut2DControl.xaml.(cs)")]
    public partial class NestedDonut2DControl : ChartsDemoModule {
        readonly NestedDonutDemoViewModel viewModel = new NestedDonutDemoViewModel();

        public override ChartControl ActualChart { get { return chart; } }

        public NestedDonut2DControl() {
            InitializeComponent();
            DataContext = viewModel;
        }
        void ChartControlBoundDataChanged(object sender, RoutedEventArgs e) {
            ChartControl chartControl = sender as ChartControl;
            if (chartControl != null)
                viewModel.UpdateSeries(chartControl.Diagram.Series);
        }
    }

    public class NestedDonutDemoViewModel : INotifyPropertyChanged {
        const string defaultArgumentDataMember = "Age";

        string argumentDataMember = defaultArgumentDataMember;
        object dataSource = null;

        bool IsDefaultDataMember { get { return argumentDataMember == defaultArgumentDataMember; } }
        public string ArgumentDataMember {
            get { return argumentDataMember; }
            set {
                if (argumentDataMember != value) {
                    argumentDataMember = value;
                    OnPropertyChanged("DemoTitle");
                    OnPropertyChanged("SeriesDataMember");
                    OnPropertyChanged("ArgumentDataMember");
                    OnPropertyChanged("HintDataMember");
                }
            }
        }
        public string SeriesDataMember { get { return "Country" + HintDataMember + "Key"; } }
        public string DemoTitle { get { return "Population: " + argumentDataMember + " Structure"; } }
        public string HintDataMember { get { return IsDefaultDataMember ? "Sex" : "Age"; } }
        public object DataSource {
            get {
                if (dataSource == null)
                    dataSource = AgePopulationDataLoader.CreateDataSource();
                return dataSource;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void UpdateSeries(SeriesCollection seriesCollection) {
            if (seriesCollection.Count > 0) {
                foreach (Series series in seriesCollection) {
                    series.ShowInLegend = false;
                    NestedDonutSeries2D nestedSeries = series as NestedDonutSeries2D;
                    AgePopulation population = series.Points[0].Tag as AgePopulation;
                    if (population != null && nestedSeries != null) {
                        string name = population.Name;
                        nestedSeries.Group = name;
                        nestedSeries.Titles.Clear();
                        nestedSeries.Titles.Add(new Title() { Content = name, HorizontalAlignment = HorizontalAlignment.Center, Visible = true });
                    }
                }
                seriesCollection[0].ShowInLegend = true;
            }
        }
    }
}
