using System;
using System.Globalization;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class UnitConverter : SpreadsheetDemoModule {
        public UnitConverter() {
            InitializeComponent();
            InitializeWorkbook();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
        }
        void InitializeWorkbook() {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(DemoUtils.GetRelativePath("UnitConverter_template.xlsx"));
        }
    }
}
