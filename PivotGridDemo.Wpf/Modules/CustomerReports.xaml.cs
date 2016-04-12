using System.Windows;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class CustomerReports : PivotGridDemoModule {
        const string allString = "(All)";

        public CustomerReports() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().CustomerReports.ToList();
            ResetValues();
        }
        private void ListBoxEdit_SelectionChanged(object sender, RoutedEventArgs e) {
            pivotGrid.BeginUpdate();
            switch(reportsList.SelectedIndex) {
                case 0:
                    ResetValues();
                    break;
                case 1:
                    SetYearFilter();
                    break;
                case 2:
                    SetTopProducts();
                    break;
                case 3:
                    SetTopCustomers();
                    break;
            }
            pivotGrid.EndUpdateAsync();
        }
        private void cbeYear_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateYearFilter();
        }
        private void cbeQuarter_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateQuarterFilter();
        }

        void SetTopCustomers() {
            ResetValues();
            fieldProductName.Area = FieldArea.FilterArea;
            fieldYear.Area = fieldQuarter.Area = FieldArea.FilterArea;
            fieldCompanyName.SortByField = fieldProductAmount;
            fieldCompanyName.SortOrder = FieldSortOrder.Descending;
            fieldCompanyName.TopValueCount = 10;
        }
        void SetTopProducts() {
            ResetValues();
            fieldYear.Area = fieldQuarter.Area = FieldArea.FilterArea;
            fieldProductName.SortByField = fieldProductAmount;
            fieldProductName.SortOrder = FieldSortOrder.Descending;
            fieldProductName.TopValueCount = 2;
        }
        void SetYearFilter() {
            ResetValues();
            if(cbeYear.Items.Count == 0) {
                cbeQuarter.Items.Add(allString);
                foreach(object obj in fieldYear.GetUniqueValues()) {
                    cbeYear.Items.Add(obj);
                }
                cbeYear.SelectedItem = cbeYear.Items[0];
                foreach(object obj in fieldQuarter.GetUniqueValues()) {
                    cbeQuarter.Items.Add(obj);
                }
                cbeQuarter.SelectedItem = cbeQuarter.Items[0];
                cbeYear.SelectedItem = cbeYear.Items[0];
            }
            gbxFiltering.Visibility = System.Windows.Visibility.Visible;
            UpdateYearFilter();
            UpdateQuarterFilter();
        }
        void ResetValues() {
            if (gbxFiltering != null)
            gbxFiltering.Visibility = System.Windows.Visibility.Collapsed;
            if(fieldQuarter != null) {
                fieldQuarter.FilterValues.Clear();
                fieldQuarter.FilterValues.FilterType = FieldFilterType.Excluded;
                fieldQuarter.Area = FieldArea.ColumnArea;
            }
            if(fieldYear != null) {
                fieldYear.FilterValues.Clear();
                fieldYear.FilterValues.FilterType = FieldFilterType.Excluded;
                fieldYear.Area = FieldArea.ColumnArea;
                fieldYear.AreaIndex = 0;
            }
            if(fieldProductName != null) {
                fieldProductName.SortOrder = FieldSortOrder.Ascending;
                fieldProductName.SortByField = null;
                fieldProductName.TopValueCount = 0;
                fieldProductName.Area = FieldArea.RowArea;
                fieldProductName.AreaIndex = 1;
            }
            if(fieldCompanyName != null) {
                fieldCompanyName.SortByField = null;
                fieldCompanyName.TopValueCount = 0;
                fieldCompanyName.SortOrder = FieldSortOrder.Ascending;
            }
        }
        void UpdateYearFilter() {
            fieldYear.FilterValues.FilterType = FieldFilterType.Included;
            fieldYear.FilterValues.Clear();
            fieldYear.FilterValues.Add(cbeYear.SelectedItem);
        }
        void UpdateQuarterFilter() {
            fieldQuarter.FilterValues.Clear();
            fieldQuarter.FilterValues.FilterType = FieldFilterType.Excluded;
            if(cbeQuarter.SelectedIndex == 0) return;
            fieldQuarter.FilterValues.FilterType = FieldFilterType.Included;
            fieldQuarter.FilterValues.Add(cbeQuarter.SelectedItem);
        }
    }
}
