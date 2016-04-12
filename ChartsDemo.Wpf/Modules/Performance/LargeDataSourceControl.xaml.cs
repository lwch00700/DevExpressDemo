using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Performance/LargeDataSourceControl(.SL).xaml"),
     CodeFile("Modules/Performance/LargeDataSourceControl.xaml.(cs)")]
    public partial class LargeDataSourceControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public LargeDataSourceControl() {
            InitializeComponent();
            LoadPoints();
        }
        void LoadPoints() {
            int pointCount = ((PointCountItem)cbPointCount.SelectedItem).PointCount;
            double value = 0;
            Random random = new Random();
            series.Points.BeginInit();
            series.Points.Clear();
            for (int i = 0; i < pointCount; i++) {
                series.Points.Add(new SeriesPoint((double)i, value));
                value += (random.NextDouble() * 10.0 - 5.0);
                if (value > 1200)
                    value -= 500;
                if (value < -1200)
                    value += 500;
            }
            series.Points.EndInit();
            axisX.VisualRange.SetMinMaxValues(3 * pointCount / 8, 5 * pointCount / 8);
            axisX.WholeRange.SetMinMaxValues(0, pointCount);
        }
        void ExecuteIdle(Action operation) {
            Dispatcher.BeginInvoke(operation, DispatcherPriority.Background);
        }
        void HideLoadingPanel() {
            loadingPanel.Visibility = Visibility.Collapsed;
        }
        void ShowLoadingPanel() {
            loadingPanel.Visibility = Visibility.Visible;
        }
        void cbPointCount_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            ShowLoadingPanel();
            ExecuteIdle(LoadPoints);
            ExecuteIdle(HideLoadingPanel);
        }
    }

    public class PointCountItem {
        int pointCount;

        public int PointCount {
            get { return pointCount; }
            set {
                if (pointCount != value)
                    pointCount = value;
            }
        }
        public override string ToString() {
            return pointCount.ToString("n0");
        }
    }
}
