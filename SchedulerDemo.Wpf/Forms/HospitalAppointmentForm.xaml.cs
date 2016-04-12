using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.XtraScheduler.Localization;
using System;
using DevExpress.XtraScheduler.UI;
using System.Windows;


namespace SchedulerDemo.Forms {
    public partial class HospitalAppointmentForm : UserControl {
        public HospitalAppointmentForm() {
            InitializeComponent();
        }

        void OnEdtEndTimeValidate(object sender, ValidationEventArgs e) {
            if(e.Value == null)
                return;
            e.IsValid = IsValidInterval(edtStartDate.DateTime, Convert.ToDateTime(edtStartTime.EditValue).TimeOfDay, edtEndDate.DateTime, ((DateTime)e.Value).TimeOfDay);
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
        }
        void OnEdtEndDateValidate(object sender, ValidationEventArgs e) {
            if(e.Value == null)
                return;
            e.IsValid = IsValidInterval(edtStartDate.DateTime, Convert.ToDateTime(edtStartTime.EditValue).TimeOfDay, (DateTime)e.Value, Convert.ToDateTime(edtEndTime.EditValue).TimeOfDay);
            e.ErrorContent = SchedulerLocalizer.GetString(SchedulerStringId.Msg_InvalidEndDate);
        }
        protected internal virtual bool IsValidInterval(DateTime startDate, TimeSpan startTime, DateTime endDate, TimeSpan endTime) {
            return AppointmentFormControllerBase.ValidateInterval(startDate, startTime, endDate, endTime);
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            LayoutUpdated += new EventHandler(OnLayoutUpdated);
            subjectEdit.Focus();
        }
        void OnLayoutUpdated(object sender, EventArgs e) {
            LayoutUpdated -= new EventHandler(OnLayoutUpdated);
            subjectEdit.Focus();
        }
    }
}
