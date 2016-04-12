using System;
using SchedulerDemo;

namespace SchedulerDemo {
    public partial class SchedulerRibbonBars : SchedulerDemoModule {
        public SchedulerRibbonBars() {
            InitializeComponent();
            InitializeScheduler();
            scheduler.Start += TimeSpan.FromDays(1);
        }
    }
}
