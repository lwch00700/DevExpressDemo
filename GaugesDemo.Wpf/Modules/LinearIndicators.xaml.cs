using System;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class LinearIndicators : GaugesDemoModule {
        public LinearIndicators() {
            InitializeComponent();
        }

        void ShowIndicatorListBoxEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            foreach (LinearScale scale in linearGaugeControl.Scales) {
                scale.LevelBars[0].Visible = (string)showIndicatorListBoxEdit.SelectedItem == "Level Bar";
                scale.Markers[0].Visible = (string)showIndicatorListBoxEdit.SelectedItem == "Marker";
                scale.RangeBars[0].Visible = (string)showIndicatorListBoxEdit.SelectedItem == "Range Bar";
            }
        }
        void LbeShowPercent_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            foreach (LinearScale scale in linearGaugeControl.Scales)
                if (scale.LabelOptions != null)
                    scale.LabelOptions.Multiplier = (string)lbeShowPercent.SelectedItem == "Values" ? 1 : 100 / scale.EndValue;
        }
    }
}
