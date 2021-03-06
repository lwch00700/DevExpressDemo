﻿#pragma checksum "..\..\..\Modules\CellEditors.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8DCBF702C18F5417E09181EBF7F06B39"
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
    /// CellEditors
    /// </summary>
    public partial class CellEditors : GridDemo.GridDemoModule, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit editorShowModeCombobox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit editorButtonShowModeListBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ComboBoxEdit booleanColumnEditorListBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.ListBoxEdit viewsListBox;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit autoCompleteCheckBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit immediatePopupCheckBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit alternativeDisplayTemplateCheckBox;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit alternativeEditTemplateCheckBox;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl grid;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colId;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colPriority;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colUserId;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colHoursActive;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Modules\CellEditors.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridColumn colHasAttachment;
        
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
            System.Uri resourceLocater = new System.Uri("/GridDemo;component/modules/celleditors.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Modules\CellEditors.xaml"
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
            this.editorShowModeCombobox = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 2:
            this.editorButtonShowModeListBox = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 3:
            this.booleanColumnEditorListBox = ((DevExpress.Xpf.Editors.ComboBoxEdit)(target));
            return;
            case 4:
            this.viewsListBox = ((DevExpress.Xpf.Editors.ListBoxEdit)(target));
            return;
            case 5:
            this.autoCompleteCheckBox = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 6:
            this.immediatePopupCheckBox = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            return;
            case 7:
            this.alternativeDisplayTemplateCheckBox = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 73 "..\..\..\Modules\CellEditors.xaml"
            this.alternativeDisplayTemplateCheckBox.Checked += new System.Windows.RoutedEventHandler(this.alternativeDisplayTemplateCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 73 "..\..\..\Modules\CellEditors.xaml"
            this.alternativeDisplayTemplateCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.alternativeDisplayTemplateCheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.alternativeEditTemplateCheckBox = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 74 "..\..\..\Modules\CellEditors.xaml"
            this.alternativeEditTemplateCheckBox.Checked += new System.Windows.RoutedEventHandler(this.alternativeEditTemplateCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 74 "..\..\..\Modules\CellEditors.xaml"
            this.alternativeEditTemplateCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.alternativeEditTemplateCheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.grid = ((DevExpress.Xpf.Grid.GridControl)(target));
            return;
            case 10:
            this.colId = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 11:
            this.colPriority = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 12:
            this.colUserId = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            case 13:
            this.colHoursActive = ((DevExpress.Xpf.Grid.GridColumn)(target));
            
            #line 101 "..\..\..\Modules\CellEditors.xaml"
            this.colHoursActive.Validate += new DevExpress.Xpf.Grid.GridCellValidationEventHandler(this.colHoursActive_Validate);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 103 "..\..\..\Modules\CellEditors.xaml"
            ((DevExpress.Xpf.Editors.Settings.TextEditSettings)(target)).GetIsActivatingKey += new DevExpress.Xpf.Editors.Settings.GetIsActivatingKeyEventHandler(this.TextEditSettings_GetIsActivatingKey);
            
            #line default
            #line hidden
            
            #line 104 "..\..\..\Modules\CellEditors.xaml"
            ((DevExpress.Xpf.Editors.Settings.TextEditSettings)(target)).ProcessActivatingKey += new DevExpress.Xpf.Editors.Settings.ProcessActivatingKeyEventHandler(this.TextEditSettings_ProcessActivatingKey);
            
            #line default
            #line hidden
            return;
            case 15:
            this.colHasAttachment = ((DevExpress.Xpf.Grid.GridColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

