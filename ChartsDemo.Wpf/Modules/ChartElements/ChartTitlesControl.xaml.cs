using System;
using System.Windows;
using System.Windows.Media;
using DevExpress.Xpf.Charts;
using System.Diagnostics;
using System.Windows.Documents;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/ChartTitlesControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/ChartTitlesControl.xaml.(cs)")]
    public partial class ChartTitlesControl : ChartsDemoModule {
        const int pointsCount = 40;

        public override ChartControl ActualChart { get { return chart; } }

        public ChartTitlesControl() {
            InitializeComponent();
            CreatePoints(chart.Diagram.Series[0]);
        }
        void ChartsDemoModule_ModuleAppear(object sender, System.Windows.RoutedEventArgs e) {
            chart.Animate();
        }
        void CreatePoints(Series series) {
            Random random = new Random();
            for (int i = 0; i < pointsCount; i++)
                series.Points.Add(new SeriesPoint(i, random.NextDouble() + 1));
        }
        void Hyperlink_Click(object sender, System.Windows.RoutedEventArgs e) {
            Hyperlink source = sender as Hyperlink;
            if (source != null) {
                Process.Start(source.NavigateUri.ToString());
            }
        }

        protected override Size ArrangeOverride(Size finalSize) {
            RectangleGeometry clipGeometry = new RectangleGeometry();
            clipGeometry.Rect = new Rect(0, 0, finalSize.Width, finalSize.Height);
            Clip = clipGeometry;
            return base.ArrangeOverride(finalSize);
        }
    }
}
