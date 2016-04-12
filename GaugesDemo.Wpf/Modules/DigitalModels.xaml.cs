using System;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class DigitalModels : GaugesDemoModule {
        public DigitalModels() {
            InitializeComponent();
        }

        private void lbModel_SelectedIndexChanged(object sender, System.Windows.RoutedEventArgs e) {
            FourteenSegmentsGauge.Text = ((PredefinedElementKind)lbModel.SelectedItem).Name.ToUpper();
            Matrix8x14Gauge.Text = ((PredefinedElementKind)lbModel.SelectedItem).Name;
        }
    }
}
