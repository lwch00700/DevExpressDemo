using DevExpress.Xpf.DemoBase;
using DevExpress.XtraPrinting;
using System.Windows;

namespace GridDemo {
    [CodeFile("ModuleResources/DataAwareExport/DataAwareExportViewModel.cs")]
    [CodeFile("ModuleResources/DataAwareExport/ProductSaleData.cs")]
    public partial class DataAwareExport : GridDemoModule {
        public DataAwareExport() {
            InitializeComponent();
            grid.ExpandGroupRow(-1);
            grid.ItemsSourceChanged += new DevExpress.Xpf.Grid.ItemsSourceChangedEventHandler(grid_ItemsSourceChanged);
        }

        void grid_ItemsSourceChanged(object sender, DevExpress.Xpf.Grid.ItemsSourceChangedEventArgs e) {
            grid.ExpandGroupRow(-1);
        }

        protected override object GetModuleDataContext() {
            return DataContext;
        }
        void ExportButtonClick(object sender, RoutedEventArgs e) {
            new DemoModuleExportHelper(view).ExportToXlsx();
        }
    }
}
