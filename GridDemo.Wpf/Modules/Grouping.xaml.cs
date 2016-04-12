using System;

using System.Data;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    public partial class Grouping : GridDemoModule {
        public Grouping() {
            InitializeComponent();
            viewsListBox.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(viewsListBox_SelectionChanged);
            GroupByCountryThenCity();
        }
        void viewsListBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            grid.View = (GridViewBase)FindResource(viewsListBox.SelectedIndex == 0 ? "tableView" : "cardView");
            allowFixedGroupsCheckBox.IsEnabled = viewsListBox.SelectedIndex == 0;
        }
  void GroupByCountryThenCity() {
            grid.ClearGrouping();
   grid.GroupBy("Country");
   grid.GroupBy("City");
  }

  private void GroupByCountryThenCityThenOrderDate() {
            grid.ClearGrouping();
            grid.GroupBy("Country");
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
  }

  private void GroupByCityThenOrderDate() {
            grid.ClearGrouping();
            grid.GroupBy("City");
            grid.GroupBy("OrderDate");
  }
        private void ClearGrouping() {
            grid.ClearGrouping();
        }
        private void groupList_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
   if(grid == null) return;
   switch(groupList.SelectedIndex) {
    case 0: GroupByCountryThenCity(); break;
    case 1: GroupByCountryThenCityThenOrderDate(); break;
    case 2: GroupByCityThenOrderDate(); break;
                case 3: ClearGrouping(); break;
   }
  }
        protected override DataViewBase GetExportView() {
            return null;
        }
    }
}
