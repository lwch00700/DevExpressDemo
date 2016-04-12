using System;
using System.Windows;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/PointLineSeries/Bubble3DControl(.SL).xaml"),
     CodeFile("Modules/PointLineSeries/Bubble3DControl.xaml.(cs)")]
    public partial class Bubble3DControl : ChartsDemoModule {
        public override ChartControl ActualChart { get { return chart; } }

        public Bubble3DControl() {
            InitializeComponent();
            lbModel.SelectedItem = Marker3DModelKindHelper.FindActualMarker3DModelKind(((BubbleSeries3D)chart.Diagram.Series[0]).ActualModel);
            Series.ToolTipPointPattern = "Industry: {A}\nEstimated Number of Cases: {V}\nMorbidity Rate: {W}";
        }
        void lbPosition_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if (chart != null && lbPosition.SelectedItem != null)
                foreach (MarkerSeries3D series in chart.Diagram.Series)
                    MarkerSeries3D.SetLabelPosition(series.Label, (Marker3DLabelPosition)lbPosition.SelectedItem);
        }
        void lbModel_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            Marker3DKind markerKind = lbModel.SelectedItem as Marker3DKind;
            if(markerKind != null)
                Marker3DModelKindHelper.SetModel(chart, (Marker3DModel)Activator.CreateInstance(markerKind.Type));
        }
        void Bubble3DDemo_ModuleAppear(object sender, RoutedEventArgs e) {
            Storyboard sizeAnimation = Series.TryFindResource("SizeAnimationStoryboard") as Storyboard;
            if (sizeAnimation != null)
                sizeAnimation.Begin(Series);
        }
        void slMaxSize_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ((BubbleSeries3D)chart.Diagram.Series[0]).MaxSize = (double)e.NewValue;
        }
        void slMinSize_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ((BubbleSeries3D)chart.Diagram.Series[0]).MinSize = (double)e.NewValue;
        }
        void Storyboard_Completed(object sender, EventArgs e) {
            ((BubbleSeries3D)chart.Diagram.Series[0]).MaxSize = slMaxSize.Value;
            ((BubbleSeries3D)chart.Diagram.Series[0]).MinSize = slMinSize.Value;
        }
    }
}
