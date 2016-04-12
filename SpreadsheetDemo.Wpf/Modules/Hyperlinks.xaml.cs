using System;
using System.Globalization;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class Hyperlinks : SpreadsheetDemoModule {
        public Hyperlinks() {
            InitializeComponent();
            InitializeWorkbook();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            ribbonControl1.SelectedPage = pageInsert;
        }
        void InitializeWorkbook() {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(DemoUtils.GetRelativePath("Hyperlinks_template.xlsx"));
        }
    }
}
