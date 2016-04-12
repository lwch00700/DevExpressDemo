using System;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Bars;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using DevExpress.Mvvm;

namespace TreeListDemo {
    public partial class DragDrop : TreeListDemoModule {
        public DragDrop() {
            InitializeComponent();
        }

        void dragDropManager_Drop(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListDropEventArgs e) {
            if(e.TargetNode != null)
                foreach(object obj in e.DraggedRows)
                    (e.SourceManager.GetObject(obj) as Employee).GroupName = (e.TargetNode.Content as Employee).GroupName;
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            BarButtonItem barItem = sender as BarButtonItem;
            if(barItem == null) return;
            string groupName = barItem.Content as string;

            treeList.BeginDataUpdate();
            foreach(Employee empl in view.DataControl.SelectedItems)
                empl.GroupName = groupName;
            treeList.EndDataUpdate();
        }
    }

    public class DragDropViewModel : BindableBase {
        public DragDropViewModel() {
            SelectionMode = MultiSelectMode.Row;
            DragDropSourceGenerator.InitSources(this);
        }

        MultiSelectMode selectionModeCore;
        public MultiSelectMode SelectionMode {
            get { return selectionModeCore; }
            set { SetProperty(ref selectionModeCore, value, () => SelectionMode); }
        }
        public ObservableCollection<Employee> ActiveEmployees { get; internal set; }
        public ObservableCollection<Employee> NewEmployees { get; internal set; }
    }
}
