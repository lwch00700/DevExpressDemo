using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.Generic;
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
    [CodeFile("IssueListContext.(cs)")]
    [CodeFile("Controls/Converters.(cs)")]
    [CodeFile("ModuleResources/CellMergingTemplates.xaml")]
    public partial class CellMerging : GridDemoModule {
        public CellMerging() {
            InitializeComponent();
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
