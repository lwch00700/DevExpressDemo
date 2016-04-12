using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Editors;
using System.Data;


namespace CommonDemo {
    [CodeFile("ModuleResources/LookUpEditWithMultipleSelectionTemplates(.SL).xaml")]
    public partial class LookUpEditWithMultipleSelection : CommonDemoModule {
        public LookUpEditWithMultipleSelection() {
            InitializeComponent();
            lookUpEdit.ItemsSource = CarsData.NewDataSource;
        }
    }
}
