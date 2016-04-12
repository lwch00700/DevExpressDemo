using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class DateHeaderStyleModule : SchedulerDemoModule {
        public DateHeaderStyleModule() {
            InitializeComponent();
            InitializeScheduler();

            ApplyDateHeaderStyle();
        }

        private void ApplyDateHeaderStyle() {
            Style style = (Style)this.FindResource("DateHeaderStyle");
            scheduler.DayView.DateHeaderStyle = style;
            scheduler.WorkWeekView.DateHeaderStyle = style;
            scheduler.TimelineView.DateHeaderStyle = style;
        }
        private void ClearDateHeaderTemplate() {
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.DateHeaderStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.DateHeaderStyleProperty);
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.DateHeaderStyleProperty);
        }
        private void cheHeaderTemplate_Checked(object sender, RoutedEventArgs e) {
            ApplyDateHeaderStyle();
        }
        private void cheHeaderTemplate_Unchecked(object sender, RoutedEventArgs e) {
            ClearDateHeaderTemplate();
        }
    }
}
