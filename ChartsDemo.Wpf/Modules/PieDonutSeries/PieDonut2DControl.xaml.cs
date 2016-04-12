using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PieDonutSeries/PieDonut2DControl(.SL).xaml"),
     CodeFile("Modules/PieDonutSeries/PieDonut2DControl.xaml.(cs)")]
    public partial class PieDonut2DControl : ChartsDemoModule {
        const int clickDelta = 200;

        DateTime mouseDownTime;
        bool rotate;
        Point startPosition;

        public override ChartControl ActualChart { get { return chart; } }

        public PieDonut2DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Pie2DModelKindHelper.FindActualPie2DModelKind(typeof(SimplePie2DModel));
                Series.ToolTipPointPattern = "{A}: {V:0.0}M km²";
        }
        bool IsClick(DateTime mouseUpTime) {
            return (mouseUpTime - mouseDownTime).TotalMilliseconds < clickDelta;
        }
        double CalcAngle(Point p1, Point p2) {
            Point center = new Point(chart.Diagram.ActualWidth / 2, chart.ActualHeight / 2);
            Point relativeP1 = new Point(p1.X - center.X, p1.Y - center.Y);
            Point relativeP2 = new Point(p2.X - center.X, p2.Y - center.Y);
            double angleP1Radian = Math.Atan2(relativeP1.X, relativeP1.Y);
            double angleP2Radian = Math.Atan2(relativeP2.X, relativeP2.Y);
            double angle = (angleP2Radian - angleP1Radian) * 180 / (Math.PI * 2);
            if (angle > 90)
                angle = 180 - angle;
            else if (angle < -90)
                angle = 180 + angle;
            return angle;
        }
        void chart_MouseUp(object sender, MouseButtonEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.GetPosition(chart));
            rotate = false;
            if (hitInfo == null || hitInfo.SeriesPoint == null || !IsClick(DateTime.Now))
                return;
            double distance = PieSeries.GetExplodedDistance(hitInfo.SeriesPoint);
            Storyboard storyBoard = new Storyboard();
            DoubleAnimation animation = new DoubleAnimation();
            animation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 300));
            animation.To = distance > 0 ? 0 : 0.3;
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, hitInfo.SeriesPoint);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            storyBoard.Begin();
        }
        void chart_MouseDown(object sender, MouseButtonEventArgs e) {
            mouseDownTime = DateTime.Now;
            Point position = e.GetPosition(chart);
            ChartHitInfo hitInfo = chart.CalcHitInfo(position);
            if (hitInfo != null && hitInfo.SeriesPoint != null) {
                rotate = true;
                startPosition = position;
            }
        }
        void chart_MouseMove(object sender, MouseEventArgs e) {
            Point position = e.GetPosition(chart);
            ChartHitInfo hitInfo = chart.CalcHitInfo(position);
            if (hitInfo == null)
                return;
            if (rotate && !IsClick(DateTime.Now)) {
                PieSeries2D series = chart.Diagram.Series[0] as PieSeries2D;
                double angleDelta = CalcAngle(startPosition, position);
                if (Math.Abs(slRotation.Value + angleDelta) < 360)
                    slRotation.Value += angleDelta;
                else if (slRotation.Value + angleDelta > 360)
                    slRotation.Value = -360;
                else
                    slRotation.Value = 360;
                startPosition = position;
            }
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void rblSweepDirection_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null)
                chart.Animate();
        }
        void chart_QueryChartCursor(object sender, QueryChartCursorEventArgs e) {
            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
                e.Cursor = Cursors.Hand;
        }
    }
}
