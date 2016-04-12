using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils.Themes;
using System.Windows;
using DevExpress.Xpf.Scheduler;
using System.Collections.Generic;
using System;
namespace SchedulerDemo {
    public class SchedulerDemoModule : DemoModule {
        public static readonly DependencyProperty SchedulerControlProperty;

        static SchedulerDemoModule() {
            SchedulerControlProperty = DependencyProperty.Register("SchedulerControl", typeof(SchedulerControl), typeof(SchedulerDemoModule), new PropertyMetadata(null));
        }
        readonly List<SchedulerViewBase> views = new List<SchedulerViewBase>();

        public SchedulerControl SchedulerControl {
            get { return (SchedulerControl)GetValue(SchedulerControlProperty); }
            set { SetValue(SchedulerControlProperty, value); }
        }
        protected List<SchedulerViewBase> Views { get { return views; } }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            if(SchedulerControl == null)
                SchedulerControl = FindScheduler();
        }
        private void PopulateSchedulerViewList(SchedulerControl scheduler) {
            views.Add(scheduler.DayView);
            views.Add(scheduler.WorkWeekView);
            views.Add(scheduler.WeekView);
            views.Add(scheduler.MonthView);
            views.Add(scheduler.TimelineView);
        }

        protected override object GetModuleDataContext() {
            return FindScheduler();
        }
        protected virtual SchedulerControl FindScheduler() {
            return (SchedulerControl)DemoModuleControl.FindDemoContent(typeof(SchedulerControl), (DependencyObject)DemoModuleControl.Content);
        }

        protected void InitializeScheduler() {
            InitializeScheduler(false);
        }
        protected void InitializeScheduler(bool bindToDatabase) {
            SchedulerControl scheduler = FindScheduler();
            if(scheduler == null)
                return;
            if(bindToDatabase)
                SchedulerDataHelper.DataBindToDatabase(scheduler);
            else
                SchedulerDataHelper.DataBind(scheduler);
            InitializeSchedulerProperties(scheduler);
        }
        protected void LoadDefaultAppoinmentStatuses(SchedulerControl scheduler) {
            scheduler.Storage.InnerStorage.Appointments.Statuses.LoadDefaults();
        }
        protected void InitializeSchedulerProperties(SchedulerControl scheduler) {
            PopulateSchedulerViewList(scheduler);
            scheduler.ShowBorder = false;
            scheduler.Start = new DateTime(2010, 7, 13);
        }


        protected override void RaiseBeforeModuleDisappear() {
            SchedulerControl scheduler = FindScheduler();
            if(scheduler != null)
                SchedulerDataHelper.DataUnbind(scheduler);
            base.RaiseBeforeModuleDisappear();
        }
    }
}
