using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class LinearModels : GaugesDemoModule {

        public LinearModels() {
            InitializeComponent();
            lbModel.SelectedIndex = 0;
            GaugeRandomDataGenerator dataGenerator = new GaugeRandomDataGenerator(0, 100);
            gauge1.DataContext = dataGenerator;
            gauge2.DataContext = dataGenerator;
            dataGenerator.Start();
        }
    }
}
