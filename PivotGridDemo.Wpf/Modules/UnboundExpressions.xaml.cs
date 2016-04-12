using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.PivotGrid.Internal;
using System.Windows.Data;
using System.Collections.Generic;
using System.Collections;
using DevExpress.Xpf.Bars;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class UnboundExpressions : PivotGridDemoModule {
        public UnboundExpressions() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            cbSalesPerson.SelectedIndex = 0;
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(true, false);
            NamesController = CreateNamesController();
            teBonusName.Text = NamesController.GetNextDefaultName();
        }
        NamesController NamesController { get; set; }
        NamesController CreateNamesController() {
            NamesController controller = new NamesController("NewBonus");
            foreach(PivotGridField field in pivotGrid.Fields) {
                controller.RegisterAvailableName(field.FieldName);
                controller.RegisterAvailableName(field.Name);
                controller.RegisterAvailableName(field.ExpressionFieldName);
                controller.RegisterAvailableName(field.UnboundFieldName);
            }
            return controller;
        }
        PivotGridField GetNewInvisibleBonusField() {
            PivotGridField newBonusField = new PivotGridField(teBonusName.Text, FieldArea.DataArea);
            newBonusField.ValueTemplate = (DataTemplate)Resources["UnboundFieldTemplate"];
            newBonusField.Name = "field_" + teBonusName.Text;
            newBonusField.Visible = false;
            newBonusField.UnboundType = FieldUnboundColumnType.Object;
            newBonusField.UnboundExpression = beExpression.Text;
            return newBonusField;
        }
        void beExpression_Click(object sender, System.Windows.RoutedEventArgs e) {
            PivotGridField newBonus = GetNewInvisibleBonusField();
            pivotGrid.Fields.Add(newBonus);
            pivotGrid.ShowUnboundExpressionEditor(newBonus);
            beExpression.Text = newBonus.UnboundExpression;
            pivotGrid.Fields.Remove(newBonus);
        }
        void btnAddField_Click(object sender, System.Windows.RoutedEventArgs e) {
            PivotGridField newBonus = GetNewInvisibleBonusField();
            newBonus.Visible = true;
            pivotGrid.Fields.Add(newBonus);
            NamesController.RegisterAvailableName(newBonus.FieldName);
            teBonusName.Text = NamesController.GetNextDefaultName();
            beExpression.Text = string.Empty;
        }
        void teBonusName_EditValueChanging(object sender, DevExpress.Xpf.Editors.EditValueChangingEventArgs e) {
            btnAddField.IsEnabled = !string.IsNullOrEmpty(e.NewValue as string);
        }
        void pivotGrid_FieldUnboundExpressionChanged(object sender, PivotFieldEventArgs e) {
            if(e.Field != null && !e.Field.Visible && btnAddField.IsEnabled)
                beExpression.Text = e.Field.UnboundExpression;
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            if(sender is Control) {
                FieldValueItem fieldValueItem = ((sender as Control).DataContext) as FieldValueItem;
                if(fieldValueItem != null && fieldValueItem.Field != null)
                    pivotGrid.ShowUnboundExpressionEditor(fieldValueItem.Field);
            }
        }
        private void removeBonus_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            if(sender is BarButtonItem) {
                FieldValueItem fieldValueItem = ((sender as BarButtonItem).Tag) as FieldValueItem;
                if(fieldValueItem != null && fieldValueItem.Field != null) {
                    NamesController.UnRegisterAvailableName(fieldValueItem.Field.FieldName);
                    pivotGrid.Fields.Remove(fieldValueItem.Field);
                }
            }
        }
    }

    public class FieldValueItemToBooleanConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            FieldValueItem item = (FieldValueItem)value;
            return item.Field != null && item.Field.Area == FieldArea.DataArea && item.Field.UnboundType != FieldUnboundColumnType.Bound;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class NamesController {
        readonly string DefaultName;
        public NamesController(string defaultName) {
            DefaultName = defaultName;
            AvailableNames = new List<string>();
        }
        List<string> AvailableNames { get; set; }

        public string GetNextDefaultName() {
            int i = 0;
            string name;
            do {
                name = DefaultName + i;
                i++;
            } while(AvailableNames.Contains(name));
            return name;
        }

        public void RegisterAvailableName(string name) {
            AvailableNames.Add(name);
        }

        public void UnRegisterAvailableName(string name) {
            AvailableNames.RemoveAll(delegate(string availableName) {
                return name == availableName;
            });
        }
    }
}
