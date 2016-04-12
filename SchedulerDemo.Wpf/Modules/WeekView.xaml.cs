using System;
using System.Collections.Generic;
using SchedulerDemo;
using DevExpress.XtraScheduler;

namespace SchedulerDemo {
    public partial class WeekView : SchedulerDemoModule {
        public WeekView() {
            InitializeComponent();
            InitializeScheduler();
            scheduler.WeekView.AppointmentDisplayOptions.StartTimeVisibility = AppointmentTimeVisibility.Always;
            scheduler.WeekView.AppointmentDisplayOptions.EndTimeVisibility = AppointmentTimeVisibility.Always;
        }
    }

    public class FirstDayOfWeekList : List<DevExpress.XtraScheduler.FirstDayOfWeek> {
    }

    public class AppointmentTimeDisplayTypeList : List<AppointmentTimeDisplayType> {
    }

    public class AppointmentTimeVisibilityList : List<AppointmentTimeVisibility> {
    }
}
