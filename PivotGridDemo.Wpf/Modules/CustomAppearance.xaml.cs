using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Markup;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows.Media;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class CustomAppearance : PivotGridDemoModule {
        public CustomAppearance() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            pivotGrid.BeginUpdate();
            fieldCategoryName.FilterValues.FilterType = FieldFilterType.Included;
            fieldCategoryName.FilterValues.Add("Grains/Cereals");
            fieldCategoryName.FilterValues.Add("Meat/Poultry");
            fieldCategoryName.FilterValues.Add("Produce");
            fieldSalesPerson.FilterValues.Add("Robert King");
            fieldSalesPerson.FilterValues.Add("Steven Buchanan");
            pivotGrid.EndUpdate();
            pivotGrid.FocusedCellChanged += new RoutedEventHandler(OnFocusedCellChanged);
            Loaded += new RoutedEventHandler(OnModuleLoaded);
        }

        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            pivotGrid.BestFit(fieldSalesPerson, true, false);
        }

        void OnFocusedCellChanged(object sender, RoutedEventArgs e) {
            pivotGrid.RaiseEvent(new PivotBrushChangedEventArgs(PivotGridControl.BrushChangedEvent, pivotGrid, PivotBrushType.CellBrush));
            pivotGrid.RaiseEvent(new PivotBrushChangedEventArgs(PivotGridControl.BrushChangedEvent, pivotGrid, PivotBrushType.ValueBrush));
        }

        void OnCustomCellAppearance(object sender, PivotCustomCellAppearanceEventArgs e) {
            bool columnSelected = e.RowIndex == pivotGrid.FocusedCell.Y;
            bool rowSelected = e.ColumnIndex == pivotGrid.FocusedCell.X;
            if(columnSelected && !rowSelected || !columnSelected && rowSelected) {
                CellMode mode = CellMode.Selected;
                if(e.RowValueType > 0 || e.ColumnValueType > 0)
                    mode = mode | CellMode.Tolal;
                e.Background = GetActualCellBackgroundBrush(mode);
                e.Foreground = GetActualCellForegroundBrush(mode);
            }
        }

        void OnCustomValueAppearance(object sender, PivotCustomValueAppearanceEventArgs e) {
            if(e.MaxIndex != e.MinIndex)
                return;
            if(e.IsColumn && e.MinIndex == pivotGrid.FocusedCell.X ||
                !e.IsColumn && e.MinIndex == pivotGrid.FocusedCell.Y)
                e.Background = pivotGrid.ValueSelectedBackground;
        }

        Brush GetActualCellForegroundBrush(CellMode actualMode) {
            switch(actualMode) {
                case CellMode.None:
                    return pivotGrid.CellForeground;
                case CellMode.Selected:
                    return pivotGrid.CellSelectedForeground;
                case CellMode.Tolal:
                    return pivotGrid.CellTotalForeground;
                case CellMode.TotalSelected:
                    return pivotGrid.CellTotalSelectedForeground;
                default:
                    throw new Exception("CellMode");
            }
        }

        Brush GetActualCellBackgroundBrush(CellMode actualMode) {
            switch(actualMode) {
                case CellMode.None:
                    return pivotGrid.CellBackground;
                case CellMode.Selected:
                    return pivotGrid.CellSelectedBackground;
                case CellMode.Tolal:
                    return pivotGrid.CellTotalBackground;
                case CellMode.TotalSelected:
                    return pivotGrid.CellTotalSelectedBackground;
                default:
                    throw new Exception("CellMode");
            }
        }

        public enum CellMode {
            None = 0,
            Tolal = 1,
            Selected = 2,
            TotalSelected = Tolal | Selected,
        }
    }
}
