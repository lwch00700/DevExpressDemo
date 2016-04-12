﻿using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/FullStackedSplineArea2DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/FullStackedSplineArea2DControl.xaml.(cs)")]
    public partial class FullStackedSplineArea2DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public FullStackedSplineArea2DControl() {
            InitializeComponent();
            foreach (AreaFullStackedSeries2D series in chart.Diagram.Series)
                series.CrosshairLabelPattern = "Architecture: {S}\nAmount: {V:0.00}";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void chbPercent_Checked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{VP:P2}";
            }
        }
        void chbPercent_UnChecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (AreaFullStackedSeries2D series in ((XYDiagram2D)chart.Diagram).Series)
                    series.Label.TextPattern = "{V:N0}";
            }
        }
    }
}
