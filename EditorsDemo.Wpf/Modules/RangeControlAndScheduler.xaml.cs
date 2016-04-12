using System;
using EditorsDemo.Utils;

namespace EditorsDemo {
    public partial class RangeControlAndScheduler : EditorsDemoModule {
        public RangeControlAndScheduler() {
            InitializeComponent();
            this.scheduler.Start = new DateTime(2010, 7, 11);
            SchedulerDataHelper.LoadTo(this.scheduler);
        }
    }
}
