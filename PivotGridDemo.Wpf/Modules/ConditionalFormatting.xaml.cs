using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Markup;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows.Media;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class ConditionalFormatting : PivotGridDemoModule {
        public ConditionalFormatting() {
            InitializeComponent();
      fieldOrderYear.FilterValues.FilterType = FieldFilterType.Included;
            fieldOrderYear.FilterValues.Add(1996);
   fieldOrderYear.FilterValues.Add(1997);
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }

        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(fieldSalesPerson, true, false);
        }

    }
}
