using System;
using System.Globalization;
using System.Windows;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/PointScatter2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/PointScatter2DControl.xaml.(cs)")]
    public partial class PointScatter2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public PointScatter2DControl() {
            InitializeComponent();
            CreateData();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(RingMarker2DModel));
            slMarkerSize.Value = 20;
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void CreateData() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/Movies.xml");
            if (document != null) {
                foreach (XElement element in document.Element("Movies").Elements()) {
                    PointSeries2D series = new PointSeries2D();
                    series.DisplayName = element.Element("Name").Value;
                    series.ArgumentScaleType = ScaleType.Numerical;
                    series.ToolTipPointPattern = "{S}\nProduction budget: {A}\nWorldwide grosses: {V}";
                    series.LabelsVisibility = true;
                    series.Label = new SeriesLabel();
                    series.Label.TextPattern = "{S}";
                    series.Label.ConnectorVisible = false;
                    series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                    series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
                    double argument = Convert.ToDouble(element.Element("ProductionBudget").Value, CultureInfo.InvariantCulture);
                    double value = Convert.ToDouble(element.Element("WorlwideGrosses").Value, CultureInfo.InvariantCulture);
                    series.Points.Add(new SeriesPoint(argument, value));
                    chart.Diagram.Series.Add(series);
                }
            }
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            chart.BeginInit();
            foreach (PointSeries2D series in chart.Diagram.Series)
                series.LabelsVisibility = !series.LabelsVisibility;
            chart.EndInit();
        }
        void slMarkerSize_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            chart.BeginInit();
            foreach (PointSeries2D series in chart.Diagram.Series) {
                series.MarkerSize = Convert.ToInt32(e.NewValue);
                series.Label.Indent = Convert.ToInt32(series.MarkerSize / 2 + 6);
            }
            chart.EndInit();
        }
        void slAngle_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            chart.BeginInit();
            foreach (PointSeries2D series in chart.Diagram.Series)
                MarkerSeries2D.SetAngle(series.Label, (double)e.NewValue);
            chart.EndInit();
        }
        void lbMarker_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Marker2DKind markerKind = lbMarker.SelectedItem as Marker2DKind;
            if (markerKind != null)
                foreach (PointSeries2D series in chart.Diagram.Series)
                    series.MarkerModel = Activator.CreateInstance(markerKind.Type) as Marker2DModel;
        }
    }
}
