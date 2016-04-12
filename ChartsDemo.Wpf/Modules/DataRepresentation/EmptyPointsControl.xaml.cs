using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/DataRepresentation/EmptyPointsControl(.SL).xaml"),
     CodeFile("Modules/DataRepresentation/EmptyPointsControl.xaml.(cs)")]
    public partial class EmptyPointsControl : ChartsDemoModule {
        bool loading = false;
        IList<WebSitePopularity> dataSource;

        public override ChartControl ActualChart { get { return chart; } }

        public EmptyPointsControl() {
            InitializeComponent();
            dataSource = CreateDataSource();
            InitSeriesListBox();
            loading = true;
            try {
                lbSeriesType.SelectedIndex = 0;
            }
            finally {
                loading = false;
            }
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void InitSeriesListBox() {
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(LineSeries2D), "2D Lines"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(SplineSeries2D), "2D Splines"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaSeries2D), "2D Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(SplineAreaSeries2D), "2D Spline Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaStackedSeries2D), "2D Stacked Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(SplineAreaStackedSeries2D), "2D Stacked Spline Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(AreaFullStackedSeries2D), "2D Full-Stacked Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram2D), typeof(SplineAreaFullStackedSeries2D), "2D Spline Full-Stacked Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram3D), typeof(AreaSeries3D), "3D Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram3D), typeof(AreaStackedSeries3D), "3D Stacked Areas"));
            lbSeriesType.Items.Add(new SeriesTypeItem(typeof(XYDiagram3D), typeof(AreaFullStackedSeries3D), "3D Full-Stacked Areas"));
        }
        void PrepareSeriesAnimation(Series series, int seriesIndex) {
            if(loading)
                series.AnimationAutoStartMode = AnimationAutoStartMode.SetStartState;
            TimeSpan beginTime = TimeSpan.FromMilliseconds(200 * seriesIndex);
            if (series is LineSeries2D) {
                ((LineSeries2D)series).PointAnimation = new Marker2DFadeInAnimation() { BeginTime = beginTime };
                ((LineSeries2D)series).SeriesAnimation = new Line2DBlowUpAnimation() { BeginTime = beginTime };
            }
            else if (series is AreaSeries2D) {
                ((AreaSeries2D)series).PointAnimation = new Marker2DFadeInAnimation() { BeginTime = beginTime };
                ((AreaSeries2D)series).SeriesAnimation = new Area2DStretchOutAnimation() { BeginTime = beginTime };
            }
            else if (series is AreaStackedSeries2D) {
                ((AreaStackedSeries2D)series).PointAnimation = new AreaStacked2DFadeInAnimation() { BeginTime = beginTime };
                ((AreaStackedSeries2D)series).SeriesAnimation = new Area2DStretchOutAnimation() { BeginTime = beginTime };
            }
        }
        void ChangeDiagram(SeriesTypeItem item) {
            if (item == null)
                return;
            chart.Diagram = (Diagram)Activator.CreateInstance(item.DiagramType);
            bool isAreaFullStackedSeries = false;
            Axis axisX = null, axisY = null;
            XYDiagram2D diagram2D = chart.Diagram as XYDiagram2D;
            if (diagram2D != null) {
                chart.Legend.HorizontalPosition = HorizontalPosition.RightOutside;
                diagram2D.AxisX = new AxisX2D();
                diagram2D.AxisY = new AxisY2D();
                axisX = diagram2D.AxisX;
                axisY = diagram2D.AxisY;
                isAreaFullStackedSeries = item.SeriesType == typeof(AreaFullStackedSeries2D);
            }
            else {
                chart.Legend.HorizontalPosition = HorizontalPosition.Right;
                XYDiagram3D diagram3D = chart.Diagram as XYDiagram3D;
                if (diagram3D != null) {
                    diagram3D.AxisX = new AxisX3D();
                    diagram3D.AxisY = new AxisY3D();
                    axisX = diagram3D.AxisX;
                    axisY = diagram3D.AxisY;
                    diagram3D.SeriesPadding = 0.5;
                    diagram3D.SeriesDistance = 1.5;
                    isAreaFullStackedSeries = item.SeriesType == typeof(AreaFullStackedSeries3D);
                }
            }
            if (axisX != null) {
                axisX.Label = new AxisLabel() { TextPattern = "{A:m}" };
            }
            if (axisY != null) {
                if (isAreaFullStackedSeries)
                    axisY.Label = new AxisLabel() { TextPattern = "{VP:P0}" };
                else {
                    axisY.Title = new AxisTitle();
                    axisY.Title.Content = "Number of visitors";
                }
            }
            Series politicsSeries = (Series)Activator.CreateInstance(item.SeriesType);
            politicsSeries.DisplayName = "Politics";
            politicsSeries.ValueDataMember = "Politics";
            PrepareSeriesAnimation(politicsSeries, 0);
            Series entertainmentSeries = (Series)Activator.CreateInstance(item.SeriesType);
            entertainmentSeries.DisplayName = "Entertainment";
            entertainmentSeries.ValueDataMember = "Entertainment";
            PrepareSeriesAnimation(entertainmentSeries, 1);
            Series travelSeries = (Series)Activator.CreateInstance(item.SeriesType);
            travelSeries.DisplayName = "Travel";
            travelSeries.ValueDataMember = "Travel";
            PrepareSeriesAnimation(travelSeries, 2);
            chart.Diagram.Series.AddRange(new Series[] { politicsSeries, entertainmentSeries, travelSeries });
            foreach (Series series in chart.Diagram.Series) {
                ISupportTransparency supportTransparency = series as ISupportTransparency;
                if (supportTransparency != null)
                    supportTransparency.Transparency = 0.5;
                series.Label = new SeriesLabel();
                if (isAreaFullStackedSeries)
                    series.Label.TextPattern = "{VP:P0}";
                series.LabelsVisibility = cheLabelsVisible.IsChecked.Value;
                series.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                series.ArgumentScaleType = ScaleType.DateTime;
                series.ArgumentDataMember = "Date";
                series.DataSource = dataSource;
            }
        }
        void lbSeriesType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                chart.BeginInit();
                try {
                    ChangeDiagram(lbSeriesType.SelectedItem as SeriesTypeItem);
                }
                finally {
                    chart.EndInit();
                }
            }
        }
        void cheLabelsVisible_Checked(object sender, RoutedEventArgs e) {
            if (chart != null && chart.Diagram != null)
                foreach (Series series in chart.Diagram.Series)
                    series.LabelsVisibility = true;
        }
        void cheLabelsVisible_Unchecked(object sender, RoutedEventArgs e) {
            if (chart != null && chart.Diagram != null)
                foreach (Series series in chart.Diagram.Series)
                    series.LabelsVisibility = false;
        }
        IList<WebSitePopularity> CreateDataSource() {
            List<WebSitePopularity> dataSource = new List<WebSitePopularity>();
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 1), 65, 56, 45));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 2), 78, 45, 40));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 3), 95, 70, 56));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 4), 110, 82, 47));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 5), 108, 80, 38));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 6), 52, 20, 31));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 7), 46, 10, 27));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 8), 70, null, 37));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 9), 86, null, 42));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 10), 92, 65, null));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 11), 108, 45, 37));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 12), 115, 56, 21));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 13), 75, 10, 10));
            dataSource.Add(new WebSitePopularity(new DateTime(2007, 1, 14), 65, 0, 5));
            return dataSource;
        }
    }

    public class WebSitePopularity {
        readonly DateTime date;
        readonly double? politics;
        readonly double? entertainment;
        readonly double? travel;

        public DateTime Date { get { return date; } }
        public double? Politics { get { return politics; } }
        public double? Entertainment { get { return entertainment; } }
        public double? Travel { get { return travel; } }

        public WebSitePopularity(DateTime date, double? politics, double? entertainment, double? travel) {
            this.date = date;
            this.politics = politics;
            this.entertainment = entertainment;
            this.travel = travel;
        }
    }
}
