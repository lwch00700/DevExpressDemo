using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Docs.Demos;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class ExpenseReport : SpreadsheetDemoModule {
        public ExpenseReport() {
            InitializeComponent();
            Initialize();
        }

        private void Initialize() {
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.BeginUpdate();
            IWorkbook book = spreadsheetControl1.Document;
            ExpenseReportDocumentGenerator generator = new ExpenseReportDocumentGenerator(book);
            book = generator.Generate();
            spreadsheetControl1.EndUpdate();
            spreadsheetControl1.Document.History.Clear();
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
