using System.Windows;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class FilterPopup : PivotGridDemoModule {

        public FilterPopup() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }

        void OnPivotGridLoaded(object sender, RoutedEventArgs e) {
            SetGroupFilter();
            SetFilter();
        }

        void SetGroupFilter() {
            if(PivotGridGroup1 == null || !PivotGridGroup1.FilterValues.IsEmpty) return;
            PivotGridGroup1.FilterValues.BeginUpdate();
            PivotGridGroup1.FilterValues.FilterType = FieldFilterType.Included;
            PivotGridGroup1.FilterValues.Values.Add("Beverages");
            PivotGridGroup1.FilterValues.EndUpdate();
            if(PivotGridGroup2 == null || !PivotGridGroup2.FilterValues.IsEmpty) return;
            PivotGridGroup2.FilterValues.BeginUpdate();
            PivotGridGroup2.FilterValues.FilterType = FieldFilterType.Included;
            object[] values = fieldYear.GetUniqueValues();
            PivotGridGroup2.FilterValues.Values.Add(values[0]).ChildValues.Add(3);
            PivotGridGroup2.FilterValues.Values[values[0]].ChildValues[3].ChildValues.Add(9);
            PivotGridGroup2.FilterValues.Values.Add(values[1]).ChildValues.Add(1);
            PivotGridGroup2.FilterValues.Values[values[1]].ChildValues.Add(4);
            PivotGridGroup2.FilterValues.EndUpdate();
        }

        void SetFilter() {
            fieldCategory.FilterValues.FilterType = FieldFilterType.Included;
            fieldCategory.FilterValues.Add("Beverages");
        }
    }
}
