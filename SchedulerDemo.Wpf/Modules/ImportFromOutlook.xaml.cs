using System;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Exchange;
using DevExpress.XtraScheduler.Outlook;
using DevExpress.Xpf.Core;

namespace SchedulerDemo {
    public partial class ImportFromOutlook : SchedulerDemoModule {
        public ImportFromOutlook() {
            InitializeComponent();
            FillCalendarNamesCombo();
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

        private void btnImport_Click(object sender, System.Windows.RoutedEventArgs e) {
            try {
                Import();
            }
            catch (Exception ex) {
                DemoUtils.ReportOutlookError("import appointments from Microsoft Outlook", ex.Message);
            }
        }

        void Import() {
            DevExpress.XtraScheduler.Outlook.OutlookImport importer = new DevExpress.XtraScheduler.Outlook.OutlookImport(scheduler.Storage.InnerStorage);
            ((ISupportCalendarFolders)importer).CalendarFolderName = CalendarName;
            importer.AppointmentImporting += new AppointmentImportingEventHandler(importer_AppointmentImporting);
            importer.OnException += new ExchangeExceptionEventHandler(importer_OnException);

            BeforeImport(importer.SourceObjectCount);
            try {
                importer.Import(System.IO.Stream.Null);
            }
            finally {
                AfterImport();
            }
        }

        void BeforeImport(int objectCount) {
            progressBar.Value = 0;
            progressBar.Maximum = objectCount;
        }
        void AfterImport() {
            progressBar.Value = 0;
        }

        void importer_OnException(object sender, ExchangeExceptionEventArgs e) {
            string errText = e.OriginalException.Message;
            OutlookExchangeExceptionEventArgs args = e as OutlookExchangeExceptionEventArgs;
            if (args != null) {
                if (args.OutlookAppointment != null) {
                    errText += String.Format("\r\nEvent '{0}' started on {1:D} at {2}\n", args.OutlookAppointment.Subject,
                            args.OutlookAppointment.Start, args.OutlookAppointment.Start.TimeOfDay);
                }
            }
            AppointmentImporter importer = (AppointmentImporter)sender;
            importer.Terminate();
            e.Handled = true;
            throw e.OriginalException;
        }
        void importer_AppointmentImporting(object sender, AppointmentImportingEventArgs e) {
            OutlookAppointmentImportingEventArgs args = (OutlookAppointmentImportingEventArgs)e;

            progressBar.Value += 1;
            DispatcherHelper.DoEvents();

            bool cancel = args.OutlookAppointment.IsRecurring ? IsCancelForRecurring : IsCancelForNonRecurring;
            if (cancel) {
                e.Cancel = true;
            }
        }
    }
}
