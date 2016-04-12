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

namespace SpreadsheetDemo {
    public partial class FirstLook : SpreadsheetDemoModule {
        public FirstLook() {
            InitializeComponent();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.Document.LoadDocument(DemoUtils.GetRelativePath("EmployeeInformation.xlsx"));
            ribbonControl1.SelectedPage = pageHome;
        }
        Worksheet ActiveSheet { get { return spreadsheetControl1.ActiveWorksheet; } }

        private void cbSelectedCellSelectedIndexChanged(object sender, RoutedEventArgs e) {
            string selectedCell = ((ComboBoxEdit)sender).SelectedItem.ToString();
            spreadsheetControl1.SelectedCell = ActiveSheet[selectedCell];
        }

        private void cbSelectionSelectedIndexChanged(object sender, RoutedEventArgs e) {
            string selection = ((ComboBoxEdit)sender).SelectedItem.ToString();
            spreadsheetControl1.Selection = ActiveSheet[selection];
        }
    }
}
