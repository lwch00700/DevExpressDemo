using System;
using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/BarSeries/BarSideBySide3DControl(.SL).xaml"),
     CodeFile("Modules/BarSeries/BarSideBySide3DControl.xaml.(cs)")]
    public partial class BarSideBySide3DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public BarSideBySide3DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Bar3DModelKindHelper.FindActualBar3DModelKind(((BarSeries3D)chart.Diagram.Series[0]).ActualModel);
  }
        void chbVisible_Checked(object sender, RoutedEventArgs e) {
            if (chart != null)
                foreach (BarSideBySideSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = true;
        }
        void chbVisible_Unchecked(object sender, RoutedEventArgs e) {
            if (chart != null)
                foreach (BarSideBySideSeries3D series in ((XYDiagram3D)chart.Diagram).Series)
                    series.LabelsVisibility = false;
        }
        void lbModel_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Bar3DKind barKind = lbModel.SelectedItem as Bar3DKind;
            if(barKind != null)
                Bar3DModelKindHelper.SetModel(chart, (Bar3DModel)Activator.CreateInstance(barKind.Type));
        }
    }
}
