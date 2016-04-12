using System;
using System.Linq;
using System.Windows;
using System.Globalization;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class Prefilter : PivotGridDemoModule {
        public Prefilter() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            deFromDate.EditValue = new System.DateTime(1994, 8, 4, 11, 25, 54, 0);
            deToDate.EditValue = new System.DateTime(1996, 6, 5, 11, 26, 19, 0);
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            deFromDate.EditValueChanged += deFromTo_EditValueChanged;
            deToDate.EditValueChanged += deFromTo_EditValueChanged;
        }
        void deFromTo_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ApplyPrefilter();
        }
        void ApplyPrefilter() {
            string str1 = GetCriteria(deFromDate.DateTime, true),
                str2 = GetCriteria(deToDate.DateTime, false);
            if(!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
                pivotGrid.PrefilterCriteria = CriteriaOperator.Parse(str1 + " And " + str2);
            else
                pivotGrid.PrefilterCriteria = CriteriaOperator.Parse(str1 + str2);
        }
        string GetCriteria(DateTime date, bool isGreater) {
            if(date.Ticks == 0) return "";
            return string.Format("{0} {1} #{2}#", fieldOrderDate.Name, isGreater ? ">=" : "<=",
                Convert.ToString(date, CultureInfo.InvariantCulture));
        }
    }
}
