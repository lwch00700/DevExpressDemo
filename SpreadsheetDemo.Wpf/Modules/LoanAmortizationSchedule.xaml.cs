using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Docs.Demos;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;
using DevExpress.Xpf.Core;

namespace SpreadsheetDemo {
    public partial class LoanAmortizationSchedule : SpreadsheetDemoModule {
        IWorkbook book;
        LoanAmortizationScheduleDocumentGenerator generator;

        public LoanAmortizationSchedule() {
            InitializeComponent();
            this.spreadsheetControl.Options.Culture = DefaultCulture;
            InitializeDocument();
            this.spreadsheetControl.Document.History.Clear();
        }

        bool ShouldUseAnnuityPayments { get { return rbAnnuityPayments.IsChecked.HasValue ? rbAnnuityPayments.IsChecked.Value : false; } }

        void InitializeDocument() {
            spreadsheetControl.BeginUpdate();
            book = spreadsheetControl.Document;
            generator = new LoanAmortizationScheduleDocumentGenerator(book);
            generator.AnnuityPayments = ShouldUseAnnuityPayments;
            generator.Generate();
            spreadsheetControl.EndUpdate();
        }


        void RadioButton_Checked(object sender, System.Windows.RoutedEventArgs e) {
            generator.AnnuityPayments = ShouldUseAnnuityPayments;
            spreadsheetControl.BeginUpdate();
            generator.Generate();
            spreadsheetControl.EndUpdate();
        }

        void spreadsheetControl_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e) {
            if ((e.ColumnIndex == 4) && ((e.RowIndex >= 3) && (e.RowIndex <= 8))) {
                spreadsheetControl.BeginUpdate();
                generator.Generate();
                spreadsheetControl.EndUpdate();
            }
        }

        void spreadsheetControl_CellEndEdit(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellValidatingEventArgs e) {
            if (e.ColumnIndex == 4 && e.RowIndex == 5) {
                int value;
                if (!int.TryParse(e.EditorText, out value) || value < 1 || value > 10) {
                    DXMessageBox.Show("Please enter a whole number of years from 1 to 10");
                    e.Cancel = true;
                }
            }
        }
    }
}
