using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using DevExpress.DemoData.Models;


namespace PivotGridDemo.PivotGrid {
    public partial class ProductReports : PivotGridDemoModule {
        FloatingContainer popupContainer;
        public ProductReports() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().ProductReports.ToList();
        }
        void ReportsList_SelectionChanged(object sender, RoutedEventArgs e) {
            if(fieldCategory == null) return;

            fieldCategory.Area = FieldArea.RowArea;
            fieldProduct.Area = FieldArea.FilterArea;
            fieldQuarter.Area = FieldArea.FilterArea;
            fieldYear.Area = FieldArea.FilterArea;
            fieldAverageSale.Visible = false;
            fieldMinimumSale.Visible = false;
            fieldProduct.SortByField = null;
            fieldProduct.SortOrder = FieldSortOrder.Ascending;
            fieldProduct.TopValueCount = 0;
            fieldMonth.Area = FieldArea.FilterArea;
            fieldMonth.Visible = false;
            fieldQuarter.Area = FieldArea.FilterArea;
            fieldCategory.TotalsVisibility = FieldTotalsVisibility.AutomaticTotals;
            fieldCategory.CustomTotals.Clear();
            cbxShowCategories.Visibility = System.Windows.Visibility.Collapsed;
            spGroupingLayout.Visibility = System.Windows.Visibility.Collapsed;

            switch(reportsList.SelectedIndex) {
                case 0:
                    fieldCategory.Area = FieldArea.RowArea;
                    fieldProduct.Area = FieldArea.FilterArea;
                    break;
                case 1:
                    fieldCategory.Area = FieldArea.FilterArea;
                    fieldProduct.Area = FieldArea.RowArea;
                    cbxShowCategories.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 2:
                    fieldMonth.Visible = true;
                    fieldCategory.Area = FieldArea.RowArea;
                    fieldProduct.Area = FieldArea.RowArea;
                    fieldQuarter.Area = FieldArea.ColumnArea;
                    fieldProduct.AreaIndex = 1;
                    spGroupingLayout.Visibility = System.Windows.Visibility.Visible;
                    ComboBoxEdit_SelectedIndexChanged(cbeGroupingLayout, e);
                    break;
                case 3:
                    fieldCategory.TotalsVisibility = FieldTotalsVisibility.CustomTotals;
                    fieldCategory.Area = FieldArea.RowArea;
                    fieldProduct.Area = FieldArea.RowArea;
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Average);
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Sum);
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Max);
                    fieldCategory.CustomTotals.Add(FieldSummaryType.Min);
                    fieldMonth.Visible = true;
                    fieldQuarter.Area = FieldArea.ColumnArea;
                    fieldYear.Area = FieldArea.ColumnArea;
                    fieldProduct.AreaIndex = 1;
                    break;
                case 4:
                    fieldQuarter.SetAreaPosition(FieldArea.RowArea, 0);
                    fieldCategory.Area = FieldArea.RowArea;
                    fieldAverageSale.SetAreaPosition(FieldArea.DataArea, 1);
                    fieldMinimumSale.SetAreaPosition(FieldArea.DataArea, 2);
                    fieldAverageSale.Visible = fieldMinimumSale.Visible = true;
                    fieldCategory.AreaIndex = 1;
                    break;
                case 5:
                    fieldProduct.Area = FieldArea.RowArea;
                    fieldCategory.Area = FieldArea.RowArea;
                    fieldProduct.SortByField = fieldSales;
                    fieldProduct.SortOrder = FieldSortOrder.Descending;
                    fieldProduct.TopValueCount = 3;
                    fieldProduct.AreaIndex = 1;
                    break;
            }
        }
        private void cbxShowCategories_Checked(object sender, RoutedEventArgs e) {
            fieldCategory.Area = cbxShowCategories.IsChecked.Value ? FieldArea.RowArea : FieldArea.FilterArea;
            fieldCategory.AreaIndex = 0;
        }
        private void ComboBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(fieldMonth == null) return;
            switch(((ComboBoxEdit)sender).SelectedIndex) {
                case 0:
                    fieldQuarter.Area = FieldArea.FilterArea;
                    fieldMonth.Area = FieldArea.FilterArea;
                    fieldYear.Area = FieldArea.ColumnArea;
                    break;
                case 1:
                    fieldYear.Area = FieldArea.FilterArea;
                    fieldMonth.Area = FieldArea.FilterArea;
                    fieldQuarter.Area = FieldArea.ColumnArea;
                    break;
                case 2:
                    fieldYear.Area = FieldArea.FilterArea;
                    fieldQuarter.Area = FieldArea.FilterArea;
                    fieldMonth.Area = FieldArea.ColumnArea;
                    break;
                case 3:
                    fieldYear.Area = FieldArea.ColumnArea;
                    fieldQuarter.Area = FieldArea.ColumnArea;
                    fieldMonth.Area = FieldArea.ColumnArea;
                    break;
            }
        }

        private void pivotGrid_CellDblClick(object sender, PivotCellEventArgs e) {
            if(!showDrillDown.IsChecked.Value) return;
            GridControl grid = new GridControl();
            ThemeManager.SetThemeName(grid, ThemeManager.ApplicationThemeName);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            grid.View = new TableView() { AllowPerPixelScrolling = true };
            grid.ItemsSource = ds;
            grid.PopulateColumns();
            grid.ShowBorder = false;
            popupContainer = FloatingContainer.ShowDialog(grid, this, new Size(520, 300),
                new FloatingContainerParameters() {
                    AllowSizing = true,
                    CloseOnEscape = true,
                    Title = "Drill Down Form",
                    ClosedDelegate = null
                });
            AddLogicalChild(popupContainer);
        }
    }
}
