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
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class ContextMenu: PivotGridDemoModule {
        public ContextMenu() {
            InitializeComponent();
            pivotGrid.BeginUpdate();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            pivotGrid.EndUpdate();
        }
        void pivotGrid_Loaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }

        void OnPivotGridCustomSummary(object sender, PivotCustomSummaryEventArgs e) {
            e.CustomValue = e.SummaryValue.Summary;
        }

        void OnPivotGridShowMenu(object sender, PopupMenuShowingEventArgs e) {
            if(e.MenuType.Equals(PivotGridMenuType.Header) && e.GetFieldInfo().Field.Area == FieldArea.DataArea) {
                e.Customizations.Add(new BarItemLinkSeparator());
                BarSubItem item = new BarSubItem() { Content = "Summary Type" };
                e.Customizations.Add(item);
                Array arr = EnumExtensions.GetValues(typeof(FieldSummaryType));
                foreach(FieldSummaryType type in arr)
                  item.ItemLinks.Add(new BarButtonItem() {
                        Name = "item" + type.ToString(),
                        Content = type.ToString(),
                        CommandParameter = new FieldSummaryItem() { Type = type, Field = e.GetFieldInfo().Field },
                        Command = new DelegateCommand<FieldSummaryItem>(SetFieldSummaryType, CanSetFieldSummaryType)
                    });
            }
        }

        void SetFieldSummaryType(FieldSummaryItem item) {
            item.Field.SummaryType = item.Type;
            item.Field.CellFormat = GetFormat(item.Type, item.Field);
            pivotGrid.BestFit(FieldArea.ColumnArea, true, false);
        }

        string GetFormat(FieldSummaryType fieldSummaryType, PivotGridField field) {
            if(field == fieldQuantity)
                return "";
            switch(fieldSummaryType){
                case FieldSummaryType.Average:
                case FieldSummaryType.Sum:
                case FieldSummaryType.Custom:
                case FieldSummaryType.Max:
                case FieldSummaryType.Min:
                    return "c";
                case FieldSummaryType.Count:
                    return "";
                case FieldSummaryType.StdDev:
                case FieldSummaryType.StdDevp:
                case FieldSummaryType.Var:
                case FieldSummaryType.Varp:
                    return "p";
            }
            return string.Empty;
        }

        bool CanSetFieldSummaryType(FieldSummaryItem item) { return !object.Equals(item.Field.SummaryType, item.Type); }
    }

    public class FieldSummaryItem {

        public FieldSummaryItem() { }

        public PivotGridField Field { get; set; }
        public FieldSummaryType Type { get; set; }
    }
}
