using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.Native;
using SchedulerDemo.Internal;

namespace SchedulerDemo {
    public partial class CustomWorkTime : SchedulerDemoModule {
        public CustomWorkTime() {
            InitializeComponent();
            InitializeSchedulerProperties(scheduler);
            DemoUtils.FillResources(scheduler.Storage, 5);
            InitAppointments();
            scheduler.Start = DateTime.Now;
        }
        void InitAppointments() {
            SchedulerStorage storage = scheduler.Storage;
            SchedulerBindingList<CustomEvent> eventList = new SchedulerBindingList<CustomEvent>();
            GenerateEvents(storage, eventList);
            storage.AppointmentStorage.DataSource = eventList;
        }
        void GenerateEvents(SchedulerStorage storage, SchedulerBindingList<CustomEvent> eventList) {
            int count = storage.ResourceStorage.Count;
            for (int i = 0; i < count; i++) {
                Resource resource = (Resource)storage.ResourceStorage[i];
                string subjPrefix = resource.Caption + "'s ";
                eventList.Add(CreateEvent(eventList, subjPrefix + "meeting", resource.Id, 2, 5));
                eventList.Add(CreateEvent(eventList, subjPrefix + "travel", resource.Id, 3, 6));
                eventList.Add(CreateEvent(eventList, subjPrefix + "phone call", resource.Id, 0, 10));
            }
        }
        CustomEvent CreateEvent(SchedulerBindingList<CustomEvent> eventList, string subject, object resourceId, int status, int label) {
            CustomEvent apt = new CustomEvent();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = DemoUtils.RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }


        TimeOfDayInterval[] workTimes = new TimeOfDayInterval[] {
            new TimeOfDayInterval(TimeSpan.FromHours(0), TimeSpan.FromHours(16)),
            new TimeOfDayInterval(TimeSpan.FromHours(10), TimeSpan.FromHours(20)),
            null,
            new TimeOfDayInterval(TimeSpan.FromHours(7), TimeSpan.FromHours(15)),
            new TimeOfDayInterval(TimeSpan.FromHours(16), TimeSpan.FromHours(24)),
        };

        void scheduler_QueryWorkTime(object sender, QueryWorkTimeEventArgs e) {
            if (chkCustomWorkTime != null) {
                if (!(bool)chkCustomWorkTime.IsChecked)
                    return;
            }

            if (scheduler != null) {
                SchedulerStorage schedulerStorage = scheduler.Storage;
                if (schedulerStorage.ResourceStorage == null)
                    return;

                int resourceIndex = schedulerStorage.ResourceStorage.Items.IndexOf(e.Resource);
                if (resourceIndex >= 0) {
                    if (resourceIndex == 0) {
                        if ((e.Interval.Start.Day % 2) == 0)
                            e.WorkTime = workTimes[resourceIndex % workTimes.Length];
                        else {
                            e.WorkTimes.Add(new TimeOfDayInterval(TimeSpan.FromHours(8), TimeSpan.FromHours(13)));
                            e.WorkTimes.Add(new TimeOfDayInterval(TimeSpan.FromHours(14), TimeSpan.FromHours(18)));
                        }
                    }
                    else {
                        if (scheduler.WorkDays.IsWorkDay(e.Interval.Start.Date))
                            e.WorkTime = workTimes[resourceIndex % workTimes.Length];
                    }
                }
            }
        }

        private void chkCustomWorkTime_Checked(object sender, RoutedEventArgs e) {
            scheduler.ActiveView.LayoutChanged();
        }
    }
}
