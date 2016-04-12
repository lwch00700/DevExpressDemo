using System;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Spreadsheet;
using System.Windows;

namespace SpreadsheetDemo {
    public partial class PivotTable : SpreadsheetDemoModule {
        public PivotTable() {
            InitializeComponent();

            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("PivotTableDemoTemplate.xlsx"), DocumentFormat.Xlsx);

            ApplyOptions();
            ribbonControl1.SelectedPage = pageHome;

            this.Loaded += PivotTable_Loaded;
            this.Unloaded += PivotTable_Unloaded;
        }

        void ApplyOptions() {
            SpreadsheetPivotTableFieldListOptions options = spreadsheetControl1.Options.PivotTableFieldList;
            options.StartPosition = SpreadsheetPivotTableFieldListStartPosition.ManualSpreadsheetControl;
            options.StartLocation = new Point(811, -138);
            options.StartSize = new Size(398, 608);
        }

        void PivotTable_Unloaded(object sender, RoutedEventArgs e) {
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet["A1"];
        }
        void PivotTable_Loaded(object sender, RoutedEventArgs e) {
            spreadsheetControl1.Selection = spreadsheetControl1.ActiveWorksheet["C3"];
        }
    }
}
