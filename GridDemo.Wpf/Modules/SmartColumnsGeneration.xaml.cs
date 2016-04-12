using System;
using System.Windows.Data;
using DevExpress.Xpf.Grid;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/SmartColumnsGenerationViewModel.(cs)")]
    public partial class SmartColumnsGeneration : GridDemoModule {
        public SmartColumnsGeneration() {
            InitializeComponent();
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
