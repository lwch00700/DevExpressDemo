using System;
using System.Windows;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Editors;

namespace SchedulerDemo {
    public partial class FullWeekView : SchedulerDemoModule {
        public FullWeekView() {
            InitializeComponent();
            InitializeScheduler();
        }
    }
}
