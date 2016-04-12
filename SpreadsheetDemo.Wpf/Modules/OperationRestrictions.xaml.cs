using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.PropertyGrid;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SpreadsheetDemo {
    public partial class OperationRestrictions : SpreadsheetDemoModule {
        int optionsGroupCounter = 11;

        public OperationRestrictions() {
            InitializeComponent();

            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("OperationRestrictions_template.xlsx"), DocumentFormat.Xlsx);

            propertyGridControl1.SelectedObject = new BehaviorOptionsProvider(spreadsheetControl1.Options.Behavior);
            SubscribeEvents();

            ribbonControl1.SelectedPage = pageHome;
        }

        void SubscribeEvents() {
            propertyGridControl1.CustomExpand += OnCustomExpand;
        }
        void UnsubscribeEvents() {
            propertyGridControl1.CustomExpand -= OnCustomExpand;
        }

        void OnCustomExpand(object sender, CustomExpandEventArgs args) {
            args.IsExpanded = true;
            args.Handled = true;

            optionsGroupCounter--;
            if (optionsGroupCounter == 0)
                UnsubscribeEvents();
        }
    }
}
