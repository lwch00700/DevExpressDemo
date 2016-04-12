using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Comments : SpreadsheetDemoModule {
        public Comments() {
            InitializeComponent();

            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("Comments_template.xlsx"), DocumentFormat.Xlsx);

            ribbonControl1.SelectedPage = pageReview;
        }
    }
}
