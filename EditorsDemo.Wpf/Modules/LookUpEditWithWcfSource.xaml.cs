using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using System.Collections;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using System.Data;
using DevExpress.Xpf.Editors;
using DevExpress.DemoData;
using System.Collections.Generic;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using EditorsDemo.SCService;
using EditorsDemo.ViewModels;


namespace EditorsDemo {
    [CodeFile("ModuleResources/LookUpEditTemplates(.SL).xaml")]
    public partial class LookUpEditWithWcfInstantFeedbackSource : EditorsDemoModule {
        public LookUpEditWithWcfInstantFeedbackSource() {
            InitializeComponent();
            WCFInstantFeedbackViewModel viewModel = new WCFInstantFeedbackViewModel();
            DataContext = viewModel;
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
        }

    }
}
