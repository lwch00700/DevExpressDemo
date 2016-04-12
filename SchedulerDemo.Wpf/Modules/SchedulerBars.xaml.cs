using System;
using SchedulerDemo;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class SchedulerBars : SchedulerDemoModule {
        public SchedulerBars() {
            InitializeComponent();
            InitializeScheduler();
            SetResourcePerPage();
        }
        void SetResourcePerPage() {
            foreach(SchedulerViewBase view in Views)
                view.ResourcesPerPage = 3;
        }
    }
}
