using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Docs.Demos;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class SportResults : SpreadsheetDemoModule {
        public SportResults() {
            InitializeComponent();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.BeginUpdate();
            IWorkbook book = spreadsheetControl1.Document;
            SportResultsDocumentGenerator generator = new SportResultsDocumentGenerator(book);
            book = generator.Generate();
            spreadsheetControl1.EndUpdate();
            book.History.Clear();
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
