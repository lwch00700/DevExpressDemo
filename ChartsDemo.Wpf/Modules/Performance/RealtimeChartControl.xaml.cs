using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Performance/RealtimeChartControl(.SL).xaml"),
     CodeFile("Modules/Performance/RealtimeChartControl.xaml.(cs)")]
    public partial class RealtimeChartControl : ChartsDemoModule {
        const int Interval = 40;

        DispatcherTimer timer = new DispatcherTimer();
        Random random = new Random();
        double value1 = 10.0;
        double value2 = -10.0;
        bool? inProcess = null;

        int TimeInterval { get { return Convert.ToInt32(seInterval.Value); } }
        public override ChartControl ActualChart { get { return chart; } }

        public RealtimeChartControl() {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(Interval);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e) {
            DateTime argument = DateTime.Now;
            series1.Points.BeginInit();
            series2.Points.BeginInit();
            SeriesPoint[] pointsToUpdate1 = new SeriesPoint[Interval];
            SeriesPoint[] pointsToUpdate2 = new SeriesPoint[Interval];
            for (int i = 0; i < Interval; i++) {
                pointsToUpdate1[i] = new SeriesPoint(argument, value1);
                pointsToUpdate2[i] = new SeriesPoint(argument, value2);
                argument = argument.AddMilliseconds(1);
                UpdateValues();
            }
            DateTime minDate = argument.AddSeconds(-TimeInterval);
            int pointsToRemoveCount = 0;
            foreach (SeriesPoint point in series1.Points)
                if (point.DateTimeArgument < minDate)
                    pointsToRemoveCount++;
            if (pointsToRemoveCount < series1.Points.Count)
                pointsToRemoveCount--;
            series1.Points.AddRange(pointsToUpdate1);
            series2.Points.AddRange(pointsToUpdate2);
            if (pointsToRemoveCount > 0) {
                RemovePointsRange(series1, pointsToRemoveCount);
                RemovePointsRange(series2, pointsToRemoveCount);
            }
            series2.Points.EndInit();
            series1.Points.EndInit();
            axisX.WholeRange.SetMinMaxValues(minDate, argument);
        }
        void RemovePointsRange(Series series, int pointsToRemoveCount) {
            for (int i = 0; i < pointsToRemoveCount; i++) {
                series.Points.RemoveAt(0);
            }
        }
        void UpdateValues() {
            value1 = CalculateNextValue(value1);
            value2 = CalculateNextValue(value2);
        }
        double CalculateNextValue(double value) {
            return value + (random.NextDouble() * 10.0 - 5.0);
        }
        void DisableProcess() {
            inProcess = timer.IsEnabled;
            timer.Stop();
        }
        void RestoreProcess() {
            if (inProcess != null) {
                timer.IsEnabled = (bool)inProcess;
                inProcess = null;
            }
        }
        void ChartsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            RestoreProcess();
        }
        void ChartsDemoModule_Unloaded(object sender, RoutedEventArgs e) {
            DisableProcess();
        }
        void ButtonPauseClick(object sender, RoutedEventArgs e) {
            timer.IsEnabled = !timer.IsEnabled;
            btnPause.Content = timer.IsEnabled ? "Pause" : "Resume";
        }
    }
}
