﻿#pragma checksum "..\..\..\Controls\MiltiSelectionOptionsControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AF69215ECDF0561BE79F3D674E415036"
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
    /// MultiSelectionOptionsControl
    /// </summary>
    public partial class MultiSelectionOptionsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Core.GroupFrame groupBoxControl;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit comboBoxControl;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectButton;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UnselectButton;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReselectButton;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/controls/miltiselectionoptionscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
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
            this.groupBoxControl = ((DevExpress.Xpf.Core.GroupFrame)(target));
            return;
            case 2:
            this.comboBoxControl = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 3:
            this.SelectButton = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
            this.SelectButton.Click += new System.Windows.RoutedEventHandler(this.SelectButtonClickInClass);
            
            #line default
            #line hidden
            return;
            case 4:
            this.UnselectButton = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
            this.UnselectButton.Click += new System.Windows.RoutedEventHandler(this.UnselectButtonClickInClass);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ReselectButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Controls\MiltiSelectionOptionsControl.xaml"
            this.ReselectButton.Click += new System.Windows.RoutedEventHandler(this.ReselectButtonClickInClass);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

