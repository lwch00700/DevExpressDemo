using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Controls;
using DevExpress.Utils;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class SingleTotal : PivotGridDemoModule {
        public SingleTotal() {
            InitializeComponent();
            pivotGrid.BeginUpdate();
            pivotGrid.AllowCrossGroupVariation = false;
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            fieldYear.FilterValues.FilterType = FieldFilterType.Included;
            object[] values = fieldYear.GetUniqueValues();
            fieldYear.FilterValues.Add(values[0]);
            fieldYear.FilterValues.Add(values[1]);
            fieldCategory.FilterValues.FilterType = FieldFilterType.Included;
            fieldCategory.FilterValues.Add("Beverages");
            fieldCategory.FilterValues.Add("Condiments");
            pivotGrid.EndUpdate();
            InitComboBoxes();
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }
        void InitComboBoxes() {
            Array arr = EnumExtensions.GetValues(typeof(FieldSummaryType));
            foreach(FieldSummaryType type in arr)
                cbSummaryType.Items.Add(type);
            foreach(PivotGridField field in pivotGrid.Fields)
                if(field.Area == FieldArea.DataArea && field.Visible) {
                    cbField.Items.Add(field.Caption);
                }
            cbField.SelectedIndex = 0;
        }
        void cbField_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            foreach(PivotGridField field in pivotGrid.Fields)
                if(field.Caption == cbField.SelectedItem.ToString())
                    cbSummaryType.SelectedIndex = cbSummaryType.Items.IndexOf(field.SummaryType);
        }
        void cbSummaryType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            foreach(PivotGridField field in pivotGrid.Fields)
                if(field.Caption == cbField.SelectedItem.ToString())
                    field.SummaryType = (FieldSummaryType)cbSummaryType.SelectedItem;
        }

        void OnPivotGridCustomSummary(object sender, PivotCustomSummaryEventArgs e) {
            e.CustomValue = e.SummaryValue.Summary;
        }
    }

}
