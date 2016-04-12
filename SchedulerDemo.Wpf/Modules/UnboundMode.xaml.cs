using System;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler;

namespace SchedulerDemo {
    public partial class UnboundMode : SchedulerDemoModule {
        public UnboundMode() {
            InitializeComponent();
            this.scheduler.Start = DateTime.Now;
            DemoUtils.FillResources(SchedulerStorage, 5);
            InitAppointments();
        }

        public SchedulerStorage SchedulerStorage { get { return this.scheduler.Storage; } }

        void InitAppointments() {
            GenerateEvents(SchedulerStorage);
        }

        void GenerateEvents(SchedulerStorage storage) {
            int count = storage.ResourceStorage.Count;
            storage.BeginUpdate();
            for (int i = 0; i < count; i++) {
                Resource resource = storage.ResourceStorage[i];
                CreateAppointentsForResource(storage, resource);
            }
            storage.EndUpdate();
        }

        private void CreateAppointentsForResource(SchedulerStorage storage, Resource resource) {
            Random rnd = DemoUtils.RandomInstance;
            int statusCount = storage.AppointmentStorage.Statuses.Count;
            string[] appointmentKind = { "appointment", "personal time", "meeting", "travel" };
            int[] labelKind = { 1, 3, 5, 6 };

            string subjPrefix = resource.Caption + "'s ";
            AppointmentStorage appointments = storage.AppointmentStorage;

            for (int i = 0; i < 50; i++) {
                int statusId = rnd.Next(0, statusCount);
                int aptKindId = rnd.Next(0, appointmentKind.Length);
                int labelId = labelKind[aptKindId];

                string subject = subjPrefix + appointmentKind[aptKindId];
                int dateRange = rnd.Next(0, 20);
                DateTime start = DateTime.Today.AddDays(dateRange - 2);

                appointments.Add(CreateAppointment(start, subject, resource.Id, statusId, labelId));
            }
        }

        Appointment CreateAppointment(DateTime start, string subject, object resourceId, int status, int label) {
            Appointment apt = SchedulerStorage.CreateAppointment(AppointmentType.Normal);
            apt.Subject = subject;
            apt.ResourceId = resourceId;
            Random rnd = DemoUtils.RandomInstance;
            apt.AllDay = rnd.Next(0, 5) == 0;

            int rangeInMinutes = 60 * 24;
            apt.Start = start + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            TimeSpan duration = TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            if (duration.Ticks == 0)
                duration = TimeSpan.FromMinutes(5);

            apt.End = apt.Start + duration;
            apt.StatusKey = status;
            apt.LabelKey = label;
            return apt;
        }
    }
}
