using System;
using System.Globalization;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class WorksheetProtection : SpreadsheetDemoModule {
        public WorksheetProtection() {
            InitializeComponent();
            InitializeWorkbook();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            this.ribbonControl1.SelectedPage = pageReview;
        }
        void InitializeWorkbook() {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(DemoUtils.GetRelativePath("SimpleMonthlyBudget.xlsx"));
        }
    }
}
