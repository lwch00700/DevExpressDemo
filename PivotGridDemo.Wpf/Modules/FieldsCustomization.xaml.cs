using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Markup;
using System.Windows.Data;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors.Popups;
using System.Windows.Controls;
using DevExpress.DemoData.Models;


namespace PivotGridDemo.PivotGrid {
    public partial class FieldsCustomization : PivotGridDemoModule {
        public FieldsCustomization() {
            InitializeComponent();
            pivotGrid.DataSource = NWindContext.Create().SalesPersons.ToList();
            pivotGrid.FieldListFactory = DefaultFieldListFactory.Instance;
        }

        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if(!IsPopupContentInvisible)
            pivotGrid.ShowFieldList();
        }

        void ShowHideFieldList_Click(object sender, RoutedEventArgs e) {
            pivotGrid.IsFieldListVisible = !pivotGrid.IsFieldListVisible;
        }

        void customizationStyle_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            bool IsFieldListVisible = pivotGrid.IsFieldListVisible;
            pivotGrid.FieldListStyle = (FieldListStyle)customizationStyle.SelectedItem;
            pivotGrid.IsFieldListVisible = IsFieldListVisible;
            if(pivotGrid.FieldListStyle == FieldListStyle.Simple) {
                VisualStateManager.GoToState(this, "HideAdvancedOptions", true);

            } else {
                VisualStateManager.GoToState(this, "ShowAdvancedOptions", true);
            }
        }

        void OnAllowCustomizationFormChanged(object sender, EditValueChangedEventArgs e) {
            if(!IsLoaded)
                return;
            pivotGrid.IsFieldListVisible = pivotGrid.AllowCustomizationForm;
        }

        void OnCustomizationLayoutsEditValueChanged(object sender, EditValueChangedEventArgs e) {
            FieldListAllowedLayouts layout = (FieldListAllowedLayouts)customizationLayouts.SelectedItems[0];
            foreach(FieldListAllowedLayouts layout2 in customizationLayouts.SelectedItems)
                layout = layout | layout2;
            pivotGrid.FieldListAllowedLayouts = layout;
            EnsureCurrentLayoutItems(true);
        }

        void OnCustomizationLayoutsPopupContentSelectionChanged(object sender, SelectionChangedEventArgs e) {
            PopupBaseEditHelper.GetOkButton(customizationLayouts).IsEnabled = GetListBox().SelectedItems.Count > 0;
        }
        PopupListBox GetListBox() {
            return (PopupListBox)LayoutHelper.FindElement((FrameworkElement)PopupBaseEditHelper.GetPopup(customizationLayouts).PopupContent, IsListBox);
        }
        bool IsListBox(FrameworkElement d) {
            return d as PopupListBox != null;
        }

        void OnCurrentLayoutEditValueChanged(object sender, EditValueChangedEventArgs e) {
            EnsureCurrentLayoutItems(false);
        }

        void EnsureCurrentLayoutItems(bool includeCurrent) {
            currentLayout.Items.BeginUpdate();
            currentLayout.Items.Clear();
            foreach(FieldListAllowedLayouts layout2 in customizationLayouts.SelectedItems) {
                currentLayout.Items.Add(ToFieldListAllowedLayouts(layout2));
            }
            if(includeCurrent && !currentLayout.Items.Contains(pivotGrid.FieldListLayout))
                currentLayout.Items.Add(pivotGrid.FieldListLayout);
            currentLayout.Items.EndUpdate();
        }

        FieldListAllowedLayouts ToFieldListAllowedLayouts(FieldListLayout layout) {
            switch(layout) {
                case FieldListLayout.BottomPanelOnly1by4:
                    return FieldListAllowedLayouts.BottomPanelOnly1by4;
                case FieldListLayout.BottomPanelOnly2by2:
                    return FieldListAllowedLayouts.BottomPanelOnly2by2;
                case FieldListLayout.StackedDefault:
                    return FieldListAllowedLayouts.StackedDefault;
                case FieldListLayout.StackedSideBySide:
                    return FieldListAllowedLayouts.StackedSideBySide;
                case FieldListLayout.TopPanelOnly:
                    return FieldListAllowedLayouts.TopPanelOnly;
                default:
                    throw new NotImplementedException("FieldListLayout");
            }
        }

        FieldListLayout ToFieldListAllowedLayouts(FieldListAllowedLayouts layout) {
            switch(layout) {
                case FieldListAllowedLayouts.BottomPanelOnly1by4:
                    return FieldListLayout.BottomPanelOnly1by4;
                case FieldListAllowedLayouts.BottomPanelOnly2by2:
                    return FieldListLayout.BottomPanelOnly2by2;
                case FieldListAllowedLayouts.StackedDefault:
                    return FieldListLayout.StackedDefault;
                case FieldListAllowedLayouts.StackedSideBySide:
                    return FieldListLayout.StackedSideBySide;
                case FieldListAllowedLayouts.TopPanelOnly:
                    return FieldListLayout.TopPanelOnly;
                default:
                    return FieldListLayout.StackedDefault;
            }
        }
    }

    public class FieldListVisibleToCommandTextConverter : MarkupExtension, IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            switch((bool)value) {
                case false:
                    return "Show Field List";
                default:
                    return "Hide Field List";
            }
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
