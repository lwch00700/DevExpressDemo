using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Grouping : SpreadsheetDemoModule {
        public Grouping() {
            InitializeComponent();

            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("OutlineGrouping_template.xlsx"), DocumentFormat.Xlsx);

            ribbonControl1.SelectedPage = pageData;
        }
    }
}
