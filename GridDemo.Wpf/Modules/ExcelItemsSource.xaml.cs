using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GridDemo {
    public partial class ExcelItemsSource : PrintViewGridDemoModule {
        public ExcelItemsSource() {
            InitializeComponent();
            SetUpSpreadsheetControl();
        }

        protected override DevExpress.Xpf.Grid.DataViewBase GetExportView() { return view; }

        protected override DevExpress.Xpf.Core.DXTabControl DXTabControl { get { return tabControl; } }


        void SetUpSpreadsheetControl() {
            using(Stream stream = Application.GetResourceStream(new Uri(@"pack://application:,,,/GridDemo;component/Data/Orders.xls")).Stream) {
                spreadsheetControl.Document.LoadDocument(stream, DocumentFormat.Xls);
            }
        }
    }
}
