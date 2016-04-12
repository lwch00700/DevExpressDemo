using System;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Collections;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.DemoBase;
using System.Data;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/GridCellMultiColumnLookupEditorResources(.SL).xaml")]
    [CodeFile("ModuleResources/GridCellMultiColumnLookupEditorClasses.(cs)")]
    public partial class GridCellMultiColumnLookupEditor : GridDemoModule {
        public GridCellMultiColumnLookupEditor() {
            InitializeComponent();
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            ShowLookUp();
        }
        protected override void Clear() {
            view.CloseEditor();
            base.Clear();
        }
        void ShowLookUp() {
            grid.CurrentColumn = grid.Columns["CustomerID"];
            view.ShowEditor();
            Dispatcher.BeginInvoke(new Action(() => {
                LookUpEdit lookUpEdit = view.ActiveEditor as LookUpEdit;
                if(lookUpEdit != null)
                    lookUpEdit.ShowPopup();
            }));
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
