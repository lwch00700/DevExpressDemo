using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using DevExpress.Xpo;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/XPOInstantFeedbackTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/XPOInstantFeedbackClasses.(cs)")]
    [CodeFile("ModuleResources/HyperLinkAttachedBehavior.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class XPOInstantFeedback : GridDemoModule {
        public XPOInstantFeedback() {
            XPOServiceHelper.SetupXpoLayer();
            InitializeComponent();
            waitIndicatorList.EditValueChanged += waitIndicatorList_EditValueChanged;
        }

        void XPInstantFeedbackDataSource_ResolveSession(object sender, ResolveSessionEventArgs e) {
            Session s = new UnitOfWork();
            e.Session = s;
        }
        void XPInstantFeedbackDataSource_DismissSession(object sender, ResolveSessionEventArgs e) {
            IDisposable session = e.Session as IDisposable;
            if(session != null) {
                session.Dispose();
            }
        }
        private void waitIndicatorList_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(e.NewValue.ToString() == "Custom") {
                view.WaitIndicatorType = WaitIndicatorType.Panel;
                view.WaitIndicatorStyle = Resources["CustomWaitIndicatorStyle"] as Style;
            }
            else {
                view.ClearValue(GridViewBase.WaitIndicatorStyleProperty);
                view.WaitIndicatorType = (WaitIndicatorType)e.NewValue;
            }
        }
        protected override DataViewBase GetExportView() {
            return null;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
