using System;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;

namespace GaugesDemo {
    public partial class IndicatorAnimation : GaugesDemoModule {
        Random rand = new Random();
        DispatcherTimer timer = new DispatcherTimer();

        public IndicatorAnimation() {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }
        void Timer_Tick(object source, EventArgs e) {
            voltmeterScaleNeedle.Value = rand.Next(1, (int)voltmeterScale.EndValue);
            double nextRand = rand.Next(3, (int)ampermeterScale.EndValue * 10);
            ampermeterScaleNeedle.Value = nextRand / 10.0;
            wattmeterScaleNeedle.Value = voltmeterScaleNeedle.Value * ampermeterScaleNeedle.Value;
        }
    }
}
