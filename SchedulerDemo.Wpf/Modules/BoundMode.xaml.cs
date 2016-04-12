using System;

namespace SchedulerDemo {
    public partial class BoundMode : SchedulerDemoModule {
        public BoundMode() {
            InitializeComponent();
            InitializeSchedulerProperties(scheduler);
            scheduler.Start = new DateTime(2014, 05, 13);
        }
    }
}
