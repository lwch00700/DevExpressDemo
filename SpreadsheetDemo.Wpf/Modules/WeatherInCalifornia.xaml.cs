using System;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraSpreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class WeatherInCalifornia : SpreadsheetDemoModule {
        public WeatherInCalifornia() {
            InitializeComponent();
            InitializeWorkbook();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            UpdateConditionalFormatting();
            ribbonControl1.SelectedPage = pageHome;
        }

        void InitializeWorkbook() {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(DemoUtils.GetRelativePath("WeatherInCalifornia.xlsx"));
        }

        void OnCheckedChanged(object sender, System.Windows.RoutedEventArgs e) {
            UpdateConditionalFormatting();
        }
        void UpdateConditionalFormatting() {
            if (spreadsheetControl1 == null)
                return;

            spreadsheetControl1.BeginUpdate();
            try {
                Worksheet sheet = spreadsheetControl1.ActiveWorksheet;
                sheet.ConditionalFormattings.Clear();
                if (chkTemperature.IsChecked == true)
                    DevExpress.XtraSpreadsheet.Demos.WeatherInCalifornia.ApplyTemperatureConditionalFormatting(sheet);
                if (chkHumidity.IsChecked == true)
                    DevExpress.XtraSpreadsheet.Demos.WeatherInCalifornia.ApplyHumidityConditionalFormatting(sheet);
                if (chkPressure.IsChecked == true)
                    DevExpress.XtraSpreadsheet.Demos.WeatherInCalifornia.ApplyPressureConditionalFormatting(sheet);
            }
            finally {
                spreadsheetControl1.EndUpdate();
            }
        }

    }
}
