using System;
using System.Windows;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.Editors;


namespace RichEditDemo {
    public partial class OperationRestrictions : RichEditDemoModule {
        public OperationRestrictions() {
            InitializeComponent();
            RtfLoadHelper.Load("TextWithImages.rtf", richEdit);
            edtMinZoomFactor.EditValue = 0.5;
            edtMaxZoomFactor.EditValue = 3.0;
        }

        public bool IsHideDisabled { get { return edtHideDisabled.IsChecked.Value; } }
        public string CurrentFileName { get { return richEdit.Options.DocumentSaveOptions.CurrentFileName; } }

        public DocumentCapability UpdateBarItemVisibility(bool? capabilityCheckBoxChecked) {
            if (!capabilityCheckBoxChecked.HasValue || capabilityCheckBoxChecked.Value)
                return DocumentCapability.Enabled;
            return (IsHideDisabled) ? DocumentCapability.Hidden : DocumentCapability.Disabled;
        }

        DocumentCapability GetOptionValue(object sender) {
            if (((CheckEdit)sender).IsChecked.Value)
                return DocumentCapability.Enabled;
            if (IsHideDisabled)
                return DocumentCapability.Hidden;
            return DocumentCapability.Disabled;
        }

        void clipboardCut_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Cut = GetOptionValue(sender);
        }
        void clipboardCopy_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Copy = GetOptionValue(sender);
        }
        void clipboardPaste_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Paste = GetOptionValue(sender);
        }
        void fileNew_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.CreateNew = GetOptionValue(sender);
        }
        void fileOpen_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Open = GetOptionValue(sender);
        }
        void fileSave_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Save = GetOptionValue(sender);
        }
        void fileSaveAs_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.SaveAs = GetOptionValue(sender);
        }
        void filePrint_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Printing = GetOptionValue(sender);
        }
        void commonDrag_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Drag = GetOptionValue(sender);
        }
        void commonDrop_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Drop = GetOptionValue(sender);
        }
        void zoom_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.Zooming = GetOptionValue(sender);
        }
        void readonly_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.ReadOnly = ((CheckEdit)sender).IsChecked.Value;
        }
        void showPopupMenu_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.Behavior.ShowPopupMenu = GetOptionValue(sender);
        }
        void hideDisabledItems_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.BeginUpdate();
            try {
                RichEditBehaviorOptions options = richEdit.Options.Behavior;
                options.Cut = UpdateBarItemVisibility(this.edtAllowCut.IsChecked);
                options.Copy = UpdateBarItemVisibility(this.edtAllowCopy.IsChecked);
                options.Paste = UpdateBarItemVisibility(this.edtAllowPaste.IsChecked);
                options.Drag = UpdateBarItemVisibility(this.edtAllowDrag.IsChecked);
                options.Drop = UpdateBarItemVisibility(this.edtAllowDrop.IsChecked);
                options.Save = UpdateBarItemVisibility(this.edtAllowSave.IsChecked);
                options.SaveAs = UpdateBarItemVisibility(this.edtAllowSaveAs.IsChecked);
                options.Printing = UpdateBarItemVisibility(this.edtAllowPrinting.IsChecked);
                options.CreateNew = UpdateBarItemVisibility(this.edtAllowCreateNew.IsChecked);
                options.Open = UpdateBarItemVisibility(this.edtAllowOpen.IsChecked);
                options.Zooming = UpdateBarItemVisibility(this.edtAllowZoom.IsChecked);
            }
            finally {
                richEdit.EndUpdate();
            }
        }

        void minZoom_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            try {
                richEdit.Options.Behavior.MinZoomFactor = Convert.ToSingle(e.NewValue);
            }
            catch {
            }
        }

        void maxZoom_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            try {
                richEdit.Options.Behavior.MaxZoomFactor = Convert.ToSingle(e.NewValue);
            }
            catch {
            }
        }
    }
}
