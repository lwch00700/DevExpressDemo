﻿#pragma checksum "..\..\..\Modules\MultiRowSelection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C0F1099C193CC364E5BA241DFBBCF2C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.DemoData;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.ConditionalFormatting;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
using GridDemo;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GridDemo {
    
    
    /// <summary>
    /// MultiRowSelection
    /// </summary>
    public partial class MultiRowSelection : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit checkShowSelectionRectangle;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit selectionModeListBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit viewsListBox;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GridDemo.MultiSelectionOptionsControl ProductsMultiSelectionOptionsControl;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GridDemo.MultiSelectionOptionsControl PriceMultiSelectionOptionsControl;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit SelectionRowsListBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colID;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colProduct;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colQuantity;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colTotal;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Modules\MultiRowSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TableView defaultView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/multirowselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\MultiRowSelection.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.checkShowSelectionRectangle = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 2:
            this.selectionModeListBox = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            
            #line 57 "..\..\..\Modules\MultiRowSelection.xaml"
            this.selectionModeListBox.SelectedIndexChanged += new System.Windows.RoutedEventHandler(this.selectionModeListBox_SelectedIndexChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.viewsListBox = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            return;
            case 4:
            this.ProductsMultiSelectionOptionsControl = ((GridDemo.MultiSelectionOptionsControl)(target));
            return;
            case 5:
            this.PriceMultiSelectionOptionsControl = ((GridDemo.MultiSelectionOptionsControl)(target));
            return;
            case 6:
            this.SelectionRowsListBox = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            return;
            case 7:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            
            #line 101 "..\..\..\Modules\MultiRowSelection.xaml"
            this.grid.CustomSummary += new DevExpress.Data.CustomSummaryEventHandler(this.grid_CustomSummary);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\Modules\MultiRowSelection.xaml"
            this.grid.SelectionChanged += new DevExpress.Xpf.Grid.GridSelectionChangedEventHandler(this.gridView_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 101 "..\..\..\Modules\MultiRowSelection.xaml"
            this.grid.CurrentItemChanged += new DevExpress.Xpf.Grid.CurrentItemChangedEventHandler(this.grid_CurrentItemChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.colID = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 9:
            this.colProduct = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 10:
            this.colUnitPrice = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 11:
            this.colQuantity = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 12:
            this.colTotal = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 13:
            this.defaultView = ((DevExpress.Xpf.Grid.TableView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

