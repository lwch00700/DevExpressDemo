using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class StatisticFunctions : SpreadsheetDemoModule {
        public StatisticFunctions() {
            InitializeComponent();
            spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.LoadDocument(DemoUtils.GetRelativePath("TrendlineAnalysis_template.xlsx"), DocumentFormat.Xlsx);
            ribbonControl1.SelectedPage = pageFormulas;
        }
    }
}
