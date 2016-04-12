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
    public partial class SummaryDisplayMode : PivotGridDemoModule {
        static SummaryDisplayMode() {
            Type ownerType = typeof(SummaryDisplayMode);
        }
        public SummaryDisplayMode() {
            InitializeComponent();
            InitComboBox();
            pivotGrid.AllowCrossGroupVariation = false;
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void InitComboBox() {
            Array arr = EnumExtensions.GetValues(typeof(FieldSummaryDisplayType));
            foreach(FieldSummaryDisplayType type in arr)
                cbSummaryDisplayType.Items.Add(type.ToString());
            cbSummaryDisplayType.SelectedIndex = 1;
        }
        private void checkAllowCrossGroupVariation_Checked(object sender, RoutedEventArgs e) {
            pivotGrid.AllowCrossGroupVariation = true;
        }
        private void checkAllowCrossGroupVariation_Unchecked(object sender, RoutedEventArgs e) {
            pivotGrid.AllowCrossGroupVariation = false;
        }
        private void checkHideEmptyVariationItems_Checked(object sender, RoutedEventArgs e) {
            fieldQuantity1.HideEmptyVariationItems = true;
        }
        private void checkHideEmptyVariationItems_Unchecked(object sender, RoutedEventArgs e) {
            fieldQuantity1.HideEmptyVariationItems = false;
        }
        private void cbSummaryDisplayType_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            string value = cbSummaryDisplayType.SelectedItem.ToString();
            fieldQuantity1.SummaryDisplayType = (FieldSummaryDisplayType)Enum.Parse(typeof(FieldSummaryDisplayType), value, false);
            fieldQuantity1.Caption = FieldVariationCaption(fieldQuantity1.SummaryDisplayType);
        }

        void OnCustomAppearance(object sender, PivotCustomCellAppearanceEventArgs e) {
            if(!(e.Value is decimal)) return;
            if(e.DataField.Equals(fieldQuantity1)) {
                if((decimal)e.Value >= 0)
                    e.Foreground = new SolidColorBrush(Colors.Blue);
                else
                    e.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
        string FieldVariationCaption(FieldSummaryDisplayType displayType) {
            switch(displayType){
                case FieldSummaryDisplayType.AbsoluteVariation:
                    return "Qty (Var)";
                case FieldSummaryDisplayType.PercentVariation:
                case FieldSummaryDisplayType.PercentOfColumn:
                case FieldSummaryDisplayType.PercentOfColumnGrandTotal:
                case FieldSummaryDisplayType.PercentOfGrandTotal:
                case FieldSummaryDisplayType.PercentOfRow:
                case FieldSummaryDisplayType.PercentOfRowGrandTotal:
                    return "Qty (%)";
                case FieldSummaryDisplayType.RankInColumnLargestToSmallest:
                case FieldSummaryDisplayType.RankInColumnSmallestToLargest:
                case FieldSummaryDisplayType.RankInRowLargestToSmallest:
                case FieldSummaryDisplayType.RankInRowSmallestToLargest:
                    return "Qty (Rank)";
                case FieldSummaryDisplayType.Index:
                    return "Qty (Index)";
                case FieldSummaryDisplayType.Default:
                default:
                    return "Qty";
            }
        }
    }
}
