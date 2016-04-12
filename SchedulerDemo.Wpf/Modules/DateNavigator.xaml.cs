using System;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.XtraScheduler;
using System.Globalization;

namespace SchedulerDemo {
    public partial class DateNavigator : SchedulerDemoModule {
        public DateNavigator() {
            InitializeComponent();
            InitializeScheduler();
            scheduler.ShowBorder = true;

            cbWeekNumberRule.ItemsSource = CreateWeekNumberRuleList();

        }

        private System.Collections.IEnumerable CreateWeekNumberRuleList() {
            ObjectList list = new ObjectList();
            list.Add(CalendarWeekRule.FirstDay);
            list.Add(CalendarWeekRule.FirstFourDayWeek);
            list.Add(CalendarWeekRule.FirstFullWeek);
            return list;
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
