using System;
using System.Windows;
using DevExpress.XtraScheduler;

namespace SchedulerDemo {
    public partial class TimeLineView : SchedulerDemoModule {
        public static readonly DependencyProperty YearScaleProperty;
        public static readonly DependencyProperty QuarterScaleProperty;
        public static readonly DependencyProperty MonthScaleProperty;
        public static readonly DependencyProperty WeekScaleProperty;
        public static readonly DependencyProperty DayScaleProperty;
        public static readonly DependencyProperty HourScaleProperty;
        public static readonly DependencyProperty Min15ScaleProperty;


        static TimeLineView() {
            YearScaleProperty = DependencyProperty.Register("YearScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            QuarterScaleProperty = DependencyProperty.Register("QuarterScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            MonthScaleProperty = DependencyProperty.Register("MonthScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            WeekScaleProperty = DependencyProperty.Register("WeekScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            DayScaleProperty = DependencyProperty.Register("DayScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            HourScaleProperty = DependencyProperty.Register("HourScale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));
            Min15ScaleProperty = DependencyProperty.Register("Min15Scale", typeof(TimeScale), typeof(TimeLineView), new PropertyMetadata(null));

        }
        public TimeLineView() {
            InitializeComponent();
            FillTimeScaleList();
            InitializeScheduler();
            this.scheduler.TimelineView.TimeIndicatorDisplayOptions.Visibility = TimeIndicatorVisibility.CurrentDate;
        }

        public TimeScale YearScale {
            get { return (TimeScale)GetValue(YearScaleProperty); }
            set { SetValue(YearScaleProperty, value); }
        }
        public TimeScale QuarterScale {
            get { return (TimeScale)GetValue(QuarterScaleProperty); }
            set { SetValue(QuarterScaleProperty, value); }
        }
        public TimeScale MonthScale {
            get { return (TimeScale)GetValue(MonthScaleProperty); }
            set { SetValue(MonthScaleProperty, value); }
        }
        public TimeScale WeekScale {
            get { return (TimeScale)GetValue(WeekScaleProperty); }
            set { SetValue(WeekScaleProperty, value); }
        }
        public TimeScale DayScale {
            get { return (TimeScale)GetValue(DayScaleProperty); }
            set { SetValue(DayScaleProperty, value); }
        }
        public TimeScale HourScale {
            get { return (TimeScale)GetValue(HourScaleProperty); }
            set { SetValue(HourScaleProperty, value); }
        }
        public TimeScale Min15Scale {
            get { return (TimeScale)GetValue(Min15ScaleProperty); }
            set { SetValue(Min15ScaleProperty, value); }
        }
        public SchedulerGroupType GroupType {
            get { return SchedulerControl.GroupType; }
            set {
                SchedulerControl.GroupType = value;
            }
        }

        protected override object GetModuleDataContext() {
            SchedulerControl = FindScheduler();
            return this;
        }
        private void FillTimeScaleList() {
            TimeScaleCollection scales = scheduler.TimelineView.Scales;
            YearScale = scales[0];
            QuarterScale = scales[1];
            MonthScale = scales[2];
            WeekScale = scales[3];
            DayScale = scales[4];
            HourScale = scales[5];
            Min15Scale = scales[6];
        }
    }
}
