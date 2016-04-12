using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class VisualElementStyles : SchedulerDemoModule {
        public VisualElementStyles() {
            InitializeComponent();
            InitializeScheduler();

            ApplyMoreButtonsStyle();
            ApplyNavigationButtonsStyle();
        }

        private void ApplyNavigationButtonsStyle() {
            Style prevNavButtonStyle = (Style)this.FindResource("PrevNavigationButton");
            Style nextNavButtonStyle = (Style)this.FindResource("NextNavigationButton");
            foreach (SchedulerViewBase view in Views) {
                view.NavigationButtonPrevStyle = prevNavButtonStyle;
                view.NavigationButtonNextStyle = nextNavButtonStyle;
            }
        }
        private void ApplyMoreButtonsStyle() {
            Style downStyle = (Style)this.FindResource("MoreButtonDownStyle");
            scheduler.DayView.MoreButtonDownStyle = downStyle;
            scheduler.WorkWeekView.MoreButtonDownStyle = downStyle;
            Style upStyle = (Style)this.FindResource("MoreButtonUpStyle");
            scheduler.DayView.MoreButtonUpStyle = upStyle;
            scheduler.WorkWeekView.MoreButtonUpStyle = upStyle;

            scheduler.WeekView.MoreButtonStyle = downStyle;
            scheduler.MonthView.MoreButtonStyle = downStyle;
            scheduler.TimelineView.MoreButtonStyle = downStyle;
        }
        private void ClearNavigationButtonsStyle() {
            foreach (SchedulerViewBase view in Views) {
                view.ClearValue(SchedulerViewBase.NavigationButtonNextStyleProperty);
                view.ClearValue(SchedulerViewBase.NavigationButtonPrevStyleProperty);
            }
        }
        private void ClearMoreButtonsStyle() {
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonDownStyleProperty);
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonUpStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonDownStyleProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.DayView.MoreButtonUpStyleProperty);

            scheduler.WeekView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.MoreButtonStyleProperty);
            scheduler.MonthView.ClearValue(DevExpress.Xpf.Scheduler.WeekView.MoreButtonStyleProperty);
            scheduler.TimelineView.ClearValue(DevExpress.Xpf.Scheduler.TimelineView.MoreButtonStyleProperty);
        }
        private void cheMoreButtons_Checked(object sender, RoutedEventArgs e) {
            ApplyMoreButtonsStyle();
        }
        private void cheMoreButtons_Unchecked(object sender, RoutedEventArgs e) {
            ClearMoreButtonsStyle();
        }
        private void cheNavigationButtons_Checked(object sender, RoutedEventArgs e) {
            ApplyNavigationButtonsStyle();
        }
        private void cheNavigationButtons_Unchecked(object sender, RoutedEventArgs e) {
            ClearNavigationButtonsStyle();
        }
    }
}
