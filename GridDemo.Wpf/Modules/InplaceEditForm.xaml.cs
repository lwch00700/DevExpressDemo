using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Grid;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/InplaceEditFormResources.xaml")]
    public partial class InplaceEditForm : GridDemoModule {
        public InplaceEditForm() {
            InitializeComponent();
            grid.ItemsSource = new CarsContext().Cars.ToList();
        }

        void OnTemplateValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(tableView == null)
                return;
            int index = templatesListBox.SelectedIndex;
            if(index == 0)
                tableView.ClearValue(TableView.EditFormTemplateProperty);
            else if(index == 1)
                tableView.EditFormTemplate = (DataTemplate)Resources["CustomEditFormTemplate"];
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
