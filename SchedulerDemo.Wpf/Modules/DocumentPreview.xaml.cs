using System;
using System.Windows;
using System.Collections.Generic;
using DevExpress.XtraScheduler;
using DevExpress.Xpf.Printing;
using DevExpress.XtraScheduler.Reporting;
using DevExpress.Xpf.Scheduler.Reporting;
using DevExpress.Xpf.Scheduler.Reporting.UI;

namespace SchedulerDemo {
    public partial class DocumentPreview : SchedulerDemoModule {

        public DocumentPreview() {
            InitializeComponent();
            SchedulerDataHelper.DataBind(storage);
            SubscribePrintAdapterEvents();
            storage.ResourceStorage.Mappings.Image = "";
            this.DataContext = this;
            UpdateInterval();
            UpdatePreview();
        }
        #region ReportTemplateInfoSource
        public List<ISchedulerReportTemplateInfo> ReportTemplateInfoSource {
            get { return (List<ISchedulerReportTemplateInfo>)GetValue(ReportTemplateInfoSourceProperty); }
            set { SetValue(ReportTemplateInfoSourceProperty, value); }
        }
        public static readonly DependencyProperty ReportTemplateInfoSourceProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<DocumentPreview, List<ISchedulerReportTemplateInfo>>("ReportTemplateInfoSource", SchedulerDataHelper.GetReportTemplateCollection());
        #endregion
        #region ActiveReportTemplateIndex
        public int ActiveReportTemplateIndex {
            get { return (int)GetValue(ActiveReportTemplateIndexProperty); }
            set { SetValue(ActiveReportTemplateIndexProperty, value); }
        }
        public static readonly DependencyProperty ActiveReportTemplateIndexProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<DocumentPreview, int>("ActiveReportTemplateIndex", 0);
        #endregion
        #region PrintInterval
        public TimeInterval PrintInterval {
            get { return (TimeInterval)GetValue(PrintIntervalProperty); }
            set { SetValue(PrintIntervalProperty, value); }
        }
        public static readonly DependencyProperty PrintIntervalProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<DocumentPreview, TimeInterval>("PrintInterval", null);
        #endregion
        #region IntervalStart
        public DateTime IntervalStart {
            get { return (DateTime)GetValue(IntervalStartProperty); }
            set { SetValue(IntervalStartProperty, value); }
        }
        public static readonly DependencyProperty IntervalStartProperty = DependencyProperty.Register("IntervalStart", typeof(DateTime), typeof(DocumentPreview), new PropertyMetadata(new DateTime(2010, 07, 13), new PropertyChangedCallback(OnIntervalStartChanged)));
        protected static void OnIntervalStartChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((DocumentPreview)o).UpdateInterval();
            ((DocumentPreview)o).UpdatePreview();
        }
        #endregion
        #region IntervalEnd
        public DateTime IntervalEnd {
            get { return (DateTime)GetValue(IntervalEndProperty); }
            set { SetValue(IntervalEndProperty, value); }
        }
        public static readonly DependencyProperty IntervalEndProperty = DependencyProperty.Register("IntervalEnd", typeof(DateTime), typeof(DocumentPreview), new PropertyMetadata(new DateTime(2010, 07, 20), new PropertyChangedCallback(OnIntervalEndChanged)));
        protected static void OnIntervalEndChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            ((DocumentPreview)o).UpdateInterval();
            ((DocumentPreview)o).UpdatePreview();
        }
        #endregion
        #region PreviewModel
        public XtraReportPreviewModel PreviewModel {
            get { return (XtraReportPreviewModel)GetValue(PreviewModelProperty); }
            set { SetValue(PreviewModelProperty, value); }
        }
        public static readonly DependencyProperty PreviewModelProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<DocumentPreview, XtraReportPreviewModel>("PreviewModel", null);
        #endregion

        private void UpdateInterval() {
            if (IntervalStart > IntervalEnd)
                PrintInterval = new TimeInterval(IntervalStart, IntervalStart.AddDays(1));
            PrintInterval = new TimeInterval(IntervalStart, IntervalEnd);
        }

        private void UpdatePreview() {
            XtraSchedulerReport report = new XtraSchedulerReport();
            report.LoadLayout(ReportTemplateInfoSource[ActiveReportTemplateIndex].ReportTemplatePath);
            adapter.TimeInterval = new TimeInterval(IntervalStart, IntervalEnd);
            report.SchedulerAdapter = adapter.SchedulerAdapter;
            report.CreateDocument();
            PreviewModel = new XtraReportPreviewModel(report);
        }

        private ResourceBaseCollection GetAvailableResources() {
            return storage.ResourceStorage.Items;
        }

        private void PrintAdapter_ValidateResources(object sender, DevExpress.XtraScheduler.Reporting.ResourcesValidationEventArgs e) {
            e.Resources.Clear();
            e.Resources.AddRange(GetAvailableResources());
        }

        private void SubscribePrintAdapterEvents() {
            adapter.SchedulerStorage = storage;
            adapter.ValidateResources += new DevExpress.XtraScheduler.Reporting.ResourcesValidationEventHandler(PrintAdapter_ValidateResources);
        }

        private void UnsubscribePrintAdapterEvents() {
            adapter.ValidateResources -= new DevExpress.XtraScheduler.Reporting.ResourcesValidationEventHandler(PrintAdapter_ValidateResources);
        }

        private void DataContextChangedMethod(object sender, DependencyPropertyChangedEventArgs e) {
            FrameworkElement element = (FrameworkElement)sender;
            if (element.DataContext != this)
                element.DataContext = this;
        }

        private void cbe_reportTemplate_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            UpdatePreview();
        }
    }
}
