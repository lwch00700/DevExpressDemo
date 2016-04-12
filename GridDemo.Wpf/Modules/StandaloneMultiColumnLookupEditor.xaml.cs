﻿using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Core;
using DevExpress.Utils;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using System.Collections.Generic;


namespace CommonDemo {
   [CodeFile("ModuleResources/MultiColumnLookupEditorTemplates(.SL).xaml")]
    public partial class StandaloneMultiColumnLookupEditor : CommonDemoModule {
        NWindDataLoader NWind { get; set; }
        string GenericXamlName { get { return "Generic.xaml"; } }
        IList<Product> Products { get { return (IList<Product>)lookUpEdit.DataContext; } }
        int NewItemRowID { get { return Products.Count + 1; } }
        public StandaloneMultiColumnLookupEditor() {
            Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(string.Format("/{0};component/Themes/{1}", AssemblyHelper.GetPartialName(typeof(StandaloneMultiColumnLookupEditor).Assembly), GenericXamlName), UriKind.Relative) });
            InitializeComponent();
            NWind = Resources["NWindDataLoader"] as NWindDataLoader;
        }
        Control control;
        private void lookUpEdit_ProcessNewValue(DependencyObject sender, DevExpress.Xpf.Editors.ProcessNewValueEventArgs e) {
            if(!(bool)chProcessNewValue.IsChecked)
                return;

            control = new ContentControl { Template = (ControlTemplate)Resources["addNewRecordTemplate"], Tag = e };
            Product row = new Product();
            row.ProductName = e.DisplayText;
            control.DataContext = row;
            FrameworkElement owner = sender as FrameworkElement;
            DialogClosedDelegate closeHandler = CloseAddNewRecordHandler;

            FloatingContainer.ShowDialogContent(control, owner, Size.Empty, new FloatingContainerParameters()
            {
                Title = "Add New Record",
                AllowSizing = false,
                ClosedDelegate = closeHandler,
                ContainerFocusable = false,
                ShowModal = true
            });

            e.PostponedValidation = true;
            e.Handled = true;
        }

        void CloseAddNewRecordHandler(bool? close) {
            if(close.HasValue && close.Value)
                Products.Add((Product)control.DataContext);
            control = null;
        }
        protected override object GetModuleDataContext() {
            return lookUpEdit;
        }
    }
}
