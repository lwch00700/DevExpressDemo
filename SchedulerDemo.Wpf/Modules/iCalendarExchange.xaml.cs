using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.XtraScheduler;
using System.IO;
using DevExpress.XtraScheduler.iCalendar;
using DevExpress.Xpf.Core;
using System.Text;

namespace SchedulerDemo {
    public partial class iCalendarExchange : SchedulerDemoModule {
        DateTime baseDate = DateTime.Today;
        public iCalendarExchange() {
            InitializeComponent();
            InitializeAppointments();
        }

        private void InitializeAppointments() {
            Appointment apt1 = CreateAppointment(this.baseDate, "Mr.Brown", 1);
            Appointment apt2 = CreateAppointment(baseDate.AddDays(1), "Repair", 2);

            Appointment apt3 = CreateAppointment(baseDate, "Wash", 3, AppointmentType.Pattern);
            apt3.RecurrenceInfo.OccurrenceCount = 3;
            apt3.RecurrenceInfo.Periodicity = 2;
            apt3.RecurrenceInfo.Start = apt3.Start;
            apt3.RecurrenceInfo.Range = RecurrenceRange.OccurrenceCount;

            Appointment apt4 = CreateAppointment(baseDate.AddDays(-2), "Mr.Green", 4);
            apt4.AllDay = true;

            scheduler.Storage.AppointmentStorage.Add(apt1);
            scheduler.Storage.AppointmentStorage.Add(apt2);
            scheduler.Storage.AppointmentStorage.Add(apt3);
            scheduler.Storage.AppointmentStorage.Add(apt4);
        }

        Appointment CreateAppointment(DateTime start, string subject, int label, AppointmentType type = AppointmentType.Normal) {
            Appointment apt = scheduler.Storage.CreateAppointment(type);
            apt.Subject = subject;
            Random rnd = DemoUtils.RandomInstance;
            apt.AllDay = rnd.Next(0, 5) == 0;

            int rangeInMinutes = 60 * 24;
            apt.Start = start + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            TimeSpan duration = TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            if (duration.Ticks == 0)
                duration = TimeSpan.FromMinutes(5);

            apt.End = apt.Start + duration;
            apt.LabelKey = label;
            return apt;
        }

        protected bool IsCancelForRecurring { get { return Object.Equals(cbCancel.EditValue, UsedAppointmentType.Recurring); } }
        protected bool IsCancelForNonRecurring { get { return Object.Equals(cbCancel.EditValue, UsedAppointmentType.NonRecurring); } }

        private void btnImport_Click(object sender, RoutedEventArgs e) {
            BeforeImportActions();
            using (Stream stream = CreateInputStream(txtVCalendar.Text))
                ImportAppointments(stream);
            AfterImportActions();
        }

        void BeforeImportActions() {
            progressBar.Value = 0;
            txtVCalendar.TextChanged -= new TextChangedEventHandler(txtVCalendar_TextChanged);
        }

        void AfterImportActions() {
            txtVCalendar.TextChanged += new TextChangedEventHandler(txtVCalendar_TextChanged);
            progressBar.Value = 0;
        }

        private void txtVCalendar_TextChanged(object sender, TextChangedEventArgs e) {
            btnImport.IsEnabled = txtVCalendar.Text.Length != 0 ? true : false;
        }

        public static Stream CreateInputStream(string text) {
            byte[] buf = Encoding.Unicode.GetBytes(text);
            return new MemoryStream(buf, 0, buf.Length);
        }

        void ImportAppointments(Stream stream) {
            if (stream == null)
                return;
            scheduler.Storage.AppointmentStorage.Clear();
            iCalendarImporter importer = new iCalendarImporter(scheduler.Storage.InnerStorage);
            importer.CalendarStructureCreated += new iCalendarStructureCreatedEventHandler(importer_CalendarStructureCreated);
            importer.AppointmentImporting += new AppointmentImportingEventHandler(importer_AppointmentImporting);
            importer.OnException += new ExchangeExceptionEventHandler(importer_OnException);
            importer.Import(stream);
        }

        void importer_OnException(object sender, ExchangeExceptionEventArgs e) {
            iCalendarParseExceptionEventArgs args = e as iCalendarParseExceptionEventArgs;
            if (args != null) {
                DXMessageBox.Show(String.Format("Can't parse line '{1}' at {0} index", args.LineIndex, args.LineText));
                iCalendarImporter importer = (iCalendarImporter)sender;
                importer.Terminate();
            }
            e.Handled = true;
        }
        void importer_AppointmentImporting(object sender, AppointmentImportingEventArgs e) {
            progressBar.Value += 1;
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new System.Threading.ThreadStart(delegate { }));

            bool cancel = e.Appointment.IsRecurring ? IsCancelForRecurring : IsCancelForNonRecurring;
            if (cancel) {
                e.Cancel = true;
            }
        }
        void importer_CalendarStructureCreated(object sender, iCalendarStructureCreatedEventArgs e) {
            iCalendarImporter importer = (iCalendarImporter)sender;
            progressBar.Maximum = importer.SourceObjectCount;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e) {
            progressBar.Value = 0;
            MemoryStream ms = new MemoryStream();
            using (StreamReader sr = new StreamReader(ms)) {
                ExportAppointments(ms);
                ms.Seek(0, SeekOrigin.Begin);
                txtVCalendar.Text = sr.ReadToEnd();
            }
            progressBar.Value = 0;
        }

        void ExportAppointments(Stream stream) {
            if (stream == null)
                return;
            iCalendarExporter exporter = new iCalendarExporter(scheduler.Storage.InnerStorage);
            int sourceObjectCount = exporter.SourceObjectCount;
            progressBar.Maximum = sourceObjectCount;
            exporter.ProductIdentifier = "-//Developer Express Inc.//DXScheduler iCalendarExchangeDemo//EN";
            exporter.AppointmentExporting += new AppointmentExportingEventHandler(exporter_AppointmentExporting);
            exporter.Export(stream);
        }

        void exporter_AppointmentExporting(object sender, AppointmentExportingEventArgs e) {
            progressBar.Value += 1;
            DispatcherHelper.DoEvents();
            bool cancel = e.Appointment.IsRecurring ? IsCancelForRecurring : IsCancelForNonRecurring;
            if (cancel) {
                e.Cancel = true;
            }
        }
        void scheduler_Drop(object sender, DragEventArgs e) {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (fileNames == null || fileNames.Length == 0)
                return;

            foreach (string fileName in fileNames) {
                if (File.Exists(fileName)) {
                    using (Stream stream = File.Open(fileName, FileMode.Open)) {
                        ImportAppointments(stream);
                    }
                }
            }
        }
    }
}
