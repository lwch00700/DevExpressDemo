using System;
using System.Windows;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Editors;

namespace SchedulerDemo {
    public partial class WorkWeekView : SchedulerDemoModule {
        public WorkWeekView() {
            InitializeComponent();
            InitializeScheduler();
        }

        private void WeekDaysCheckEditChecked(object sender, RoutedEventArgs e) {
            CheckEdit edit = (CheckEdit)sender;
            WeekDays day = WeekDaysToBooleanConverter.GetDay((string)edit.Content);
            if ((int)day == 0)
                return;
            WeekDays weekDays = SchedulerDemoUtils.AddWeekDay(scheduler.WorkDays.GetWeekDays(), day);
            SchedulerDemoUtils.UpdateSchedulerWorkDays(scheduler, weekDays);
        }
        private void WeekDaysCheckEditUnchecked(object sender, RoutedEventArgs e) {
            CheckEdit edit = (CheckEdit)sender;
            WeekDays day = WeekDaysToBooleanConverter.GetDay((string)edit.Content);
            if ((int)day == 0)
                return;
            WeekDays weekDays = SchedulerDemoUtils.RemoveWeekDay(scheduler.WorkDays.GetWeekDays(), day);
            SchedulerDemoUtils.UpdateSchedulerWorkDays(scheduler, weekDays);
        }
    }
}
