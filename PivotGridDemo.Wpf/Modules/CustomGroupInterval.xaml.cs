using System;
using System.Windows;
using System.Linq;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class CustomGroupInterval : PivotGridDemoModule {
        int selectedDemo;

        public CustomGroupInterval() {
            selectedDemo = 0;
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().ProductReports.ToList();
            cbGroup.Items.Add(new ComboBoxEditItem() { Content = "Group Products by First Characters", Tag = 0 });
            cbGroup.Items.Add(new ComboBoxEditItem() { Content = "Group Sales by Year and Quarter", Tag = 1 });
            cbGroup.SelectedIndex = 0;
        }

        int SelectedDemo { get { return selectedDemo; } }

        void cbGroup_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            object tag = ((ComboBoxEditItem)cbGroup.SelectedItem).Tag;
            selectedDemo = (int)tag;
            pivotGrid.BeginUpdate();
            switch(SelectedDemo) {
                case 0:
                    pivotGrid.Fields[0].Visible = true;
                    pivotGrid.Fields[0].Caption = "Product Group";
                    pivotGrid.Fields[0].GroupInterval = FieldGroupInterval.Custom;

                    pivotGrid.Fields[4].Caption = "Year";
                    pivotGrid.Fields[4].GroupInterval = FieldGroupInterval.DateYear;

                    pivotGrid.Fields[3].Visible = false;
                    pivotGrid.Fields[3].GroupInterval = FieldGroupInterval.Default;
                    break;
                case 1:
                    pivotGrid.Fields[3].Visible = true;
                    pivotGrid.Fields[3].Caption = "Year - Quarter";
                    pivotGrid.Fields[3].GroupInterval = FieldGroupInterval.Custom;
                    pivotGrid.Fields[3].AreaIndex = 0;

                    pivotGrid.Fields[4].Caption = "Shipped Date";
                    pivotGrid.Fields[4].GroupInterval = FieldGroupInterval.Date;
                    pivotGrid.Fields[4].AreaIndex = 1;

                    pivotGrid.Fields[0].Visible = false;
                    pivotGrid.Fields[0].GroupInterval = FieldGroupInterval.Default;
                    break;
            }
            pivotGrid.EndUpdate();
            pivotGrid.CollapseAll();
        }
        void pivotGrid_CustomGroupInterval(object sender, PivotCustomGroupIntervalEventArgs e) {
            switch(SelectedDemo) {
                case 0:
                    if(!object.ReferenceEquals(e.Field, pivotGrid.Fields[0])) return;
                    if(Convert.ToChar(e.Value.ToString()[0]) < 'F') {
                        e.GroupValue = "A-E";
                        return;
                    }
                    if(Convert.ToChar(e.Value.ToString()[0]) > 'E' && Convert.ToChar(e.Value.ToString()[0]) < 'T') {
                        e.GroupValue = "F-S";
                        return;
                    }
                    if(Convert.ToChar(e.Value.ToString()[0]) > 'S')
                        e.GroupValue = "T-Z";
                    break;
                case 1:
                    if(!object.ReferenceEquals(e.Field, pivotGrid.Fields[3])) return;
                    e.GroupValue = ((DateTime)e.Value).Year + " - " + ((((DateTime)e.Value).Month - 1) / 3 + 1).ToString();
                    break;
            }
        }
    }
}
