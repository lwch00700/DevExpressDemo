using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace DevExpress.Xpf.LayoutControlDemo {
    public partial class pageLayoutControl : LayoutControlDemoModule {
        public pageLayoutControl() {
            InitializeComponent();
        }

        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if (IsPopupContentInvisible && layoutItems.IsCustomization)
                layoutItems.Controller.CustomizationController.SelectedElements.Clear();
        }

        void layoutItems_IsCustomizationChanged(object sender, EventArgs e) {
            if (layoutItems.IsCustomization)
                layoutItems.Controller.CustomizationController.SelectionChanged += layoutItems_SelectionChanged;
            else
                layoutItems.Controller.CustomizationController.SelectionChanged -= layoutItems_SelectionChanged;
            if (PropertiesControl != null)
                PropertiesControl.SelectedItem = null;
        }
        void layoutItems_Loaded(object sender, RoutedEventArgs e) {
            layoutItems.IsCustomization = true;
        }
        void layoutItems_SelectionChanged(object sender, LayoutControlSelectionChangedEventArgs e) {
            if (e.SelectedElements.Count == 1 && !ReferenceEquals(e.SelectedElements[0], layoutItems))
                PropertiesControl.SelectedItem = e.SelectedElements[0];
            else
                PropertiesControl.SelectedItem = null;
        }

        void AllowAvailableItemsDuringCustomizationCheckEdit_Checked(object sender, RoutedEventArgs e) {
            layoutItems.AllowItemMovingDuringCustomization = true;
        }
        void AllowNewItemsDuringCustomizationCheckEdit_Checked(object sender, RoutedEventArgs e) {
            layoutItems.AllowAvailableItemsDuringCustomization = true;
        }

        void LayoutGroupViewGroupBoxRadioButton_Checked(object sender, RoutedEventArgs e) {
            var radioButton = (RadioButton)sender;
            var group = (LayoutGroup)radioButton.DataContext;
            if (group.Header == null)
                group.Header = "Group";
        }
        void LayoutGroupHeaderTextEdit_EditValueChanging(object sender, EditValueChangingEventArgs e) {
            var textBox = (TextEdit)sender;
            var layoutGroup = textBox.DataContext as LayoutGroup;
            if (layoutGroup == null)
                return;
            if (textBox.EditValue != null && object.Equals(textBox.EditValue, e.NewValue) &&
                !((LayoutGroup)layoutGroup.Parent).View.Equals(LayoutGroupView.Tabs))
                layoutGroup.View = LayoutGroupView.GroupBox;
        }
        void LayoutGroupIsCollapsibleCheckBox_Checked(object sender, RoutedEventArgs e) {
            var checkEdit = (CheckEdit)sender;
            ((LayoutGroup)checkEdit.DataContext).View = LayoutGroupView.GroupBox;
        }
        void LayoutGroupIsCollapsedCheckBox_Checked(object sender, RoutedEventArgs e) {
            var checkEdit = (CheckEdit)sender;
            var group = (LayoutGroup)checkEdit.DataContext;
            group.View = LayoutGroupView.GroupBox;
            group.IsCollapsible = true;
        }
    }
}
