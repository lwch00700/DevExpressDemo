using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomMarker2DControl(.SL).xaml"),
     CodeFile("Modules/Appearance/CustomMarker2DControl.xaml.(cs)")]
    public partial class CustomMarker2DControl : ChartsDemoModule {
        bool loading;

        public override ChartControl ActualChart { get { return chart; } }

        public CustomMarker2DControl() {
            InitializeComponent();
            loading = true;
            try {
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(PointSeries2D), "2D Points", 1));
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineSeries2D), "2D Lines", 1));
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaSeries2D), "2D Areas", 1));
                lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(BubbleSeries2D), "2D Bubbles", 1));
                lbSeriesType.SelectedIndex = 0;
            }
            finally {
                loading = false;
            }
        }
        void AddPoint(MarkerSeries2D series, SeriesPoint point, double weight) {
            BubbleSeries2D bubbleSeries = series as BubbleSeries2D;
            if (bubbleSeries != null)
                BubbleSeries2D.SetWeight(point, weight);
            series.Points.Add(point);
        }
        void FillSeries(MarkerSeries2D series) {
            if (series == null)
                return;
            AreaSeries2D areaSeries = series as AreaSeries2D;
            if (areaSeries != null) {
                areaSeries.Transparency = 0.8;
                areaSeries.MarkerVisible = true;
            }
            LineSeries2D lineSeries = series as LineSeries2D;
            if (lineSeries != null)
                lineSeries.MarkerVisible = true;
            series.ColorEach = true;
            series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            AddPoint(series, new SeriesPoint(1.0, 2.1), 1.0);
            AddPoint(series, new SeriesPoint(2.0, 1.4), 2.0);
            AddPoint(series, new SeriesPoint(3.0, 1.1), 4.0);
            AddPoint(series, new SeriesPoint(4.0, 1.9), 3.0);
            AddPoint(series, new SeriesPoint(5.0, 3.1), 2.5);
            AddPoint(series, new SeriesPoint(6.0, 2.4), 1.7);
            AddPoint(series, new SeriesPoint(7.0, 2.6), 3.9);
            AddPoint(series, new SeriesPoint(8.0, 1.9), 2.8);
            AddPoint(series, new SeriesPoint(9.0, 3.2), 2.1);
            AddPoint(series, new SeriesPoint(10.0, 3.5), 1.3);
            ((ISupportMarker2D)series).MarkerModel = chart.Resources["CustomMarker2DModel"] as CustomMarker2DModel;
            series.Label = new SeriesLabel();
            series.Label.ConnectorVisible = false;
            series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            DataTemplate labelTemplate = chart.Resources["labelTemplate"] as DataTemplate;
            if (labelTemplate != null) {
                series.Label.ElementTemplate = labelTemplate;
                series.Label.RenderMode = LabelRenderMode.CustomShape;
            }
            if (!(series is BubbleSeries2D)) {
                ((ISupportMarker2D)series).MarkerSize = 19;
                series.Label.Indent = 15;
            }
            else {
                series.Label.Indent = 5;
                MarkerSeries2D.SetAngle(series.Label, 90);
                BubbleSeries2D.SetLabelPosition(series.Label, Bubble2DLabelPosition.Outside);
            }
            chart.Diagram.Series.Clear();
            chart.Diagram.Series.Add(series);
            if (!loading)
                chart.Animate();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void lbSeriesType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            SeriesTypeItem item = lbSeriesType.SelectedItem as SeriesTypeItem;
            if (item != null)
                FillSeries(Activator.CreateInstance(item.SeriesType) as MarkerSeries2D);
        }
    }
}
