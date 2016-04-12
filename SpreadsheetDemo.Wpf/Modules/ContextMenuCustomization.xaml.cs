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
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Spreadsheet;

namespace SpreadsheetDemo {
    public partial class ContextMenuCustomization : SpreadsheetDemoModule {
        public ContextMenuCustomization() {
            InitializeComponent();
            this.spreadsheetControl1.Options.Culture = DefaultCulture;
            spreadsheetControl1.Document.LoadDocument(DemoUtils.GetRelativePath("EmployeeInformation.xlsx"));
        }

        private void SpreadsheetControlPopupMenuShowing(object sender, DevExpress.Xpf.Spreadsheet.Menu.PopupMenuShowingEventArgs e) {
            if (e.MenuType.Equals(DevExpress.Xpf.Spreadsheet.SpreadsheetMenuType.Cell)) {
                if (ceInsertPicture.IsChecked == true)
                    e.Customizations.Add(new BarButtonItem() { Command = SpreadsheetUICommand.InsertPicture, Content = "Insert picture", CommandParameter=spreadsheetControl1});
                if (ceZoomIn.IsChecked.Value == true)
                    e.Customizations.Add(new BarButtonItem() { Command = SpreadsheetUICommand.ViewZoomIn, Content = "Zoom In", CommandParameter = spreadsheetControl1 });
                if (ceZoomOut.IsChecked.Value == true)
                    e.Customizations.Add(new BarButtonItem() { Command = SpreadsheetUICommand.ViewZoomOut, Content = "Zoom Out", CommandParameter = spreadsheetControl1 });
            }

        }
    }
}
