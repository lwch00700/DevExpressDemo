using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Appearance/CustomBar3DControl(.SL).xaml"),
     CodeFile("Modules/Appearance/CustomBar3DControl.xaml.(cs)")]
    public partial class CustomBar3DControl : ChartsDemoModule {
        const int clickDelta = 200;
        const string chair = "Chair";

        int mouseDownTime;
        bool isLeftMouseButtonReleased = true;
        Storyboard initialAnimation;
        Storyboard seriesPointAnimationStoryboard;

        public override ChartControl ActualChart { get { return chart; } }

        public CustomBar3DControl() {
            InitializeComponent();
   initialAnimation = TryFindResource("InitialAnimation") as Storyboard;
   seriesPointAnimationStoryboard = TryFindResource("SeriesPointAnimationStoryboard") as Storyboard;
            if (seriesPointAnimationStoryboard != null && seriesPointAnimationStoryboard.Children.Count > 0)
                Storyboard.SetTarget(seriesPointAnimationStoryboard.Children[0], SeriesPointAnimationRecord);
  }
        List<SeriesPoint> GetPencilPoints() {
            List<SeriesPoint> points = new List<SeriesPoint>();
            points.Add(new SeriesPoint(1, 65));
            points.Add(new SeriesPoint(2, 78));
            points.Add(new SeriesPoint(3, 95));
            points.Add(new SeriesPoint(4, 110));
            points.Add(new SeriesPoint(5, 108));
            points.Add(new SeriesPoint(6, 52));
            points.Add(new SeriesPoint(7, 46));
            return points;
        }
        List<SeriesPoint> GetChairPoints() {
            List<SeriesPoint> points = new List<SeriesPoint>();
            points.Add(new SeriesPoint(1, 55));
            points.Add(new SeriesPoint(2, 70));
            points.Add(new SeriesPoint(3, 40));
            return points;
        }
        bool IsClick(int mouseUpTime) {
            return mouseUpTime - mouseDownTime < clickDelta;
        }
        void chart_MouseDown(object sender, MouseButtonEventArgs e) {
            mouseDownTime = e.Timestamp;
            isLeftMouseButtonReleased = false;
        }
        void chart_MouseUp(object sender, MouseButtonEventArgs e) {
            isLeftMouseButtonReleased = true;
            if (seriesPointAnimationStoryboard == null || SeriesPointAnimation == null || !IsClick(e.Timestamp))
                return;
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.GetPosition(chart));
            if (hitInfo != null && hitInfo.SeriesPoint != null) {
                SeriesPointAnimation.TargetSeriesPoint = hitInfo.SeriesPoint;
                seriesPointAnimationStoryboard.Begin(chart);
            }
        }
        void ChangeModel(ListBoxItem selectedItem) {
            if (selectedItem == null)
                return;
            string modelName = (string)selectedItem.Content;
            CustomBar3DModel model = TryFindResource(modelName) as CustomBar3DModel;
            if (model == null)
                return;
            series.Points.Clear();
            series.DisplayName = modelName;
            series.Model = model;
            if (modelName == chair) {
                series.Points.AddRange(GetChairPoints());
                series.BarWidth = 0.9;
                diagram.SeriesPadding = 0.5;
            }
            else {
                series.Points.AddRange(GetPencilPoints());
                series.BarWidth = 0.45;
                diagram.SeriesPadding = 0.3;
            }
        }
        void lbModel_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (series != null)
                ChangeModel(lbModel.SelectedItem as ListBoxItem);
        }
        void chart_Loaded(object sender, RoutedEventArgs e) {
            ChangeModel(lbModel.SelectedItem as ListBoxItem);
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null && isLeftMouseButtonReleased)
                e.Cursor = Cursors.Hand;
        }
    }
}
