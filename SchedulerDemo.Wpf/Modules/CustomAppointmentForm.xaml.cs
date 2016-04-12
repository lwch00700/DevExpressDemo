using System;
using SchedulerDemo.Forms;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.Native;

namespace SchedulerDemo {
    public partial class CustomAppointmentForm : SchedulerDemoModule {
        public CustomAppointmentForm() {
            InitializeComponent();
            AddCustomFieldsMapping(scheduler.Storage);
            scheduler.Storage.FilterAppointment += new PersistentObjectCancelEventHandler(Storage_FilterAppointment);
            InitializeScheduler();
            scheduler.OptionsBehavior.RecurrentAppointmentEditAction = RecurrentAppointmentAction.Ask;
        }
        void Storage_FilterAppointment(object sender, PersistentObjectCancelEventArgs e) {
            Appointment apt = e.Object as Appointment;
            if(apt == null)
                return;
            if(apt.RecurrencePattern != null)
                if(!IsAllowedRecurrenceType(apt.RecurrencePattern.RecurrenceInfo.Type)) {
                    e.Cancel = true;
            }
            else if(apt.RecurrenceInfo != null) {
                if(!IsAllowedRecurrenceType(apt.RecurrenceInfo.Type))
                    e.Cancel = true;
            }
        }
        bool IsAllowedRecurrenceType(RecurrenceType type) {
            return Object.Equals(type, RecurrenceType.Daily) || Object.Equals(type, RecurrenceType.Monthly);
        }
        void AddCustomFieldsMapping(SchedulerStorage schedulerStorage) {
            SchedulerCustomFieldMapping customNameMapping = new SchedulerCustomFieldMapping("CustomName", "CustomName");
            SchedulerCustomFieldMapping customStatusMapping = new SchedulerCustomFieldMapping("CustomStatus", "CustomStatus");
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(customNameMapping);
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(customStatusMapping);
        }
        private void scheduler_EditAppointmentFormShowing(object sender, DevExpress.Xpf.Scheduler.EditAppointmentFormEventArgs e) {
            SchedulerControl control = sender as SchedulerControl;
            if(Object.Equals(control,null))
                return;
            e.Form = new MyAppointmentEditForm(sender as SchedulerControl, e.Appointment);
            e.AllowResize = false;
        }
    }
}
