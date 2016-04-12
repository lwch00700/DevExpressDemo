using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.XtraPivotGrid;
using DevExpress.Xpf.Core;

namespace PivotGridDemo.PivotGrid {

    public partial class DataSourceDialog : Control {
        const string CubeEditName = "CubeEdit",
        ServerRadioName = "AnalysisServerRadio",
        TextCubeFileName = "TextCubeFile",
        CubeRadioName = "CubeRadio";

        bool OpenCubeFile(string fileName) {
            try {
                CubeEdit.Text = fileName;
                metaGetter.ConnectionString = "Provider=msolap;Data Source=" + CubeEdit.Text;
                RetriveCatalogsAndCubes();
                return true;
            } finally {
                metaGetter.Connected = false;
            }
        }

        void RetriveCatalogsAndCubes() {
            if(!InitComboBox(metaGetter.GetCatalogs(), CatalogsCombo)) {
                ShowMessage("There is no catalogs in the cube file.");
            }
            IsCatalogsRetriving = false;
        }

        void UpdateControls() {
            bool isServer = AnalysisServerRadio.IsChecked.Value;
            if(CubeEdit.Text.Length > 0)
                ConnectButton.IsEnabled = true;
            CatalogsCombo.IsEnabled = isServer;
            CubesCombo.IsEnabled = isServer;
            CubeEdit.Text = string.Empty;
            CubeEdit.AllowDefaultButton = !isServer;
            ClearCombo(CatalogsCombo);
            ClearCombo(CubesCombo);
            TextCubeFile.Text = isServer ? "Server" : "Cube File";
        }

        public string GetConnectionString() {
            if(CatalogOrCubeEmpty())
                return null;
            return GetConnectionStringCore() +
                ";Initial Catalog=" + (string)CatalogsCombo.EditValue +
                ";Cube Name=" + (string)CubesCombo.EditValue;
        }

        string GetConnectionStringCore() {
            return "Provider=msolap;Data Source=" + CubeEdit.Text;
        }

        void ShowMessage(string message) {
            DXMessageBox.Show(message, Application.Current.MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        bool InitComboBox(List<string> items, ComboBoxEdit edit) {
            ClearCombo(edit);
            if(items!=null && items.Count > 0) {
                edit.Items.AddRange(items.ToArray());
                edit.SelectedItem = edit.Items[0];
                return true;
            }
            return false;
        }

        ButtonEdit CubeEdit { get; set; }
        RadioButton AnalysisServerRadio { get; set; }
        RadioButton CubeRadio { get; set; }
        TextBlock TextCubeFile { get; set; }

        void ApplyPlatformTemplate() {
            CubeEdit = GetTemplateChild(CubeEditName) as ButtonEdit;
            CubeEdit.EditValueChanged += new EditValueChangedEventHandler(CubeEdit_EditValueChanged);
            ConnectButton.IsEnabled = false;
            AnalysisServerRadio = GetTemplateChild(ServerRadioName) as RadioButton;
            CubeRadio = GetTemplateChild(CubeRadioName) as RadioButton;
            TextCubeFile = GetTemplateChild(TextCubeFileName) as TextBlock;
            AnalysisServerRadio.Checked += new RoutedEventHandler(AnalysisServerRadio_Checked);
            CubeRadio.Checked += new RoutedEventHandler(CubeRadio_Checked);
            CubeEdit.DefaultButtonClick += new RoutedEventHandler(CubeEdit_DefaultButtonClick);

        }

        void CubeEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            if(CubeEdit.Text.Length > 0)
                ConnectButton.IsEnabled = true;
            else ConnectButton.IsEnabled = false;
        }

        void CubeEdit_DefaultButtonClick(object sender, RoutedEventArgs e) {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Cube files (*.cub)|*.cub";
            if(dialog.ShowDialog() == true) {
                OpenCubeFile(dialog.FileName);
            }
        }

        void CubeRadio_Checked(object sender, RoutedEventArgs e) {
            UpdateControls();
        }

        void AnalysisServerRadio_Checked(object sender, RoutedEventArgs e) {
            UpdateControls();
        }

        void RetriveCubes() {
            if(!InitComboBox(metaGetter.GetCubes(CatalogsCombo.SelectedItem.ToString()), CubesCombo))
                ShowMessage("There is no catalogs in the cube file.");
            IsCubesRetriving = false;
        }
    }
}
