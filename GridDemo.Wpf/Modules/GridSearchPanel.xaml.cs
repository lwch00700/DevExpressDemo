﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Core;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("ModuleResources/SearchPanelViewModel.(cs)")]
    [CodeFile("ModuleResources/SearchPanelClasses.(cs)")]
    [CodeFile("ModuleResources/SearchPanelTemplates(.SL).xaml")]
    [CodeFile("Controls/Converters.(cs)")]
    public partial class GridSearchPanel : GridDemoModule {
        public GridSearchPanel() {
            InitializeComponent();
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
