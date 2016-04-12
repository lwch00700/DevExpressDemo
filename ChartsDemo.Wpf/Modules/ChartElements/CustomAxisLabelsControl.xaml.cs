using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/ChartElements/CustomAxisLabelsControl(.SL).xaml"),
     CodeFile("Modules/ChartElements/CustomAxisLabelsControl.xaml.(cs)")]
    public partial class CustomAxisLabelsControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public CustomAxisLabelsControl() {
            InitializeComponent();
            FillCustomAxisLabels();
            axisYLabel.TextPattern = "{A} minutes";
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void ClearCustomAxisLabels() {
            ((XYDiagram2D)chart.Diagram).AxisY.CustomLabels.Clear();
        }
        void FillCustomAxisLabels() {
            XYDiagram2D diagram = (XYDiagram2D)chart.Diagram;
            for (int i = 0; i < 1000; i += 120) {
                TimeSpan duration = TimeSpan.FromMinutes(i);
                diagram.AxisY.CustomLabels.Add(new CustomAxisLabel(i, String.Format("{0} hours", duration.Hours.ToString())));
            }
        }
        void chbEnable_Checked(object sender, RoutedEventArgs e) {
            ClearCustomAxisLabels();
            FillCustomAxisLabels();
        }
        void chbEnable_Unchecked(object sender, RoutedEventArgs e) {
            ClearCustomAxisLabels();
        }

        private void chartToolTipControler_ToolTipOpening(object sender, ChartToolTipEventArgs e) {
            e.Hint = GetHoursAndMinutesAsString(e.SeriesPoint.Value);
        }
        string GetHoursAndMinutesAsString(double flyDuration) {
            const int minutesInHour = 60;
            string hours = ((int)flyDuration / minutesInHour).ToString("D2");
            string minutes = ((int)(flyDuration % minutesInHour)).ToString("D2");
            return hours + ":" + minutes;
        }
    }
}
