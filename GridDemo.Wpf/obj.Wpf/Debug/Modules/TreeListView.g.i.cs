﻿#pragma checksum "..\..\..\Modules\TreeListView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EDF2B7D0C9DD0535C85162FA66D4999A"
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
using DevExpress.Xpf.Grid.Themes;
using DevExpress.Xpf.Grid.TreeList;
using GridDemo;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
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
    /// TreeListView
    /// </summary>
    public partial class TreeListView : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowSortingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowMovingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowResizingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showIndicatorCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit autoWidthCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowPerPixelScrollingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showDataNavigator;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showVerticalLinesCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showHorizontalLinesCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showTreelLinesCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit NavigationStyleComboBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit chkEnableContextMenu;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton showHideButton;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl treeList;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Modules\TreeListView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TreeListView view;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/treelistview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\TreeListView.xaml"
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
            this.allowSortingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 2:
            this.allowMovingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 3:
            this.allowResizingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 4:
            this.showIndicatorCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 5:
            this.autoWidthCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 6:
            this.allowPerPixelScrollingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 7:
            this.showDataNavigator = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 8:
            this.showVerticalLinesCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 9:
            this.showHorizontalLinesCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 10:
            this.showTreelLinesCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 11:
            this.NavigationStyleComboBox = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 12:
            this.chkEnableContextMenu = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 58 "..\..\..\Modules\TreeListView.xaml"
            this.chkEnableContextMenu.Checked += new System.Windows.RoutedEventHandler(this.chkEnableContextMenu_Checked);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\Modules\TreeListView.xaml"
            this.chkEnableContextMenu.Unchecked += new System.Windows.RoutedEventHandler(this.chkEnableContextMenu_Unchecked);
            
            #line default
            #line hidden
            return;
            case 13:
            this.showHideButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 14:
            this.treeList = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 15:
            this.view = ((DevExpress.Xpf.Grid.TreeListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

