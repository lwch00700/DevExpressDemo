using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Collections.Generic;
using System.IO;
using DevExpress.Xpf.Core.Native;
using System.Windows.Media.Imaging;


namespace SchedulerDemo {
    public partial class AppointmentStyles : SchedulerDemoModule {
        Dictionary<Resource, BitmapImage> resourceImages = new Dictionary<Resource, BitmapImage>();

        public AppointmentStyles() {
            InitializeComponent();
            InitializeScheduler();

            ApplyHorizontalAppointmentStyle();
            ApplyVerticalAppointmentStyle();

            ApplyAppointmentToolTipContentTemplate();
        }
        private void ApplyHorizontalAppointmentStyle() {
            StyleSelector styleSelector = (StyleSelector)Resources["HorizontalAppointmentStyleSelector"];
            foreach(SchedulerViewBase view in Views)
                view.HorizontalAppointmentStyleSelector = styleSelector;
        }
        private void ApplyVerticalAppointmentStyle() {
            StyleSelector styleSelector = (StyleSelector)FindResource("VerticalAppointmentStyleSelector");
            scheduler.DayView.VerticalAppointmentStyleSelector = styleSelector;
            scheduler.WorkWeekView.VerticalAppointmentStyleSelector = styleSelector;
        }
        private void ApplyAppointmentToolTipContentTemplate() {
            DataTemplate template = (DataTemplate)this.FindResource("AppointmentTooltipContentTemplate");
            foreach(SchedulerViewBase view in Views)
                view.AppointmentToolTipContentTemplate = template;
        }
        private void chHorizontalAppointmentStyle_Checked(object sender, RoutedEventArgs e) {
            ApplyHorizontalAppointmentStyle();
        }
        private void chHorizontalAppointmentStyle_Unchecked(object sender, RoutedEventArgs e) {
            foreach(SchedulerViewBase view in Views) {
                view.ClearValue(SchedulerViewBase.HorizontalAppointmentStyleSelectorProperty);
            }
        }

        private void chVerticalAppointmentStyle_Checked(object sender, RoutedEventArgs e) {
            ApplyVerticalAppointmentStyle();
        }
        private void chVerticalAppointmentStyle_Unchecked(object sender, RoutedEventArgs e) {
            scheduler.DayView.ClearValue(DevExpress.Xpf.Scheduler.DayView.VerticalAppointmentStyleSelectorProperty);
            scheduler.WorkWeekView.ClearValue(DevExpress.Xpf.Scheduler.WorkWeekView.VerticalAppointmentStyleSelectorProperty);
        }

        private void scheduler_ActiveViewChanged(object sender, EventArgs e) {
            if(this.chVerticalAppointmentStyle == null)
                return;
            SchedulerViewType viewType = ((SchedulerControl)sender).ActiveView.Type;
            this.chVerticalAppointmentStyle.IsEnabled = Object.Equals(viewType, SchedulerViewType.Day) || Object.Equals(viewType, SchedulerViewType.WorkWeek);
        }


        private void scheduler_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e) {
            if(!chkAppointmentToolTip.IsChecked.GetValueOrDefault() == true)
                return;

            AppointmentViewInfo viewInfo = e.ViewInfo;
            Resource resource = scheduler.Storage.ResourceStorage.GetResourceById(viewInfo.Appointment.ResourceId);
            if (Object.Equals(resource.Id, EmptyResourceId.Id) || resource.ImageBytes == null)
                viewInfo.CustomViewInfo = null;
            else {
                if(!this.resourceImages.ContainsKey(resource))
                    this.resourceImages[resource] = resource.GetImage() as BitmapImage;
                viewInfo.CustomViewInfo = this.resourceImages[resource];
            }

        }

        private void chkAppointmentToolTip_Checked(object sender, RoutedEventArgs e) {
            ApplyAppointmentToolTipContentTemplate();
        }

        private void chkAppointmentToolTip_Unchecked(object sender, RoutedEventArgs e) {
            foreach(SchedulerViewBase view in Views)
                view.ClearValue(SchedulerViewBase.AppointmentToolTipContentTemplateProperty);
        }
    }

}
