using System;
using System.Globalization;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class ChartingModule : SpreadsheetDemoModule {
        public ChartingModule() {
            InitializeComponent();
            InitializeWorkbook();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            ribbonControl1.SelectedPage = pageInsert;
        }
        void InitializeWorkbook() {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(DemoUtils.GetRelativePath("BreakevenAnalysis.xlsx"));
        }
    }
}
