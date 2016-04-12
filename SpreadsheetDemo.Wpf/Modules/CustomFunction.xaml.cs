using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Spreadsheet;
using DevExpress.Spreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class CustomFunction : SpreadsheetDemoModule {
        public CustomFunction() {
            InitializeComponent();
            IWorkbook book = spreadsheetControl1.Document;
            GenerateDocument(book);
            spreadsheetControl1.CellValueChanged += spreadsheetControl1_CellValueChanged;
            book.History.Clear();
        }
        void GenerateDocument(IWorkbook book) {
            spreadsheetControl1.BeginUpdate();
            try {
                CustomFunctionDocumentGenerator generator = new CustomFunctionDocumentGenerator(book);
                generator.Generate();
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }

        private void spreadsheetControl1_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            Worksheet sheet = spreadsheetControl1.Document.Worksheets[0];
            if (e.SheetName == sheet.Name && e.RowIndex == 3 && e.ColumnIndex == 2) {
                sheet.Columns[2].AutoFit();
                sheet.Columns[4].AutoFit();
            }
        }
    }
}
