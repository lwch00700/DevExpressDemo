using System;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Point3DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/Point3DControl.xaml.(cs)")]
    public partial class Point3DControl : ChartsDemoModule {

        public override ChartControl ActualChart { get { return chart; } }

        public Point3DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Marker3DModelKindHelper.FindActualMarker3DModelKind(((PointSeries3D)chart.Diagram.Series[0]).ActualModel);
            foreach (Series series in chart.Diagram.Series)
                series.ToolTipPointPattern = "Year: {s}\nState: {A}\nGSP: {V:0.00}";
  }

        void lbModel_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Marker3DKind markerKind = lbModel.SelectedItem as Marker3DKind;
            if (markerKind != null)
                Marker3DModelKindHelper.SetModel(chart, (Marker3DModel)Activator.CreateInstance(markerKind.Type));
        }
        void lbPosition_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null) {
                foreach (MarkerSeries3D series in chart.Diagram.Series)
                    MarkerSeries3D.SetLabelPosition(series.Label, (Marker3DLabelPosition)lbPosition.SelectedItem);
            }
        }
        void chart_Loaded(object sender, RoutedEventArgs e) {
            lbPosition.SelectedIndex = (int)MarkerSeries3D.GetLabelPosition(chart.Diagram.Series[0].Label);
        }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {

            if (chart != null) {
                chart.Diagram.Series[0].LabelsVisibility = true;
                lbPosition.IsEnabled = true;
            }
        }
        void chbVisible_Unchecked(object sender, RoutedEventArgs e) {
            if (chart != null) {
                chart.Diagram.Series[0].LabelsVisibility = false;
                lbPosition.IsEnabled = false;
            }
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            foreach (PointSeries3D series in chart.Diagram.Series) {
                Storyboard markerSizeAnimation = series.TryFindResource("MarkerSizeAnimation") as Storyboard;
                if (markerSizeAnimation != null)
                    markerSizeAnimation.Begin(series);
            }
        }
        void slMarkerSize_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            foreach (PointSeries3D series in chart.Diagram.Series)
                series.MarkerSize = (double)e.NewValue;
        }
        void MarkerSizeAnimationAnimation_Completed(object sender, EventArgs e) {
            foreach (PointSeries3D series in chart.Diagram.Series)
                series.MarkerSize = slMarkerSize.Value;
        }
    }
}
