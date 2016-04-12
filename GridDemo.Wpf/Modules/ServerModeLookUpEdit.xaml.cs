using System;
using DevExpress.Xpf.DemoBase;
using GridDemo.PeopleDataService;
using DevExpress.Data.WcfLinq;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/ServerModeLookUpEditViewModel.(cs)")]
    [CodeFile("ModuleResources/ServerModeLookUpEditResources.xaml")]
    public partial class ServerModeLookUpEdit : GridDemoModule {
        public ServerModeLookUpEdit() {
            InitializeComponent();
        }
        protected override void Clear() {
            view.CloseEditor();
            base.Clear();
        }

        protected override object GetModuleDataContext() {
            return DataContext;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
