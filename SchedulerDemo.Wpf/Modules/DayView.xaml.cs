using System;
using System.Collections.Generic;
using DevExpress.XtraScheduler;

namespace SchedulerDemo {
    public partial class DayView : SchedulerDemoModule {
        public DayView() {
            InitializeComponent();
            InitializeScheduler();
        }
    }

    public class AppointmentSnapToCellsModeList : List<AppointmentSnapToCellsMode> {
    }
    public class AppointmentStatusDisplayTypeList : List<AppointmentStatusDisplayType> {
    }
    public class TimeIndicatorVisibilityTypeList : List<TimeIndicatorVisibility> {
    }
    public class TimeMarkerVisibilityTypeList : List<TimeMarkerVisibility> {
    }

}
