using System;
using System.Collections.Generic;
using DevExpress.XtraScheduler;
using System.Collections;
using System.ComponentModel;
using DevExpress.Xpf.Scheduler;
using SchedulerDemo.Internal;

namespace SchedulerDemo {
    public partial class ListBoundMode : SchedulerDemoModule {
        public ListBoundMode() {
            InitializeComponent();
            InitializeSchedulerProperties(scheduler);
            InitializeResources();
            InitializeAppointments();
        }
        void InitializeResources() {
            int count = DemoUtils.ResourceList.Count;
            List<CustomResource> resources = new List<CustomResource>();
            for(int i = 0; i < count; i++) {
                CustomResource customResource = new CustomResource();
                customResource.Caption = DemoUtils.ResourceList[i];
                customResource.Id = i;
                resources.Add(customResource);
            }
            this.scheduler.Storage.ResourceStorage.DataSource = resources;
        }
        void InitializeAppointments() {
            SchedulerBindingList<CustomEvent> eventList = new SchedulerBindingList<CustomEvent>();
            GenerateEvents(eventList);
            this.scheduler.Storage.AppointmentStorage.DataSource = eventList;

        }
        void GenerateEvents(SchedulerBindingList<CustomEvent> eventList) {
            int count = this.scheduler.Storage.ResourceStorage.Count;
            for(int i = 0; i < count; i++) {
                Resource resource = this.scheduler.Storage.ResourceStorage[i];
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
            apt.StartTime = new DateTime(2010, 7, 13) + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }
    }
}
