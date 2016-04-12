using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class MultipleTotals : PivotGridDemoModule {

        public MultipleTotals() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            fieldYear.FilterValues.FilterType = FieldFilterType.Included;
            fieldYear.FilterValues.Add(fieldYear.GetUniqueValues()[0]);
        }
    }
}
