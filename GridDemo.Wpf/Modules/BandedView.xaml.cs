using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Data.Mask;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.DemoData.Models;
using System.Data.Entity;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.XtraExport.Helpers;

namespace GridDemo {
    public partial class BandedView : GridDemoModule {
        public BandedView() {
            InitializeComponent();
            grid.ItemsSource = new CarsContext().Cars.ToList();
        }

        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
