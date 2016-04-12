using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class TopValues : PivotGridDemoModule {
        static TopValues() {
            Type ownerType = typeof(TopValues);
        }
        public TopValues() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            InitComboBoxes();
            cbField.SelectedIndex = cbField.Items.Count - 1;
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(true, false);
        }
        void InitComboBoxes() {
            foreach(PivotGridField field in pivotGrid.Fields) {
                if(field.Area == FieldArea.RowArea || field.Area == FieldArea.ColumnArea)
                    cbField.Items.Add(field.Caption);
            }
        }
        void checkTopValueShowOthers_Checked(object sender, RoutedEventArgs e) {
            SetFieldTop();
        }
        void checkTopValueShowOthers_Unchecked(object sender, RoutedEventArgs e) {
            SetFieldTop();
        }
        void cbField_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            SetFieldTop();
            HideRowAreaFields();
        }
        void seTopValuesCount_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            SetFieldTop();
            pivotGrid.BestFit(true, false);
        }
        void SetFieldTop() {
            if(pivotGrid == null) return;
            pivotGrid.BeginUpdate();
            foreach(PivotGridField field in pivotGrid.Fields) {
                if(field.Caption == cbField.SelectedItem.ToString()) {
                    field.Visible = true;
                    field.SortOrder = FieldSortOrder.Descending;
                    field.SortByField = fieldExtendedPrice;
                    field.TopValueCount = Convert.ToInt32(seTopValuesCount.Value);
                    field.TopValueShowOthers = checkTopValueShowOthers.IsChecked.Value;
                } else {
                    field.TopValueCount = 0;
                }
            }
            pivotGrid.EndUpdate();
        }

        void HideRowAreaFields() {
            if(pivotGrid == null) return;
            pivotGrid.BeginUpdate();
            foreach(PivotGridField field in pivotGrid.Fields) {
                if(field.Caption == cbField.SelectedItem.ToString()) {
                } else {
                    if(field.Area == FieldArea.ColumnArea || field.Area == FieldArea.RowArea)
                        field.Visible = false;
                }
            }
            pivotGrid.EndUpdate();
        }
    }
}
