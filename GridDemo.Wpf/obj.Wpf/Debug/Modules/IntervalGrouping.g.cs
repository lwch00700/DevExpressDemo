﻿#pragma checksum "..\..\..\Modules\IntervalGrouping.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58C5E45DB7B442EFF1468973E23D6A5A"
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
    /// IntervalGrouping
    /// </summary>
    public partial class IntervalGrouping : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\Modules\IntervalGrouping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit groupModeList;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Modules\IntervalGrouping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Modules\IntervalGrouping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colCountry;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Modules\IntervalGrouping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colOrderDate;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Modules\IntervalGrouping.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Modules\IntervalGrouping.xaml"
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/intervalgrouping.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\IntervalGrouping.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.groupModeList = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            
            #line 30 "..\..\..\Modules\IntervalGrouping.xaml"
            this.groupModeList.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.groupModeList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            
            #line 45 "..\..\..\Modules\IntervalGrouping.xaml"
            this.grid.CustomColumnGroup += new DevExpress.Xpf.Grid.CustomColumnSortEventHandler(this.grid_CustomColumnGroup);
            
            #line default
            #line hidden
            
            #line 45 "..\..\..\Modules\IntervalGrouping.xaml"
            this.grid.CustomGroupDisplayText += new DevExpress.Xpf.Grid.CustomGroupDisplayTextEventHandler(this.view_CustomGroupDisplayText);
            
            #line default
            #line hidden
            return;
            case 3:
            this.colCountry = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 4:
            this.colOrderDate = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 5:
            this.colUnitPrice = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 6:
            this.view = ((DevExpress.Xpf.Grid.TableView)(target));
            
            #line 62 "..\..\..\Modules\IntervalGrouping.xaml"
            this.view.ShowGridMenu += new DevExpress.Xpf.Grid.GridMenuEventHandler(this.view_ShowGridMenu);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

