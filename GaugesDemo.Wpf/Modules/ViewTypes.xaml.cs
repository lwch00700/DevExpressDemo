using System;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Gauges;

namespace GaugesDemo {
    public partial class ViewTypes : GaugesDemoModule {
        DispatcherTimer timer = new DispatcherTimer();

        public ViewTypes() {
            InitializeComponent();
            UpdateTime();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void UpdateTime() {
            time7Segment.Text = string.Format("{0:H:mm:ss}", DateTime.Now);
            time14Segment.Text = string.Format("{0:H:mm:ss}", DateTime.Now);
            timeMatrix5x8.Text = string.Format("{0:H:mm:ss}", DateTime.Now);
            timeMatrix8x14.Text = string.Format("{0:H:mm:ss}", DateTime.Now);
        }
    }
}
