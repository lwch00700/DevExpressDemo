using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using DevExpress.Xpf.DemoBase;
using DevExpress.Utils;
using DevExpress.Mvvm;


namespace GridDemo {
    [CodeFile("ModuleResources/RowTemplateClasses.(cs)")]
    [CodeFile("ModuleResources/RowTemplateTemplates(.SL).xaml")]
    public partial class RowTemplate : GridDemoModule {
        public RowTemplate() {
            InitializeComponent();
            CheckBox.IsChecked = true;
            view.Tag = new DelegateCommand<object>(OnSendMail);
        }
        void OnSendMail(object parameter) {
            string emailUri = "mailto:" + parameter.ToString();
            try {
                Process.Start(emailUri);
            } catch { }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            if(grid != null) {
                colEMail.CellTemplate = (DataTemplate)Resources["eMailTemplate"];
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            if(grid != null) {
                colEMail.CellTemplate = null;
            }
        }

        private void RowTemplateComboBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(grid == null)
                return;
            if(rowTemplateComboBox.SelectedIndex == 0)
                view.DataRowTemplate = (DataTemplate)Resources["expandableRowDetailTemplate"];
            if(rowTemplateComboBox.SelectedIndex == 1)
                view.DataRowTemplate = (DataTemplate)Resources["rowDetailTemplate"];
            if(rowTemplateComboBox.SelectedIndex == 2)
                view.DataRowTemplate = (DataTemplate)Resources["rowToolTipTemplate"];
            if(rowTemplateComboBox.SelectedIndex == 3)
                view.ClearValue(DevExpress.Xpf.Grid.TableView.DataRowTemplateProperty);
        }

  protected bool ShouldUseModifiedTheme {
   get { return rowTemplateComboBox.SelectedIndex <= 2; }
  }
    }
}
