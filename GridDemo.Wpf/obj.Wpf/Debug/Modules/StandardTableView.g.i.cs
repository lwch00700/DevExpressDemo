﻿#pragma checksum "..\..\..\Modules\StandardTableView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "861D99169017CCEA15E4008FDE8D4FDF"
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
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
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
    /// StandardTableView
    /// </summary>
    public partial class StandardTableView : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowFixedGroupsCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowCascadeUpdateCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowPerPixelScrollingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowScrollingAnimation;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit autoWidthCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowSortingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowGroupingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowMovingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowResizingCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit allowBestFitCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showIndicatorCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit useEvenRowBackground;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit scrollBarMode;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showDataNavigator;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showVerticalLinesCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit showHorizontalLinesCheckEdit;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit NavigationStyleComboBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit lbSummary;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colID;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colCountry;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colCity;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colQuantity;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colTotal;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Modules\StandardTableView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TableView view;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/standardtableview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\StandardTableView.xaml"
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
            this.allowFixedGroupsCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 2:
            this.allowCascadeUpdateCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 3:
            this.allowPerPixelScrollingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 4:
            this.allowScrollingAnimation = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 5:
            this.autoWidthCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 6:
            this.allowSortingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 7:
            this.allowGroupingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 8:
            this.allowMovingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 9:
            this.allowResizingCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 10:
            this.allowBestFitCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 11:
            this.showIndicatorCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 12:
            this.useEvenRowBackground = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 13:
            this.scrollBarMode = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 14:
            this.showDataNavigator = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 15:
            this.showVerticalLinesCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 16:
            this.showHorizontalLinesCheckEdit = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 17:
            this.NavigationStyleComboBox = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 18:
            this.lbSummary = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            
            #line 66 "..\..\..\Modules\StandardTableView.xaml"
            this.lbSummary.SelectedIndexChanged += new System.Windows.RoutedEventHandler(this.lbSummary_SelectedIndexChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 20:
            this.colID = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 21:
            this.colCountry = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 22:
            this.colCity = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 23:
            this.colUnitPrice = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 24:
            this.colQuantity = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 25:
            this.colTotal = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 26:
            this.view = ((DevExpress.Xpf.Grid.TableView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

