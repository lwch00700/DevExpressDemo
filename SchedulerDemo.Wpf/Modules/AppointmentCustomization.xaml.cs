using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using DevExpress.Xpf.Scheduler;
using DevExpress.Xpf.Scheduler.Drawing;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Native;
using System.Windows.Markup;
using System.IO;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using System.Windows.Media;



namespace SchedulerDemo {
    public partial class AppointmentCustomization : SchedulerDemoModule {
        Dictionary<Resource, BitmapImage> resourceImages = new Dictionary<Resource, BitmapImage>();
        public AppointmentCustomization() {
            InitializeComponent();
            InitializeScheduler();
        }

        void scheduler_AppointmentViewInfoCustomizing(object sender, DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs e) {
            AppointmentViewInfo viewInfo = e.ViewInfo;
            if(chkCustomText.IsChecked.GetValueOrDefault())
                UpdateText(viewInfo);
            CustomAppointmentData data = new CustomAppointmentData();
            data.Filtered = false;
            data.ShowImages = chkCustomImage.IsChecked.GetValueOrDefault();
            viewInfo.CustomViewInfo = data;

            if(!IsEmptyFilter(cbSearch.Text)) {
                string searchText = cbSearch.Text.ToUpper();
                if(IsAppointmentFiltered(searchText, e.ViewInfo.Appointment))
                    data.Filtered = true;
            }

            if(!data.Filtered) {
                IAppointmentLabel label = this.scheduler.Storage.AppointmentStorage.Labels.CreateNewLabel(string.Empty);
                label.SetColor(Colors.LightGray);
                viewInfo.Label = label;
                IAppointmentStatus status = this.scheduler.Storage.AppointmentStorage.Statuses.CreateNewStatus(string.Empty, string.Empty);
                status.Type = AppointmentStatusType.Custom;
                status.SetBrush(new SolidColorBrush(Colors.LightGray));
                viewInfo.Status = status;
            }

        }
        private bool IsEmptyFilter(string text) {
            return String.IsNullOrEmpty(text) || text == "(None)";
        }
        private static bool IsAppointmentFiltered(string searchText, Appointment apt) {
            return apt.Subject.ToUpper().Contains(searchText) || apt.Location.ToUpper().Contains(searchText);
        }
        void UpdateText(AppointmentViewInfo viewInfo) {
            Appointment appointment = viewInfo.Appointment;
            string description = viewInfo.Description;
            if(appointment.IsRecurring)
                description = String.Format("{0}\r\nOccurs on {1}", description, RecurrenceInfo.GetDescription(appointment, DateTimeHelper.ConvertFirstDayOfWeek(scheduler.OptionsView.FirstDayOfWeek)));
            viewInfo.Description = description;
            viewInfo.Location = (String.IsNullOrEmpty(viewInfo.Appointment.Location)) ? String.Empty : String.Format("- [{0}]", viewInfo.Appointment.Location);
        }
        void EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            UpdateSchedulerView();
        }
        private void cbSearch_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            scheduler.ActiveView.LayoutChanged();
        }
        private void UpdateSchedulerView() {
            scheduler.ActiveView.LayoutChanged();
        }
    }

    public class CustomAppointmentData : BindableBase {
        bool filteredCore;
        bool showImagesCore;

        public bool Filtered {
            get { return filteredCore; }
            set { SetProperty(ref filteredCore, value, "Filtered"); }
        }
        public bool ShowImages {
            get { return showImagesCore; }
            set { SetProperty(ref showImagesCore, value, "ShowImages"); }
        }
    }
}
