using System;
using System.Windows;
using System.Windows.Data;
using GridDemo;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;


namespace GridDemo {
    [CodeFile("ModuleResources/MultiValueGroupingTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/MultiValueGroupingClasses.(cs)")]
    public partial class MultiValueGrouping : GridDemoModule {
        public MultiValueGrouping() {
            InitializeComponent();
            gc = new GroupingControllerTasksWithCategories(grid);
            gc.AfterGrouping += new EventHandler(gc_AfterGrouping);
            InitButtonCaption();
        }

        GroupingControllerTasksWithCategories gc;
        void InitButtonCaption() {
            groupButton.Content = gc.IsCategoryGrouping ? " Ungroup by 'Category'" : "Group by 'Category'";
        }
        private void groupButton_Click(object sender, RoutedEventArgs e) {
            if(gc.CategoryColumn == null) {
                groupButton.IsEnabled = false;
                return;
            }
            if(!gc.CategoryColumn.IsGrouped)
                grid.GroupBy(gc.CategoryColumn);
            else grid.UngroupBy(gc.CategoryColumn);

        }
        void gc_AfterGrouping(object sender, EventArgs e) {
            InitButtonCaption();
        }
        protected override IGridViewFactory<ColumnWrapper, RowBaseWrapper> GetReportView() {
            return null;
        }
    }
}
