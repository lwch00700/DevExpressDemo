﻿using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using DevExpress.Data;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Bars;
using System.Collections;
using System.Data;
using DevExpress.DemoData.Models;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("Controls/MiltiSelectionOptionsControl.xaml.(cs)")]
    [CodeFile("Controls/MiltiSelectionOptionsControl(.SL).xaml")]
    [CodeFile("ModuleResources/MultiSelectionClasses.(cs)")]
    [CodeFile("ModuleResources/MultiSelectionTemplates(.SL).xaml")]
    public partial class MultiRowSelection : GridDemoModule {
        GridViewBase View { get { return (GridViewBase)grid.View; } }
        public MultiRowSelection() {
            InitializeComponent();
            viewsListBox.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(viewsListBox_SelectionChanged);
            View.ShowGridMenu += new GridMenuEventHandler(View_ShowGridMenu);
            FillComboBoxes();
            ProductsMultiSelectionOptionsControl.SelectButtonClick += new EventHandler(SelectProductsButtonClick);
            ProductsMultiSelectionOptionsControl.UnselectButtonClick += new EventHandler(UnselectProductsButtonClick);
            ProductsMultiSelectionOptionsControl.ReselectButtonClick += new EventHandler(ReselectProductsButtonClick);
            PriceMultiSelectionOptionsControl.SelectButtonClick += new EventHandler(SelectPriceButtonClick);
            PriceMultiSelectionOptionsControl.UnselectButtonClick += new EventHandler(UnselectPriceButtonClick);
            PriceMultiSelectionOptionsControl.ReselectButtonClick += new EventHandler(ReselectPriceButtonClick);
        }

        void View_ShowGridMenu(object sender, GridMenuEventArgs e) {
            if(e.MenuType == GridMenuType.Column) {
                e.Customizations.Add(new RemoveBarItemAndLinkAction() { ItemName = DefaultColumnMenuItemNames.SortBySummary });
            }
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            grid.View = (GridViewBase)FindResource(viewsListBox.SelectedIndex == 0 ? "tableView" : "cardView");
        }

        List<Invoice> dataTableFromGrid = null;
        List<Invoice> DataTableFromGrid {
            get {
                if(dataTableFromGrid == null) {
                    dataTableFromGrid = (List<Invoice>)grid.ItemsSource;
                }
                return dataTableFromGrid;
            }
        }
        private void FillComboBoxes() {
            List<Range> listRanges = new List<Range>();
            const int lastRangeMinLimit = 240;
            const int rangeInList = 30;
            for(int i = 0; i <= lastRangeMinLimit; i += rangeInList) {
                listRanges.Add(new Range() { Text = ("$" + Convert.ToString(i) + " - $" + Convert.ToString(i + rangeInList)), Min = i, Max = (i + rangeInList) });
            }
            PriceMultiSelectionOptionsControl.ComboBox.ItemsSource = listRanges;
            PriceMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0;
            ProductsMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0;
        }
        void RunAction(RowFilter FilterDelegate, Action actionForRows) {
            try {
                grid.BeginSelection();
                IEnumerable<Invoice> rows = DataTableFromGrid;
                foreach(var row in rows) {
                    if(FilterDelegate(row))
                        actionForRows(grid.GetRowHandleByListIndex(DataTableFromGrid.IndexOf(row)));
                }
            } finally {
                grid.EndSelection();
            }
        }
        delegate bool RowFilter(Invoice row);
        bool SelectProductsFilter(Invoice row) {
            return object.Equals(row.ProductID, (long)ProductsMultiSelectionOptionsControl.ComboBox.EditValue);
        }
        bool SelectRangeFilter(Invoice row) {
            Range range = (Range)PriceMultiSelectionOptionsControl.ComboBox.SelectedItem;
            try {
                int unitPrice = Convert.ToInt32(row.UnitPrice);
                return unitPrice >= range.Min && unitPrice <= range.Max;
            }
            catch(OverflowException) {
                return false;
            }
        }
        delegate void Action(int rowHandle);
        void SelectAction(int rowHandle) {
            grid.SelectItem(rowHandle);
        }
        void UnselectAction(int rowHandle) {
            grid.UnselectItem(rowHandle);
        }
        private void SelectProductsButtonClick(object sender, EventArgs e) {
            RunAction(SelectProductsFilter, SelectAction);
        }
        private void UnselectProductsButtonClick(object sender, EventArgs e) {
            RunAction(SelectProductsFilter, UnselectAction);
        }
        private void ReselectProductsButtonClick(object sender, EventArgs e) {
            grid.UnselectAll();
            RunAction(SelectProductsFilter, SelectAction);
        }
        private void SelectPriceButtonClick(object sender, EventArgs e) {
            RunAction(SelectRangeFilter, SelectAction);
        }
        private void UnselectPriceButtonClick(object sender, EventArgs e) {
            RunAction(SelectRangeFilter, UnselectAction);
        }
        private void ReselectPriceButtonClick(object sender, EventArgs e) {
            grid.UnselectAll();
            RunAction(SelectRangeFilter, SelectAction);
        }
        decimal sum = 0;
        private void grid_CustomSummary(object sender, DevExpress.Data.CustomSummaryEventArgs e) {
            if(object.Equals(e.SummaryProcess, CustomSummaryProcess.Start)) {
                sum = 0;
            }
            if(e.SummaryProcess == CustomSummaryProcess.Calculate) {
                if((grid != null) && (grid.View != null)) {
                    if(grid.View.IsRowSelected(e.RowHandle)) {
                        sum += (decimal)e.FieldValue;
                    }
                }
            }
            if(e.SummaryProcess == CustomSummaryProcess.Finalize)
                e.TotalValue = sum;
        }
        void UpdateSummary() {
            if(grid != null) {
                grid.UpdateTotalSummary();
                grid.UpdateGroupSummary();
            }
        }
        private void gridView_SelectionChanged(object sender, GridSelectionChangedEventArgs e) {
            UpdateSummary();
        }
        void SetMultiSelectMode(bool enabled) {
            ProductsMultiSelectionOptionsControl.IsEnabled = enabled;
            PriceMultiSelectionOptionsControl.IsEnabled = enabled;
        }

        private void grid_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e) {
            if(grid.SelectionMode != MultiSelectMode.None)
                UpdateSummary();
        }

        private void selectionModeListBox_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            SetMultiSelectMode(grid.SelectionMode != MultiSelectMode.None);
        }
        protected override DataViewBase GetExportView() {
            return null;
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }

    }
}
