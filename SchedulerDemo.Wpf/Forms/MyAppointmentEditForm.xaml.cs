using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.UI;
using DevExpress.Xpf.Core;
using DevExpress.XtraScheduler.Localization;
using System.Globalization;
using DevExpress.Xpf.Editors;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler.Native;


namespace SchedulerDemo.Forms {
    public partial class MyAppointmentEditForm : UserControl {
        SchedulerControl control;
        Appointment appointment;
        MyAppointmentFormController controller;
        RecurrenceVisualController recurrenceVisualController;

        public MyAppointmentEditForm(SchedulerControl control, Appointment appointment) {
            this.control = control;
            this.appointment = appointment;
            this.controller = new MyAppointmentFormController(Control, Appointment);
            this.recurrenceVisualController = new RecurrenceVisualController(Controller);
            RecurrenceVisualController.EnableYearlyRecurrence = false;
            RecurrenceVisualController.EnableWeeklyRecurrence = false;
            RecurrenceVisualController.EnableNoneRecurrence = false;
            InitializeComponent();
            Loaded += new RoutedEventHandler(OnLoaded);
        }

        public SchedulerControl Control { get { return control; } }
        public Appointment Appointment { get { return appointment; } }
        public MyAppointmentFormController Controller { get { return controller; } }
        protected internal bool IsNewAppointment { get { return controller != null ? controller.IsNewAppointment : true; } }
        public string TimeEditMask { get { return CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern; } }
        public RecurrenceVisualController RecurrenceVisualController { get { return recurrenceVisualController; } }
        public bool ShouldShowRecurrence { get { return !Appointment.IsOccurrence && Controller.ShouldShowRecurrenceButton; } }
        public TimeZoneHelper TimeZoneHelper { get { return Controller.TimeZoneHelper; } }

        void OnLoaded(object sender, RoutedEventArgs e) {
            Size currentSize = SchedulerFormBehavior.GetSize(this);
            SchedulerFormBehavior.SetSize(this, new Size(currentSize.Width + recurrenceGrid.Width, double.NaN));
        }
        void OnOKButtonClick(object sender, RoutedEventArgs e) {
            if(!controller.IsConflictResolved()) {
                DXMessageBox.Show(SchedulerLocalizer.GetString(SchedulerStringId.Msg_Conflict), "DXScheduler Demo", System.Windows.MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if(ShouldShowRecurrence)
                RecurrenceVisualController.ApplyRecurrence();

            Controller.ApplyChanges();

            SchedulerFormBehavior.Close(this, true);
        }
        private void OnCancelButtonClick(object sender, RoutedEventArgs e) {
            SchedulerFormBehavior.Close(this, false);
        }
        private void OnDeleteButtonClick(object sender, RoutedEventArgs e) {
            if(IsNewAppointment)
                return;

            Controller.DeleteAppointment();
            SchedulerFormBehavior.Close(this, false);
        }
        protected override void OnVisualParentChanged(DependencyObject oldParent) {
            base.OnVisualParentChanged(oldParent);
            if(oldParent == null) {
                UpdateContainerCaption(Controller.Subject);
            }
        }
        void UpdateContainerCaption(string subject) {
            if (Controller.IsNewAppointment)
                SchedulerFormBehavior.SetTitle(this, "New appointment");
            else
                SchedulerFormBehavior.SetTitle(this, "Edit - [" + Appointment.Subject + "]");
        }
        void OnEdtEndTimeValidate(object sender, ValidationEventArgs e) {
            if (e.Value == null)
                return;
            e.IsValid = IsValidInterval(Controller.Start.Date, Controller.Start.TimeOfDay, Controller.End.Date, ((DateTime)e.Value).TimeOfDay);
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
        }
        void OnEdtEndDateValidate(object sender, ValidationEventArgs e) {
            if (e.Value == null)
                return;
            e.IsValid = IsValidInterval(Controller.Start.Date, Controller.Start.TimeOfDay, (DateTime)e.Value, Controller.End.TimeOfDay);
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
        }
        protected internal virtual bool IsValidInterval(DateTime startDate, TimeSpan startTime, DateTime endDate, TimeSpan endTime) {
            return AppointmentFormControllerBase.ValidateInterval(startDate, startTime, endDate, endTime);
        }
    }

    public class MyAppointmentFormController : AppointmentFormController {

        public string CustomName { get { return (string)EditedAppointmentCopy.CustomFields["CustomName"]; } set { EditedAppointmentCopy.CustomFields["CustomName"] = value; } }
        public string CustomStatus { get { return (string)EditedAppointmentCopy.CustomFields["CustomStatus"]; } set { EditedAppointmentCopy.CustomFields["CustomStatus"] = value; } }

        string SourceCustomName { get { return (string)SourceAppointment.CustomFields["CustomName"]; } set { SourceAppointment.CustomFields["CustomName"] = value; } }
        string SourceCustomStatus { get { return (string)SourceAppointment.CustomFields["CustomStatus"]; } set { SourceAppointment.CustomFields["CustomStatus"] = value; } }

        public MyAppointmentFormController(SchedulerControl control, Appointment apt)
            : base(control, apt) {
        }

        public override bool IsAppointmentChanged() {
            if(base.IsAppointmentChanged())
                return true;
            return SourceCustomName != CustomName ||
                SourceCustomStatus != CustomStatus;
        }

        protected override void ApplyCustomFieldsValues() {
            SourceCustomName = CustomName;
            SourceCustomStatus = CustomStatus;
        }
    }
}
