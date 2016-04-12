using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class AutoFilter : SpreadsheetDemoModule {
        public AutoFilter() {
            InitializeComponent();

            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("AutoFilter_template.xlsx"));
            ribbonControl1.SelectedPage = pageData;
        }
    }
}
