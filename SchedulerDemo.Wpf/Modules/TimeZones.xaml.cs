using System;
using System.Windows;
using System.Collections.Generic;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;
using System.Windows.Markup;
using System.Windows.Data;

namespace SchedulerDemo {
    public partial class TimeZones : SchedulerDemoModule {
        public TimeZones() {
            InitializeComponent();
            InitializeScheduler();
        }

        private void edTimeZones_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            string tzId = edTimeZones.TimeZoneId;
            UpdateClientTimeRuler(scheduler.DayView.TimeRulers[2], tzId);
            UpdateClientTimeRuler(scheduler.WorkWeekView.TimeRulers[2], tzId);
        }

        private static void UpdateClientTimeRuler(SchedulerTimeRuler ruler, string tzId) {
            ruler.Caption = tzId.ToString();
            ruler.TimeZoneId = tzId;
        }
    }

    public class DemoViewTypeListItemList : List<DemoViewTypeListItem> {
    }
}
