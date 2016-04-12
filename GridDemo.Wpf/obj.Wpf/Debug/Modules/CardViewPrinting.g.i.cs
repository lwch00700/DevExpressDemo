﻿#pragma checksum "..\..\..\Modules\CardViewPrinting.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7EA314AC9108910DE56490ED6E0BF0EF"
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
    /// CardViewPrinting
    /// </summary>
    public partial class CardViewPrinting : GridDemo.PrintViewGridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit printStyleChooser;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit cePrintAutoCardWidth;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.SpinEdit sePrintMaximumCardColumns;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button newTabButton;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button newWindowButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.DXTabControl tabControl;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Modules\CardViewPrinting.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.CardView cardView;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/cardviewprinting.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\CardViewPrinting.xaml"
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
            this.printStyleChooser = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            
            #line 35 "..\..\..\Modules\CardViewPrinting.xaml"
            this.printStyleChooser.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.printStyleChooser_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cePrintAutoCardWidth = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 3:
            this.sePrintMaximumCardColumns = ((DevExpress.Xpf.Editors.SpinEdit)(target));
            return;
            case 4:
            this.newTabButton = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\Modules\CardViewPrinting.xaml"
            this.newTabButton.Click += new System.Windows.RoutedEventHandler(this.newTabButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.newWindowButton = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\Modules\CardViewPrinting.xaml"
            this.newWindowButton.Click += new System.Windows.RoutedEventHandler(this.newWindowButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tabControl = ((DevExpress.Xpf.Core.DXTabControl)(target));
            
            #line 56 "..\..\..\Modules\CardViewPrinting.xaml"
            this.tabControl.TabHidden += new DevExpress.Xpf.Core.TabControlTabHiddenEventHandler(this.tabControl_TabHidden);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 8:
            this.cardView = ((DevExpress.Xpf.Grid.CardView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
