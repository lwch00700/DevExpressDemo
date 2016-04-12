using System;
using System.Windows;
using GridDemo;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Utils;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    [CodeFile("ModuleResources/PrintMasterDetailViewModel.(cs)")]
    [CodeFile("Controls/DemoModuleControl.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class PrintingMasterDetail : PrintViewGridDemoModule {
        protected override DXTabControl DXTabControl { get { return tabControl; } }

        public PrintingMasterDetail() {
            InitializeComponent();
            Loaded += PrintingMasterDetail_Loaded;
        }

        private void PrintingMasterDetail_Loaded(object sender, RoutedEventArgs e) {
            grid.ExpandMasterRow(2);
            ((GridControl)grid.GetVisibleDetail(2)).ExpandMasterRow(0);
            grid.ExpandMasterRow(4, CustomersDescriptor);
            grid.ExpandMasterRow(6);
            ShowPreviewInNewTab();
        }

        protected override void ShowPreviewInNewTab() {
            ShowPrintPreviewInNewTab(grid, tabControl, "Preview");
        }
        protected void newTabButton_Click(object sender, RoutedEventArgs e) {
            ShowPreviewInNewTab();
        }
        protected void tabControl_TabHidden(object sender, TabControlTabHiddenEventArgs e) {
            DisposePrintPreviewTabContent((DXTabItem)DXTabControl.Items[e.TabIndex]);
        }
    }
}
