using System;
using System.Collections.Generic;
using GridDemo;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/ValidationViewModel.(cs)")]
    public partial class Validation : GridDemoModule {
        public Validation() {
            InitializeComponent();
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
