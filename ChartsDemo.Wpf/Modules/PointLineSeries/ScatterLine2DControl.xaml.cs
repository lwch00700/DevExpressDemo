using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/ScatterLine2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/ScatterLine2DControl.xaml.(cs)")]
    public partial class ScatterLine2DControl : ChartsDemoModule {
        const int a = 10;

        bool loading = true;

        public override ChartControl ActualChart { get { return chart; } }

        public ScatterLine2DControl() {
            InitializeComponent();
            CreateArchimedianSpiralPoints();
            CreateCardioidPoints();
            CreateCartesianFoliumPoints();
            lbMarker.SelectedItem = Marker2DModelKindHelper.FindActualMarker2DModelKind(typeof(CircleMarker2DModel));
            lbFunctionKind.SelectedIndex = 0;
            foreach (XYSeries2D series in ActualChart.Diagram.Series)
                series.CrosshairLabelPattern = "{A:F2} : {V:F2}";
        }

        void CreateArchimedianSpiralPoints() {
            for (int i = 0; i < 720; i += 15) {
                double t = (double)i / 180 * Math.PI;
                double x = t * Math.Cos(t);
                double y = t * Math.Sin(t);
                ArchimedianSpiral.Points.Add(new SeriesPoint(x, y));
            }
        }
        void CreateCardioidPoints() {
            for (int i = 0; i < 360; i += 10) {
                double t = (double)i / 180 * Math.PI;
                double x = a * (2 * Math.Cos(t) - Math.Cos(2 * t));
                double y = a * (2 * Math.Sin(t) - Math.Sin(2 * t));
                Cardioid.Points.Add(new SeriesPoint(x, y));
            }
        }
        void CreateCartesianFoliumPoints() {
            for (int i = -30; i < 125; i += 5) {
                double t = Math.Tan((double)i / 180 * Math.PI);
                double x = 3 * (double)a * t / (t * t * t + 1);
                double y = x * t;
                CartesianFolium.Points.Add(new SeriesPoint(x, y));
            }
        }
        void ShowSeries(Series visibleSeries) {
            foreach (Series series in chart.Diagram.Series)
                series.Visible = series == visibleSeries;
            if(!loading)
                chart.Animate();
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            loading = false;
            chart.Animate();
        }
        void lbFunctionKind_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (lbFunctionKind.SelectedIndex == 0)
                ShowSeries(ArchimedianSpiral);
            else if (lbFunctionKind.SelectedIndex == 1)
                ShowSeries(Cardioid);
            else if (((ListBoxEdit)sender).SelectedIndex == 2)
                ShowSeries(CartesianFolium);
        }
    }
}
