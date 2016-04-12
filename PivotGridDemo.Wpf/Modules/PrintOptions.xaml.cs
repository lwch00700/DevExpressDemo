using System.Windows;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Printing;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class PrintOptions : PivotGridDemoModule {

        public PrintOptions() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(true, false);
            ShowPrintPreview(pivotGrid);
        }
    }
}
