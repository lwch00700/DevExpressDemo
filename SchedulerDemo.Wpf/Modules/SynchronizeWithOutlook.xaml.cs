using System;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Exchange;
using DevExpress.XtraScheduler.Outlook;
using DevExpress.Xpf.Core;
using SchedulerDemo.Internal;

namespace SchedulerDemo {
    public partial class SynchronizeWithOutlook : SchedulerDemoModule {
        public SynchronizeWithOutlook() {
            InitializeComponent();
            FillCalendarNamesCombo();
            SchedulerBindingList<CustomEvent> eventList = new SchedulerBindingList<CustomEvent>();
            scheduler.Storage.AppointmentStorage.DataSource = eventList;

            scheduler.Storage.AppointmentStorage.Mappings.Start = "StartTime";
            scheduler.Storage.AppointmentStorage.Mappings.End = "EndTime";
            scheduler.Storage.AppointmentStorage.Mappings.Subject = "Subject";
            scheduler.Storage.AppointmentStorage.Mappings.AllDay = "AllDay";
            scheduler.Storage.AppointmentStorage.Mappings.Description = "Description";
            scheduler.Storage.AppointmentStorage.Mappings.Label = "Label";
            scheduler.Storage.AppointmentStorage.Mappings.Location = "Location";
            scheduler.Storage.AppointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            scheduler.Storage.AppointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            scheduler.Storage.AppointmentStorage.Mappings.ResourceId = "OwnerId";
            scheduler.Storage.AppointmentStorage.Mappings.Status = "Status";
            scheduler.Storage.AppointmentStorage.Mappings.Type = "EventType";
        }

        protected bool IsCancelForRecurring { get { return Object.Equals(cbCancel.EditValue, UsedAppointmentType.Recurring); } }
        protected bool IsCancelForNonRecurring { get { return Object.Equals(cbCancel.EditValue, UsedAppointmentType.NonRecurring); } }
        protected string CalendarName { get { return cbCalendarFolderPaths.Text; } }

        private void FillCalendarNamesCombo() {
            try {
                cbCalendarFolderPaths.Items.Clear();
                cbCalendarFolderPaths.Items.AddRange(DemoUtils.OutlookCalendarPaths);
            }
            finally {
                cbCalendarFolderPaths.SelectedIndex = 0;
            }
        }

        #region supplementary functionality
        void BeforeSynchronization(int objectCount) {
            progressBar.Value = 0;
            progressBar.Maximum = objectCount;
        }
        void AfterSynchronization() {
            progressBar.Value = 0;
        }
        #endregion

        private void btnSynchronize_Click(object sender, EventArgs e) {
            try {
                Synchronize();
            }
            catch (Exception ex) {
                DemoUtils.ReportOutlookError("synchronize XtraScheduler with Microsoft Outlook", ex.Message);
            }
        }
        void Synchronize() {
            OutlookImportSynchronizer synchronizer = new OutlookImportSynchronizer(scheduler.Storage.InnerStorage);
            ((ISupportCalendarFolders)synchronizer).CalendarFolderName = CalendarName;
            synchronizer.ForeignIdFieldName = "EntryID";
            synchronizer.AppointmentSynchronizing += new AppointmentSynchronizingEventHandler(synchronizer_AppointmentSynchronizing);
            synchronizer.OnException += new ExchangeExceptionEventHandler(synchronizer_OnException);

            BeforeSynchronization(synchronizer.SourceObjectCount);
            try {
                synchronizer.Synchronize();
            }
            finally {
                AfterSynchronization();
            }
        }

        void synchronizer_OnException(object sender, ExchangeExceptionEventArgs e) {
            string errText = e.OriginalException.Message;
            OutlookExchangeExceptionEventArgs args = e as OutlookExchangeExceptionEventArgs;
            if (args != null) {
                if (args.OutlookAppointment != null) {
                    errText += String.Format("\r\nEvent '{0}' started on {1:D} at {2}\n", args.OutlookAppointment.Subject,
                            args.OutlookAppointment.Start, args.OutlookAppointment.Start.TimeOfDay);
                }
            }
            AppointmentImportSynchronizer synchronizer = (AppointmentImportSynchronizer)sender;
            synchronizer.Terminate();
            e.Handled = true;
            throw e.OriginalException;
        }
        void synchronizer_AppointmentSynchronizing(object sender, AppointmentSynchronizingEventArgs e) {
            progressBar.Value += 1;
            DispatcherHelper.DoEvents();

            Appointment apt = e.Appointment;
            bool cancel = false;
            if (apt != null) {
                cancel = apt.IsRecurring ? IsCancelForRecurring : IsCancelForNonRecurring;
            }

            if (cancel) {
                e.Cancel = true;
                return;
            }

            switch (e.Operation) {
                case SynchronizeOperation.Create:
                    if ((bool)chDontCreateNew.IsChecked) {
                        e.Cancel = true;
                    }
                    break;

                case SynchronizeOperation.Delete:
                    if ((bool)chDontDeleteExisting.IsChecked) {
                        e.Cancel = true;
                    }
                    break;

                case SynchronizeOperation.Replace:
                    if ((bool)chDeleteInsteadReplace.IsChecked) {
                        e.Operation = SynchronizeOperation.Delete;
                    }
                    break;
            }

        }
    }
}
