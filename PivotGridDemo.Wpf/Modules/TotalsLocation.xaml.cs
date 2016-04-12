using System;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Utils;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class TotalsLocation : PivotGridDemoModule {
        static TotalsLocation() {
            Type ownerType = typeof(TotalsLocation);
        }
        public TotalsLocation() {
            InitializeComponent();
            InitComboBoxes();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void InitComboBoxes() {
            Array arr = EnumExtensions.GetValues(typeof(FieldColumnTotalsLocation));
            foreach(FieldColumnTotalsLocation type in arr)
                cbColumnTotalsLocation.Items.Add(type);
            arr = EnumExtensions.GetValues(typeof(FieldRowTotalsLocation));
            foreach(FieldRowTotalsLocation type in arr)
                cbRowTotalsLocation.Items.Add(type);
        }
    }
}
