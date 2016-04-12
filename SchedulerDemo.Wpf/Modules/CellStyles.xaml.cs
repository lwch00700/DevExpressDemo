using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class CellStyles : SchedulerDemoModule {
        public CellStyles() {
            InitializeComponent();
            InitializeScheduler();

            ApplyCellStyles();
            ApplySelectionTemplate();
        }

        private void ClearCellStyles() {
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.CellStyleProperty);
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.AllDayAreaCellStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.CellStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.AllDayAreaCellStyleProperty);
            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.VerticalWeekCellStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.VerticalWeekCellStyleProperty);
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.MonthView.HorizontalWeekCellStyleProperty);
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.CellStyleProperty);
        }
        private void ClearSelectionTemplate() {
            foreach (SchedulerViewBase view in Views)
                view.ClearValue(SchedulerViewBase.SelectionTemplateProperty);

            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.SelectionTemplateProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.SelectionTemplateProperty);
            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.SelectionTemplateProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.SelectionTemplateProperty);
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.MonthView.SelectionTemplateProperty);
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.SelectionTemplateProperty);
        }
        private void ApplyCellStyles() {
            Style style = (Style)this.FindResource("DayViewCellStyle");
            scheduler.DayView.CellStyle = style;
            scheduler.WorkWeekView.CellStyle = style;
            scheduler.FullWeekView.CellStyle = style;

            Style allDayAreaStyle = (Style)this.FindResource("AllDayAreaCellStyle");
            scheduler.DayView.AllDayAreaCellStyle = allDayAreaStyle;
            scheduler.WorkWeekView.AllDayAreaCellStyle = allDayAreaStyle;

            scheduler.WeekView.VerticalWeekCellStyle = (Style)this.FindResource("VerticalWeekCellStyle");
            scheduler.MonthView.HorizontalWeekCellStyle = (Style)this.FindResource("HorizontalWeekCellStyle");

            scheduler.TimelineView.CellStyle = (Style)this.FindResource("TimelineViewCellStyle");

        }
        private void ApplySelectionTemplate() {
            ControlTemplate template = (ControlTemplate)this.FindResource("SelectionTemplate");
            foreach (SchedulerViewBase view in Views)
                view.SelectionTemplate = template;
        }
        private void chkCellStyles_Checked(object sender, RoutedEventArgs e) {
            ApplyCellStyles();
        }
        private void chkCellStyles_Unchecked(object sender, RoutedEventArgs e) {
            ClearCellStyles();
        }
        private void chkSelectionStyle_Checked(object sender, RoutedEventArgs e) {
            ApplySelectionTemplate();
        }
        private void chkSelectionStyle_Unchecked(object sender, RoutedEventArgs e) {
            ClearSelectionTemplate();
        }
    }
}
