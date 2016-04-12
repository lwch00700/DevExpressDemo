using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;
using DevExpress.Xpf.Grid;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Mvvm;

namespace TreeListDemo{
    public partial class BuildTreeFromSelfReferenceData : TreeListDemoModule {
        public BuildTreeFromSelfReferenceData() {
            InitializeComponent();
        }

        private void DemoModuleControl_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            this.treeList.View.ExpandAllNodes();
        }
    }

    public class SelfReferenceDataViewModel : BindableBase {
        bool showServiceColumns;

        public bool ShowServiceColumns {
            get { return showServiceColumns; }
            set { SetProperty(ref showServiceColumns, value, () => ShowServiceColumns); }
        }
        public string KeyFieldName { get { return "Id"; } }
        public string ParentFieldName { get { return "ParentId"; } }
    }
}
