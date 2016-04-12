﻿#pragma checksum "..\..\..\Modules\FilterControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3EBBB6AE6A7E461A3C5ECCE57D92624A"
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
using DevExpress.Xpf.Bars;
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
    /// FilterControl
    /// </summary>
    public partial class FilterControl : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showGroupCommandsIcon;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showOperandTypeIcon;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showToolTips;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApplyFilterButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.Filtering.FilterControl filterEditor;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl filterGrid;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colID;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colProduct;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colQuantity;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Modules\FilterControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colTotal;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/filtercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\FilterControl.xaml"
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
            this.showGroupCommandsIcon = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 2:
            this.showOperandTypeIcon = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 3:
            this.showToolTips = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 4:
            this.ApplyFilterButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\Modules\FilterControl.xaml"
            this.ApplyFilterButton.Click += new System.Windows.RoutedEventHandler(this.ApplyFilterButtonClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.filterEditor = ((DevExpress.Xpf.Editors.Filtering.FilterControl)(target));
            return;
            case 6:
            this.filterGrid = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 7:
            this.colID = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 8:
            this.colProduct = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 9:
            this.colUnitPrice = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 10:
            this.colQuantity = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 11:
            this.colTotal = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 12:
            
            #line 71 "..\..\..\Modules\FilterControl.xaml"
            ((DevExpress.Xpf.Grid.TableView)(target)).FilterEditorCreated += new DevExpress.Xpf.Grid.FilterEditorEventHandler(this.TableView_FilterEditorCreated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
