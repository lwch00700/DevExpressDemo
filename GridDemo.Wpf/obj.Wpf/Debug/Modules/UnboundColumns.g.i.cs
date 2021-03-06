﻿#pragma checksum "..\..\..\Modules\UnboundColumns.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "25C8BB0BF2232660643B818127E4B82A"
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
    /// UnboundColumns
    /// </summary>
    public partial class UnboundColumns : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit columnsList;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button showExpressionEditorButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colOrderID;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colProduct;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUnitPrice;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colDiscount;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colQuantity;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colDiscountAmount;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colTotal;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Modules\UnboundColumns.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colTotalScale;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/unboundcolumns.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\UnboundColumns.xaml"
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
            this.columnsList = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            return;
            case 2:
            this.showExpressionEditorButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Modules\UnboundColumns.xaml"
            this.showExpressionEditorButton.Click += new System.Windows.RoutedEventHandler(this.showExpressionEditorButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 4:
            this.colOrderID = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 5:
            this.colProduct = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 6:
            this.colUnitPrice = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 7:
            this.colDiscount = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 8:
            this.colQuantity = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 9:
            this.colDiscountAmount = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 10:
            this.colTotal = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 11:
            this.colTotalScale = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

