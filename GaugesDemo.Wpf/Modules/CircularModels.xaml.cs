using System;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;

namespace GaugesDemo {
    public partial class CircularModels : GaugesDemoModule {
        public CircularModels() {
            InitializeComponent();
            lbModel.SelectedIndex = 0;
            GaugeRandomDataGenerator dataGenerator = new GaugeRandomDataGenerator(0, 100, 1500);
            gauge.DataContext = dataGenerator;
            gaugeHalfTop.DataContext = dataGenerator;
            gaugeQuarterTopLeft.DataContext = dataGenerator;
            gaugeThreeQuarters.DataContext = dataGenerator;
            dataGenerator.Start();
        }
    }
}
