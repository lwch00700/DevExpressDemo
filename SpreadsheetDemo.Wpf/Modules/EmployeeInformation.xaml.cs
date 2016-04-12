using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.Docs.Demos;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Demos;

namespace SpreadsheetDemo {
    public partial class EmployeeInformation : SpreadsheetDemoModule {
        public EmployeeInformation() {
            InitializeComponent();
            spreadsheetControl1.Options.Culture = DefaultCulture;
            EmployeeInformationDocumentGenerator generator = new EmployeeInformationDocumentGenerator(this.spreadsheetControl1.Document);
            generator.Generate(DemoUtils.GetRelativePath("EmployeeInformation_template.xlsx"));
            spreadsheetControl1.Document.History.Clear();
            ribbonControl1.SelectedPage = pageHome;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e) {
        }
    }
}
