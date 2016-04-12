using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class SortBySummary : PivotGridDemoModule {
        public SortBySummary() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BeginUpdate();
            fieldYear.FilterValues.FilterType = FieldFilterType.Included;
            fieldYear.FilterValues.Add(fieldYear.GetUniqueValues()[0]);
            pivotGrid.EndUpdate();
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
            InitComboBoxes();
        }
        void InitComboBoxes() {
            foreach(PivotGridField field in pivotGrid.Fields)
                if(field.Area == FieldArea.DataArea) {
                    cbField.Items.Add(new ComboBoxEditItem() { Content = field.Caption.ToString(), Tag = field });
                    if(object.ReferenceEquals(field, fieldSalesPerson.SortByField))
                        cbField.SelectedItem = cbField.Items[cbField.Items.Count - 1];
                }
            cbField.SelectedIndex = 0;
        }
        void cbField_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(cbField.SelectedIndex < 0)
                return;
            fieldSalesPerson.SortByField = (PivotGridField)((ComboBoxEditItem)cbField.SelectedItem).Tag;
        }
    }
}
