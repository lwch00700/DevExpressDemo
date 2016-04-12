using System;
using System.Collections.Generic;
using System.Windows;
using GridDemo;
using System.Collections;
using System.Globalization;
using DevExpress.Data;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.DemoBase.Helpers;
using System.Data;
using System.ComponentModel;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    [CodeFile("Controls/SalesByYearData.(cs)")]
    [CodeFile("ModuleResources/AlignGroupSummariesByColumnsViewModel.(cs)")]
    public partial class AlignGroupSummariesByColumns : GridDemoModule {
        AlignGroupSummariesByColumnsViewModel ViewModel { get { return (AlignGroupSummariesByColumnsViewModel)DataContext; } }
        public AlignGroupSummariesByColumns() {
            InitializeComponent();
            if(grid.ItemsSource != null)
                PopulateColumnsAndSummaries();
            grid.ExpandGroupRow(-1);
            grid.ItemsSourceChanged += new ItemsSourceChangedEventHandler(grid_ItemsSourceChanged);
        }

        void PopulateColumnsAndSummaries() {
            PropertyDescriptorCollection properties = ((ITypedList)grid.ItemsSource).GetItemProperties(null);
            foreach(PropertyDescriptor property in properties) {
                if(property.Name.Contains("Date")) continue;
                grid.Columns.Add(new GridColumn() {
                    FieldName = property.Name,
                    EditSettings = new SpinEditSettings() {
                        MaskType = MaskType.Numeric, Mask = "c", MaskCulture = new CultureInfo("en-US"), MaskUseAsDisplayFormat = true,
                        DisplayFormat="${0:N}"
                    }
                });
                grid.TotalSummary.Add(new GridSummaryItem() { FieldName = property.Name, SummaryType = SummaryItemType.Sum, DisplayFormat = "${0:N}" });
                grid.GroupSummary.Add(new GridSummaryItem() { FieldName = property.Name, SummaryType = SummaryItemType.Sum, DisplayFormat = "${0:N}" });
            }
        }
        private void grid_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e) {
            if(grid.GroupSummary.Count == 0)
                PopulateColumnsAndSummaries();
            bool byMonthReport = ViewModel.ReportTypeIndex == 1;
            grid.Columns["DateMonth"].Visible = byMonthReport;
            if(byMonthReport)
                grid.Columns["DateMonth"].GroupIndex = 1;
            else
                grid.UngroupBy("DateMonth");
            grid.ExpandGroupRow(-1);
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
