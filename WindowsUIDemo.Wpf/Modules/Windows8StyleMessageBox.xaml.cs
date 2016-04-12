using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.WindowsUI;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
namespace WindowsUIDemo {
    public partial class Windows8StyleMessageBox : WindowsUIDemoModule {
        public Windows8StyleMessageBox() {
            InitializeComponent();
            ComboBoxEdit.SetupComboBoxEnumItemSource<MessageBoxButton, MessageBoxButton>(buttons);
            ComboBoxEdit.SetupComboBoxEnumItemSource<MessageBoxImage, MessageBoxImage>(icons);
            ComboBoxEdit.SetupComboBoxEnumItemSource<FloatingMode, FloatingMode>(floatingMode);
            if(BrowserInteropHelper.IsBrowserHosted) {
                floatingMode.Visibility = System.Windows.Visibility.Collapsed;
                floatingModeLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            WinUIMessageBox.Show(
                Window.GetWindow(this),
                contentEdit.DisplayText,
                captionEdit.DisplayText,
                (MessageBoxButton)buttons.EditValue,
                (MessageBoxImage)icons.EditValue,
                MessageBoxResult.None, MessageBoxOptions.None,
                (FloatingMode)floatingMode.EditValue
                );
        }
    }
}
