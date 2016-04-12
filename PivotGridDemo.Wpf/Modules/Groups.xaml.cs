using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.PivotGrid;
using DevExpress.DemoData.Models;

namespace PivotGridDemo.PivotGrid {
    public partial class Groups : PivotGridDemoModule {
        bool isExpanded;

        public Groups() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
        }
        void UpdateGroupsExpanded() {
            pivotGrid.BeginUpdate();
            try {
                foreach(PivotGridGroup group in pivotGrid.Groups)
                    foreach(PivotGridField field in group)
                        field.ExpandedInFieldsGroup = isExpanded;
            } finally {
                pivotGrid.EndUpdate();
            }
        }


        private void Collapse_Click(object sender, RoutedEventArgs e) {
            isExpanded = true;
            UpdateGroupsExpanded();
        }

        private void Expand_Click(object sender, RoutedEventArgs e) {
            isExpanded = false;
            UpdateGroupsExpanded();
        }
    }
}
