using System;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;

namespace GaugesDemo {
    public partial class CircularIndicators : GaugesDemoModule {
        public CircularIndicators() {
            InitializeComponent();
            GaugeRandomDataGenerator dataGenerator = new GaugeRandomDataGenerator(-100, 100, 1500);
            gauge.DataContext = dataGenerator;
            dataGenerator.Start();
        }
    }
}
