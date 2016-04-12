using System.Windows;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace ChartsDemo {
    [CodeFile("Modules/RadarPolarSeries/RangeArea2DControl(.SL).xaml"),
     CodeFile("Modules/RadarPolarSeries/RangeArea2DControl.xaml.(cs)")]
    public partial class RangeArea2DControl : ChartsDemoModule {
        static string[] predefinedSizes = new string[] { "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30"};

        public override ChartControl ActualChart { get { return chart; } }

        public RangeArea2DControl() {
            InitializeComponent();
            InitializeMarkersSizeComboBox();
            RangeArea2DHelper.PrepareComboBox(cbeLabelKind, 1);
            rangeArea.LabelValueSeparator = "\n";
            rangeArea.CrosshairLabelPattern = "Month: {A:MMMM}\n2007: {V1:0.00}%\n2008: {V2:0.00}%";
        }
        void InitializeMarkersSizeComboBox() {
            cbeMarker1Size.Items.AddRange(predefinedSizes);
            cbeMarker2Size.Items.AddRange(predefinedSizes);
        }
        void ChartsDemoModule_ModuleAppear(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void cbeLabelKind_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            RangeAreaLabelKind labelKind = RangeArea2DHelper.GetMode(sender as ComboBoxEdit);
            RangeAreaSeries2D.SetLabelKind(rangeArea.Label, labelKind);
        }
    }
}
