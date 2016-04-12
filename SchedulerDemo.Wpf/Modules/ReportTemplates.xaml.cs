using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Scheduler.Reporting;
using DevExpress.Xpf.Scheduler.Reporting.UI;

namespace SchedulerDemo {
    public partial class ReportTemplates : SchedulerDemoModule {

        public ReportTemplates() {
            ViewModel = new SchedulerPrintingSettingsFormViewModel();

            InitializeComponent();
            InitializeScheduler();
            scheduler.Storage.ResourceStorage.Mappings.Image = "";

            InitializeSettingsFormViewModel();

            this.DataContext = this;
            SubscribePrintAdapterEvents();

        }
        public SchedulerPrintingSettingsFormViewModel ViewModel { get; private set; }

        void InitializeSettingsFormViewModel() {
            ViewModel.ReportInstance = new XtraSchedulerReport();
            ViewModel.SchedulerPrintAdapter = adapter;
            ViewModel.AvailableResources = GetAvailableResources();
            ViewModel.TimeInterval = new TimeInterval(scheduler.Start, scheduler.Start.AddDays(7));
            ViewModel.ReportTemplateInfoSource = SchedulerDataHelper.GetReportTemplateCollection();
            ViewModel.ActiveReportTemplateIndex = 0;
        }

        private ResourceBaseCollection GetAvailableResources() {
            return scheduler.Storage.ResourceStorage.Items;
        }

        private ResourceBaseCollection GetOnScreenResources() {
            return scheduler.ActiveView.GetResources();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e) {
            ViewModel.OnScreenResources = GetOnScreenResources();
            SchedulerPrintingSettingsForm printingSettingsForm = CreatePrintingSettingsForm();
            printingSettingsForm.ShowDialog();
        }

        private SchedulerPrintingSettingsForm CreatePrintingSettingsForm() {
            SchedulerPrintingSettingsForm form = new SchedulerPrintingSettingsForm(ViewModel);
            form.FlowDirection = scheduler.FlowDirection;
            form.PreviewButtonClick += new EventHandler(printingSettingsForm_PreviewButtonClick);
            form.Closed += new EventHandler(printingSettingsForm_Closed);
            return form;
        }

        void printingSettingsForm_Closed(object sender, EventArgs e) {
            SchedulerPrintingSettingsForm form = sender as SchedulerPrintingSettingsForm;
            form.PreviewButtonClick -= new EventHandler(printingSettingsForm_PreviewButtonClick);
            form.Closed -= new EventHandler(printingSettingsForm_Closed);
        }

        void printingSettingsForm_PreviewButtonClick(object sender, EventArgs e) {
            SchedulerPrintingSettingsForm form = sender as SchedulerPrintingSettingsForm;
            ISchedulerPrintingSettings settings = (ISchedulerPrintingSettings)form.PrintingSettings;
            string reportTemplatePath = settings.GetReportTemplatePath();
            if (reportTemplatePath.ToLower().Contains("trifold")) {
                settings.SchedulerPrintAdapter.EnableSmartSync = true;
            }
        }

        private void PrintAdapter_ValidateResources(object sender, DevExpress.XtraScheduler.Reporting.ResourcesValidationEventArgs e) {
            e.Resources.Clear();
            e.Resources.AddRange(ViewModel.PrintResources);
        }

        private void SubscribePrintAdapterEvents() {
            adapter.ValidateResources += new DevExpress.XtraScheduler.Reporting.ResourcesValidationEventHandler(PrintAdapter_ValidateResources);
        }

        private void UnsubscribePrintAdapterEvents() {
            adapter.ValidateResources -= new DevExpress.XtraScheduler.Reporting.ResourcesValidationEventHandler(PrintAdapter_ValidateResources);
        }

    }
}
