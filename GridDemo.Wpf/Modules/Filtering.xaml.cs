using System;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/FilteringTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/FilteringClasses.(cs)")]
    public partial class Filtering : GridDemoModule {
        public Filtering() {
            InitializeComponent();

            grid.FilterCriteria = new OperandProperty("City") == "Bergamo";
            grid.FilterCriteria = new OperandProperty("UnboundOrderDate") >= new DateTime(DateTime.Today.Year, 1, 1);

            showFilterPanelModeListBox.EditValueChanged += new EditValueChangedEventHandler(OnShowFilterPanelModeChanged);
            viewsListBox.EditValueChanged += new EditValueChangedEventHandler(OnViewChanged);
        }
        void OnViewChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(viewsListBox.SelectedIndex == 0) {
                grid.View = (GridViewBase)FindResource("columnView");
            }
            if(viewsListBox.SelectedIndex == 1) {
                grid.View = (GridViewBase)FindResource("cardView");
            }
            UpdateShowFilterPanelMode();
        }
        void OnShowFilterPanelModeChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            UpdateShowFilterPanelMode();
        }
        void UpdateShowFilterPanelMode() {
            grid.View.ShowFilterPanelMode = GetMode(showFilterPanelModeListBox.SelectedIndex);
        }
        ShowFilterPanelMode GetMode(int index) {
            switch(index) {
                case 0:
                    return ShowFilterPanelMode.Default;
                case 1:
                    return ShowFilterPanelMode.ShowAlways;
                case 2:
                    return ShowFilterPanelMode.Never;
                default:
                    return ShowFilterPanelMode.Default;
            }
        }
        protected override DataViewBase GetExportView() {
            return null;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
