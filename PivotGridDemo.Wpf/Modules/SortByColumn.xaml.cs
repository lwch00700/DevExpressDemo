using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using System.Linq;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class SortByColumn : PivotGridDemoModule {
        public SortByColumn() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BeginUpdate();
            object value = fieldYear.GetUniqueValues()[0];
            fieldYear.FilterValues.ValuesIncluded = new object[] { value };
            if(fieldSalesPerson.SortByConditions.Count < 1) {
                fieldSalesPerson.SortByConditions.Add(new SortByCondition(fieldYear, value));
                fieldSalesPerson.SortByConditions.Add(new SortByCondition(fieldMonth, 8));
            }
            pivotGrid.EndUpdate();
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }
    }
}
