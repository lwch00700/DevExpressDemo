using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.Utils;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class IntervalGrouping : PivotGridDemoModule {
        public IntervalGrouping() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            InitComboBoxes();
            ceProductInterval.IsChecked = true;
        }
        void InitComboBoxes() {
            Array arr = EnumExtensions.GetValues(typeof(FieldGroupInterval));
            foreach(FieldGroupInterval interval in arr)
                if(interval.ToString().IndexOf("Date") == 0)
                    cbGroupInterval.Items.Add(new ComboBoxEditItem() { Content = interval.ToString(), Tag = interval });
            cbGroupInterval.SelectedIndex = cbGroupInterval.Items.Count - 1;
        }
        void pivotGrid_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e) {
            if(object.ReferenceEquals(e.Field, fieldOrderDate) && e.Field.GroupInterval == FieldGroupInterval.DateQuarter) {
                e.DisplayText = string.Format("Qtr {0}", e.Value);
                if(e.ValueType == FieldValueType.Total) e.DisplayText += " Total";
            }
        }
        void cbGroupInterval_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(cbGroupInterval.SelectedIndex < 0)
                return;
            fieldOrderDate.FilterValues.Clear();
            fieldOrderDate.GroupInterval = (FieldGroupInterval)((ComboBoxEditItem)cbGroupInterval.SelectedItem).Tag;
            fieldOrderDate.Caption = string.Format("Order Date ({0})", fieldOrderDate.GroupInterval.ToString().Replace("Date", ""));
        }
    }
}
