using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Docs.Demos;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class ShiftSchedule : SpreadsheetDemoModule {
        public ShiftSchedule() {
            InitializeComponent();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.BeginUpdate();
            IWorkbook book = spreadsheetControl1.Document;
            ShiftScheduleGenerator generator = new ShiftScheduleGenerator(book);
            book = generator.Generate();
            spreadsheetControl1.EndUpdate();
            book.History.Clear();
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
