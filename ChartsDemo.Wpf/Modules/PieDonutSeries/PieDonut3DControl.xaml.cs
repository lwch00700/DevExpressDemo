using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/PieDonut3DControl(.SL).xaml"),
     CodeFile("Modules/PieDonutSeries/PieDonut3DControl.xaml.(cs)")]
    public partial class PieDonut3DControl : ChartsDemoModule {
        const int clickDelta = 200;

        bool isLeftMouseButtonReleased = true;
        int mouseDownTime;

        public override ChartControl ActualChart { get { return chart; } }

        public PieDonut3DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Pie3DModelKindHelper.FindActualPie3DModelKind(((PieSeries3D)chart.Diagram.Series[0]).ActualModel);
            Series.ToolTipPointPattern = "{A}: {V:0.0}M km²";
        }
        bool IsClick(int mouseUpTime) {
            return mouseUpTime - mouseDownTime < clickDelta;
        }
        void lbModel_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Pie3DKind modelKind = lbModel.SelectedItem as Pie3DKind;
            if (modelKind != null) {
                Type type = modelKind.Type;
                Pie3DModelKindHelper.SetModel(chart, (Pie3DModel)Activator.CreateInstance(type));
                PieSeries3D series = (PieSeries3D)chart.Diagram.Series[0];
                series.HoleRadiusPercent = type.Name.StartsWith("Semi") ? 0 : 50;
                series.DepthTransform = type.Name.StartsWith("Semi") ? 0.5 : 1;
            }
        }
        void chart_MouseDown(object sender, MouseButtonEventArgs e) {
            mouseDownTime = e.Timestamp;
            isLeftMouseButtonReleased = false;
        }
        void chart_MouseUp(object sender, MouseButtonEventArgs e) {
            isLeftMouseButtonReleased = true;
            if (!IsClick(e.Timestamp))
                return;
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.GetPosition(chart));
            if (hitInfo == null || hitInfo.SeriesPoint == null)
                return;
            double distance = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint);
            AnimationTimeline animation = distance > 0 ? (AnimationTimeline)TryFindResource("CollapseAnimation") : (AnimationTimeline)TryFindResource("ExplodeAnimation");
            Storyboard storyBoard = new Storyboard();
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, hitInfo.SeriesPoint);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            storyBoard.Begin();
        }
        void chbVisible_Unchecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                chart.Diagram.Series[0].LabelsVisibility = false;
                lbPosition.IsEnabled = false;
            }
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            chart.Diagram.Series[0].LabelsVisibility = true;
            lbPosition.IsEnabled = true;
        }
        void lbPosition_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null)
                PieSeries.SetLabelPosition(Series.Label, (PieLabelPosition)lbPosition.SelectedItem);
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null && isLeftMouseButtonReleased)
                e.Cursor = Cursors.Hand;
        }
    }
}
