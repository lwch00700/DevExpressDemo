using System;
using System.Windows;
using SchedulerDemo;
using DevExpress.XtraScheduler;

namespace SchedulerDemo {
    public partial class Reminders : SchedulerDemoModule {
        public Reminders() {
            InitializeComponent();

            scheduler.Start = DateTime.Now.Date;
            scheduler.Storage.RemindersCheckInterval = 3000;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            DateTime now = DateTime.Now + TimeSpan.FromMinutes(5);
            Appointment apt = scheduler.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Start = now;
            apt.Duration = TimeSpan.FromHours(1);
            apt.Subject = "Appointment with Reminder";
            apt.StatusKey = 1;
            apt.LabelKey = 0;
            apt.HasReminder = true;
            scheduler.Storage.AppointmentStorage.Add(apt);
            scheduler.ActiveView.GotoTimeInterval(new TimeInterval(apt.Start, apt.Duration));
        }
    }
}
