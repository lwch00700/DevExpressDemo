using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using System.ComponentModel;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System.Windows.Interop;

namespace EditorsDemo {
    public partial class DXMessageBoxModule : EditorsDemoModule {
        public DXMessageBoxModule() {
            InitializeComponent();
            ComboBoxEdit.SetupComboBoxEnumItemSource<MessageBoxButton, MessageBoxButton>(buttons);
            List<EnumHelperData> iconsCollection = new List<EnumHelperData>();
            foreach (string mbi in Enum.GetNames(typeof(MessageBoxImage))) iconsCollection.Add(new EnumHelperData() { Text = mbi, Value = Enum.Parse(typeof(MessageBoxImage), mbi)});
            icons.ItemsSource = iconsCollection;
            icons.DisplayMember = "Text";
            ComboBoxEdit.SetupComboBoxEnumItemSource<FloatingMode, FloatingMode>(floatingMode);
            icons.SelectedIndex = 0;
            buttons.SelectedIndex = 0;
            floatingMode.SelectedIndex = 1;
            if (BrowserInteropHelper.IsBrowserHosted) {
                floatingMode.Visibility = System.Windows.Visibility.Collapsed;
                floatingModeLabel.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            DXMessageBox.Show(
                LayoutHelper.GetRoot(this) as Window,
                contentEdit.DisplayText,
                captionEdit.DisplayText,
                (MessageBoxButton)buttons.EditValue,
                (MessageBoxImage)((EnumHelperData)icons.EditValue).Value,
                MessageBoxResult.None, MessageBoxOptions.None,
                (FloatingMode)floatingMode.EditValue
                );
        }

        public bool BrouwserInteropHlper { get; set; }
    }
    public class EnumHelperData {
        public string Text { get; set; }
        public object Value { get; set; }
    }
}
