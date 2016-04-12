using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using System.Windows;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.Xpf.Core.Native;

namespace SchedulerDemo.Tests {
    #region SchedulerCheckAllDemosFixture
    public class SchedulerCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
            Type[] skipModuleTypes = { typeof(ImportFromOutlook), typeof(SynchronizeWithOutlook) };
            return !skipModuleTypes.Contains(moduleType);
        }
        protected override bool CheckMemoryLeaks(Type moduleTyle) {
            return true;
        }
    }
    #endregion

    #region SchedulerDemoModuleAccessor
    public class SchedulerDemoModuleAccessor : DemoModulesAccessor<SchedulerDemoModule> {
        public SchedulerDemoModuleAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public SchedulerControl SchedulerControl { get { return DemoModule.SchedulerControl; } }
        public SchedulerDemo.DayView DayViewModule { get { return (SchedulerDemo.DayView)DemoModule; } }
        public SchedulerDemo.WorkWeekView WorkWeekViewModule { get { return (SchedulerDemo.WorkWeekView)DemoModule; } }
        public SchedulerDemo.WeekView WeekViewModule { get { return (SchedulerDemo.WeekView)DemoModule; } }
        public SchedulerDemo.MonthView MonthViewModule { get { return (SchedulerDemo.MonthView)DemoModule; } }
        public SchedulerDemo.TimeLineView TimeLineViewModule { get { return (SchedulerDemo.TimeLineView)DemoModule; } }
        public SchedulerDemo.AppointmentStyles AppointmentStylesModule { get { return (SchedulerDemo.AppointmentStyles)DemoModule; } }
    }
    #endregion
    #region BaseSchedulerTestingFixture
    public abstract class BaseSchedulerTestingFixture : BaseDemoTestingFixture {
        readonly SchedulerDemoModuleAccessor modulesAccessor;
        public BaseSchedulerTestingFixture() {
            this.modulesAccessor = new SchedulerDemoModuleAccessor(this);
        }
        public SchedulerControl SchedulerControl { get { return this.modulesAccessor.SchedulerControl; } }
        public SchedulerDemo.DayView DayViewModule { get { return modulesAccessor.DayViewModule; } }
        public SchedulerDemo.WorkWeekView WorkWeekViewModule { get { return modulesAccessor.WorkWeekViewModule; } }
        public SchedulerDemo.WeekView WeekViewModule { get { return modulesAccessor.WeekViewModule; } }
        public SchedulerDemo.MonthView MonthViewModule { get { return modulesAccessor.MonthViewModule; } }
        public SchedulerDemo.TimeLineView TimeLineViewModule { get { return modulesAccessor.TimeLineViewModule; } }
        public SchedulerDemo.AppointmentStyles AppointmentStylesModule { get { return modulesAccessor.AppointmentStylesModule; } }
    }
    #endregion
    #region CheckDayViewModuleFixture
    public class CheckDayViewModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            AddLoadModuleActions(typeof(DayView));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            DayView dayViewModule = (DayView)DemoBaseTesting.CurrentDemoModule;
            int initialDayCount = 3;
            Assert.AreEqual(SchedulerViewType.Day, dayViewModule.scheduler.ActiveViewType);
            Assert.AreEqual(initialDayCount, dayViewModule.scheduler.DayView.DayCount);

            for(int i = initialDayCount + 1; i <= 10; i++) {
                dayViewModule.spnDayCount.SpinUp();
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(false, dayViewModule.spnDayCount.HasValidationError);
                Assert.AreEqual(i, dayViewModule.scheduler.DayView.DayCount);
            }

            Assert.AreEqual(false, dayViewModule.scheduler.DayView.AllDayAreaScrollBarVisible);
            Assert.AreEqual(false, dayViewModule.chkShowAllDayAreaScrollBars.IsChecked);
            dayViewModule.chkShowAllDayAreaScrollBars.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, dayViewModule.scheduler.DayView.AllDayAreaScrollBarVisible);

            dayViewModule.chkShowAllDayAreaScrollBars.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, dayViewModule.scheduler.DayView.AllDayAreaScrollBarVisible);

            Assert.AreEqual(false, dayViewModule.scheduler.DayView.ShowWorkTimeOnly);
            Assert.AreEqual(false, dayViewModule.chkShowWorkTimeOnly.IsChecked);
            dayViewModule.chkShowWorkTimeOnly.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, dayViewModule.scheduler.DayView.ShowWorkTimeOnly);

            dayViewModule.chkShowWorkTimeOnly.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, dayViewModule.scheduler.DayView.ShowWorkTimeOnly);

            Assert.AreEqual(true, dayViewModule.scheduler.DayView.ShowDayHeaders);
            Assert.AreEqual(true, dayViewModule.chkShowDayHeaders.IsChecked);
            dayViewModule.chkShowDayHeaders.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, dayViewModule.scheduler.DayView.ShowDayHeaders);

            dayViewModule.chkShowDayHeaders.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, dayViewModule.scheduler.DayView.ShowDayHeaders);

            Assert.AreEqual(true, dayViewModule.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.DayView.ShowAllDayArea must be true");
            Assert.AreEqual(true, dayViewModule.chkShowAllDayArea.IsChecked, "dayViewModule.chkShowAllDayArea.IsChecked must be true");
            dayViewModule.chkShowAllDayArea.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, dayViewModule.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.WorkWeekView.ShowAllDayArea must be false");

            dayViewModule.chkShowAllDayArea.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, dayViewModule.scheduler.DayView.ShowAllDayArea, "dayViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true");
        }
    }
    #endregion
    #region CheckWorkWeekViewModuleFixture
    public class CheckWorkWeekViewModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemoActions);
        }
        void CreateCheckDemoActions() {
            AddLoadModuleActions(typeof(WorkWeekView));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            WorkWeekView workWeekViewModule = (WorkWeekView)DemoBaseTesting.CurrentDemoModule;
            WeekDays weekDays = WeekDays.Monday | WeekDays.Tuesday | WeekDays.Wednesday | WeekDays.Thursday | WeekDays.Friday;
            Assert.AreEqual(SchedulerViewType.WorkWeek, workWeekViewModule.scheduler.ActiveViewType);
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkMonday.IsChecked = false;
            weekDays = weekDays & ~WeekDays.Monday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkTuesday.IsChecked = false;
            weekDays = weekDays & ~WeekDays.Tuesday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkWednesday.IsChecked = false;
            weekDays = weekDays & ~WeekDays.Wednesday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkThursday.IsChecked = false;
            weekDays = weekDays & ~WeekDays.Thursday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkFriday.IsChecked = false;
            weekDays = weekDays & ~WeekDays.Friday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkSaturday.IsChecked = true;
            weekDays = weekDays | WeekDays.Saturday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            workWeekViewModule.chkSunday.IsChecked = true;
            weekDays = weekDays | WeekDays.Sunday;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(weekDays, workWeekViewModule.scheduler.WorkDays.GetWeekDays());

            Assert.AreEqual(false, workWeekViewModule.scheduler.WorkWeekView.AllDayAreaScrollBarVisible);
            Assert.AreEqual(false, workWeekViewModule.chkShowAllDayAreaScrollBars.IsChecked);
            workWeekViewModule.chkShowAllDayAreaScrollBars.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, workWeekViewModule.scheduler.WorkWeekView.AllDayAreaScrollBarVisible);

            workWeekViewModule.chkShowAllDayAreaScrollBars.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, workWeekViewModule.scheduler.WorkWeekView.AllDayAreaScrollBarVisible);

            Assert.AreEqual(true, workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true");
            Assert.AreEqual(true, workWeekViewModule.chkShowAllDayArea.IsChecked, "workWeekView.chkShowAllDayArea.IsChecked must be true");
            workWeekViewModule.chkShowAllDayArea.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be false");

            workWeekViewModule.chkShowAllDayArea.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea, "workWeekViewModule.scheduler.WorkWeekView.ShowAllDayArea must be true");
        }
    }
    #endregion
    #region CheckWeekViewModuleFixture
    public class CheckWeekViewModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemoActions);
        }
        void CreateCheckDemoActions() {
            AddLoadModuleActions(typeof(WeekView));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            WeekView weekViewModule = (WeekView)DemoBaseTesting.CurrentDemoModule;
            Assert.AreEqual(SchedulerViewType.Week, weekViewModule.scheduler.ActiveViewType);
            Assert.AreEqual(DevExpress.XtraScheduler.FirstDayOfWeek.System, weekViewModule.scheduler.OptionsView.FirstDayOfWeek);

            for (int i = 0; i <= 7; i++) {
                weekViewModule.cbFirstDayOfWeek.SelectedItem = (DevExpress.XtraScheduler.FirstDayOfWeek)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(false, weekViewModule.cbFirstDayOfWeek.HasValidationError);
                Assert.AreEqual((DevExpress.XtraScheduler.FirstDayOfWeek)i, weekViewModule.scheduler.OptionsView.FirstDayOfWeek);
            }

            for (int i = 0; i <= 2; i++) {
                weekViewModule.cbTimeDisplayType.SelectedItem = (AppointmentTimeDisplayType)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeDisplayType)i, weekViewModule.scheduler.WeekView.AppointmentDisplayOptions.TimeDisplayType);
            }

            for (int i = 0; i <= 2; i++) {
                weekViewModule.cbTimeDisplayType.SelectedItem = (AppointmentTimeDisplayType)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeDisplayType)i, weekViewModule.scheduler.WeekView.AppointmentDisplayOptions.TimeDisplayType);
            }

            for (int i = 0; i <= 2; i++) {
                weekViewModule.cbStartTimeVisibility.SelectedItem = (AppointmentTimeVisibility)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeVisibility)i, weekViewModule.scheduler.WeekView.AppointmentDisplayOptions.StartTimeVisibility);
            }

            for (int i = 0; i <= 2; i++) {
                weekViewModule.cbEndTimeVisibility.SelectedItem = (AppointmentTimeVisibility)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeVisibility)i, weekViewModule.scheduler.WeekView.AppointmentDisplayOptions.EndTimeVisibility);
            }
        }
    }
    #endregion
    #region CheckMonthViewModuleFixture
    public class CheckMonthViewModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            AddLoadModuleActions(typeof(MonthView));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            MonthView monthViewModule = (MonthView)DemoBaseTesting.CurrentDemoModule;
            int initialWeekCount = 5;
            Assert.AreEqual(SchedulerViewType.Month, monthViewModule.scheduler.ActiveViewType);
            Assert.AreEqual(initialWeekCount, monthViewModule.scheduler.MonthView.WeekCount);

            for (int i = initialWeekCount + 1; i <= 10; i++)
            {
                monthViewModule.spnWeekCount.SpinUp();
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(false, monthViewModule.spnWeekCount.HasValidationError);
                Assert.AreEqual(i, monthViewModule.scheduler.MonthView.WeekCount);
            }

            Assert.AreEqual(DevExpress.XtraScheduler.FirstDayOfWeek.System, monthViewModule.scheduler.OptionsView.FirstDayOfWeek);

            for (int i = 0; i <= 7; i++)
            {
                monthViewModule.cbFirstDayOfWeek.SelectedItem = (DevExpress.XtraScheduler.FirstDayOfWeek)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(false, monthViewModule.cbFirstDayOfWeek.HasValidationError);
                Assert.AreEqual((DevExpress.XtraScheduler.FirstDayOfWeek)i, monthViewModule.scheduler.OptionsView.FirstDayOfWeek);
            }

            Assert.AreEqual(false, monthViewModule.chCompressWeekend.IsChecked);
            Assert.AreEqual(false, monthViewModule.scheduler.MonthView.CompressWeekend);
            monthViewModule.chCompressWeekend.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, monthViewModule.scheduler.MonthView.CompressWeekend);

            Assert.AreEqual(true, monthViewModule.chCompressWeekend.IsChecked);
            Assert.AreEqual(true, monthViewModule.scheduler.MonthView.CompressWeekend);
            monthViewModule.chCompressWeekend.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, monthViewModule.scheduler.MonthView.CompressWeekend);


            Assert.AreEqual(true, monthViewModule.scheduler.MonthView.ShowWeekend);
            Assert.AreEqual(true, monthViewModule.chShowWeekend.IsChecked);
            monthViewModule.chShowWeekend.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, monthViewModule.scheduler.MonthView.ShowWeekend);

            Assert.AreEqual(false, monthViewModule.chShowRecurrence.IsChecked);
            Assert.AreEqual(false, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.ShowRecurrence);
            monthViewModule.chShowRecurrence.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.ShowRecurrence);

            for (int i = 0; i <= 2; i++)
            {
                monthViewModule.cbStatusDisplayType.SelectedItem = (AppointmentStatusDisplayType)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentStatusDisplayType)i, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.StatusDisplayType);
            }

            for (int i = 0; i <= 2; i++)
            {
                monthViewModule.cbTimeDisplayType.SelectedItem = (AppointmentTimeDisplayType)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeDisplayType)i, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.TimeDisplayType);
            }

            for (int i = 0; i <= 2; i++)
            {
                monthViewModule.cbStartTimeVisibility.SelectedItem = (AppointmentTimeVisibility)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeVisibility)i, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.StartTimeVisibility);
            }

            for (int i = 0; i <= 2; i++)
            {
                monthViewModule.cbEndTimeVisibility.SelectedItem = (AppointmentTimeVisibility)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentTimeVisibility)i, monthViewModule.scheduler.MonthView.AppointmentDisplayOptions.EndTimeVisibility);
            }
        }
    }
    #endregion
    #region CheckTimeLineViewModuleFixture
    public class CheckTimeLineViewModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemosActions);
        }
        void CreateCheckDemosActions() {
            AddLoadModuleActions(typeof(TimeLineView));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            TimeLineView timeLineViewModule = (TimeLineView)DemoBaseTesting.CurrentDemoModule;
            int initialIntervalCount = 10;
            int initialSelectionBarHeight = 0;
            SchedulerControl scheduler = timeLineViewModule.scheduler;
            Assert.AreEqual(SchedulerViewType.Timeline, scheduler.ActiveViewType);
            DevExpress.Xpf.Scheduler.TimelineView timelineView = scheduler.TimelineView;
            Assert.AreEqual(initialIntervalCount, timelineView.IntervalCount);
            SchedulerSelectionBarOptions selectionBar = timelineView.SelectionBar;
            Assert.AreEqual(initialSelectionBarHeight, selectionBar.Height);

            for (int i = initialIntervalCount - 1; i >= 1; i--) {
                timeLineViewModule.spnIntervalCount.SpinDown();
                if (initialIntervalCount / 2 - 1 < i && i < initialIntervalCount / 2 + 1)
                    timeLineViewModule.chkVisible.IsChecked = false;
                else
                    timeLineViewModule.chkVisible.IsChecked = true;
                UpdateLayoutAndDoEvents();
                if (selectionBar.Visible)
                    Assert.AreEqual(i, GetSelectionBarElementCount(scheduler));
                Assert.AreEqual(false, timeLineViewModule.spnIntervalCount.HasValidationError);
                Assert.AreEqual(i, timelineView.IntervalCount);
            }

            for (int i = 0; i <= 2; i++) {
                timeLineViewModule.cbSnapToCellsMode.SelectedItem = (AppointmentSnapToCellsMode)i;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual((AppointmentSnapToCellsMode)i, timelineView.AppointmentDisplayOptions.SnapToCellsMode);
            }

            Assert.AreEqual(true, timeLineViewModule.chkVisible.IsChecked);

            for (int i = 20; i <= 10; i++) {
                timeLineViewModule.spnSelectionBarHeight.SpinUp();
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(i, selectionBar.Height);
            }

            timeLineViewModule.chkVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, selectionBar.Visible);

            Assert.AreEqual(false, timeLineViewModule.chYearScaleEnabled.IsChecked);
            Assert.AreEqual(false, timelineView.Scales[0].Enabled);
            timeLineViewModule.chYearScaleEnabled.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, timelineView.Scales[0].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chYearScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[0].Visible);
            timeLineViewModule.chYearScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[0].Visible);

            Assert.AreEqual(false, timeLineViewModule.chQuarterScaleEnabled.IsChecked);
            Assert.AreEqual(false, timelineView.Scales[1].Enabled);
            timeLineViewModule.chQuarterScaleEnabled.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, timelineView.Scales[1].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chQuarterScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[1].Visible);
            timeLineViewModule.chQuarterScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[1].Visible);

            Assert.AreEqual(false, timeLineViewModule.chMonthScaleEnabled.IsChecked);
            Assert.AreEqual(false, timelineView.Scales[2].Enabled);
            timeLineViewModule.chMonthScaleEnabled.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, timelineView.Scales[2].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chMonthScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[2].Visible);
            timeLineViewModule.chMonthScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[2].Visible);

            Assert.AreEqual(true, timeLineViewModule.chWeekScaleEnabled.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[3].Enabled);
            timeLineViewModule.chWeekScaleEnabled.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[3].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chWeekScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[3].Visible);
            timeLineViewModule.chWeekScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[3].Visible);

            Assert.AreEqual(true, timeLineViewModule.chDayScaleEnabled.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[4].Enabled);
            timeLineViewModule.chDayScaleEnabled.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[4].Enabled);

            Assert.AreEqual(false, timeLineViewModule.chHourScaleEnabled.IsChecked);
            Assert.AreEqual(false, timelineView.Scales[5].Enabled);
            timeLineViewModule.chHourScaleEnabled.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, timelineView.Scales[5].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chHourScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[5].Visible);
            timeLineViewModule.chHourScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[5].Visible);

            Assert.AreEqual(false, timeLineViewModule.chMin15ScaleEnabled.IsChecked);
            Assert.AreEqual(false, timelineView.Scales[6].Enabled);
            timeLineViewModule.chMin15ScaleEnabled.IsChecked = true;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(true, timelineView.Scales[6].Enabled);

            Assert.AreEqual(true, timeLineViewModule.chMin15ScaleVisible.IsChecked);
            Assert.AreEqual(true, timelineView.Scales[6].Visible);
            timeLineViewModule.chMin15ScaleVisible.IsChecked = false;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(false, timelineView.Scales[6].Visible);
        }
        int GetSelectionBarElementCount(SchedulerControl scheduler) {
            List<FrameworkElement> result = new List<FrameworkElement>();
            VisualTreeEnumerator treeWalker = new VisualTreeEnumerator(scheduler);
            while(treeWalker.MoveNext()) {
                FrameworkElement fe = treeWalker.Current as FrameworkElement;
                if (fe == null)
                    continue;
                if (IsRootSelectionBarCell(fe, result))
                    result.Add(fe);
            }
            return result.Count;
        }
        bool IsRootSelectionBarCell(FrameworkElement element, List<FrameworkElement> roots) {
            SchedulerHitTest hitTest = SchedulerControl.GetHitTestType(element);
            if (hitTest.Equals(SchedulerHitTest.SelectionBarCell)) {
                foreach(FrameworkElement fe in roots) {
                    if (LayoutHelper.IsChildElement(fe, element))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
    #endregion

    #region CheckGroupByResourceFixture
    public class CheckGroupByResourceFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemoActions);
        }
        void CreateCheckDemoActions() {
            AddLoadModuleActions(typeof(GroupByResource));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            GroupByResource module = (GroupByResource)DemoBaseTesting.CurrentDemoModule;

            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Day);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Resource);

            module.viewType.EditValue = SchedulerViewType.Week;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Week);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Resource);

            module.viewType.EditValue = SchedulerViewType.WorkWeek;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.WorkWeek);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Resource);

            module.viewType.EditValue = SchedulerViewType.Month;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Month);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Resource);

            module.viewType.EditValue = SchedulerViewType.Timeline;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Timeline);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Resource);
        }
    }
    #endregion
    #region CheckGroupByDateModuleFixture
    public class CheckGroupByDateModuleFixture : BaseSchedulerTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddSimpleAction(CreateCheckDemoActions);
        }
        void CreateCheckDemoActions() {
            AddLoadModuleActions(typeof(GroupByDate));
            AddSimpleAction(CheckDemo);
        }
        void CheckDemo() {
            GroupByDate module = (GroupByDate)DemoBaseTesting.CurrentDemoModule;

            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Day);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Date);

            module.viewType.EditValue = SchedulerViewType.Week;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Week);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Date);

            module.viewType.EditValue = SchedulerViewType.WorkWeek;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.WorkWeek);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Date);

            module.viewType.EditValue = SchedulerViewType.Month;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Month);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Date);

            module.viewType.EditValue = SchedulerViewType.Timeline;
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(module.scheduler.ActiveViewType, SchedulerViewType.Timeline);
            Assert.AreEqual(module.scheduler.GroupType, SchedulerGroupType.Date);
        }
    }
    #endregion
}
