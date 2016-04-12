using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DevExpress.Export;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
using Microsoft.Win32;
using DevExpress.XtraExport.Helpers;
using DevExpress.Xpf.Grid.Printing;

namespace GridDemo {
    public partial class DemoModuleExportControl : UserControl {
        public DataViewBase View { get; private set; }
        public IGridViewFactory<ColumnWrapper, RowBaseWrapper> DesignerWrapper { get { return View as IGridViewFactory<ColumnWrapper, RowBaseWrapper>; } }
        public TableView TableView { get { return (TableView)View; } }
        public ICommand ExportCommand { get; private set; }

        public DemoModuleExportControl(DataViewBase view, bool allowDataAwareExport = true, bool allowWysiwygExport = true, bool allowReport = false) {
            ExportCommand = new DelegateCommand<ExportFormat>(Export);
            DataContext = View = view;
            InitializeComponent();
            if(!allowDataAwareExport)
                dataAwareExportPanel.Visibility = System.Windows.Visibility.Collapsed;
            if(!allowWysiwygExport) {
                wysiwygExportPanel.Visibility = System.Windows.Visibility.Collapsed;
                printPreviewButton.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        #region Data Aware Export

        void DataAwareExportToXlsx(object sender, RoutedEventArgs e) {
            new DemoModuleExportHelper(TableView).ExportToXlsx();
        }
        void DataAwareExportToXls(object sender, RoutedEventArgs e) {
            new DemoModuleExportHelper(TableView).ExportToXls();
        }
        void DataAwareExportToCsv(object sender, RoutedEventArgs e) {
            new DemoModuleExportHelper(TableView).ExportToCsv();
        }

        #endregion
        #region WYSIWYG

        void Export(ExportFormat format) {
            PrintableControlLink link = CreateLink();
            LinkPreviewModel model = CreateLinkPreviewModel(link);
            ExportProgressWaitIndicator exportDialog = CreateExportDialog(model);
            bool buildCompleted = false;

            EventHandler createDocumentCompletedHandler = (d, e) => {
                buildCompleted = true;
                exportDialog.Close();
            };
            link.PrintingSystem.AfterBuildPages += createDocumentCompletedHandler;
            link.CreateDocument(true);
            exportDialog.ShowDialog();
            link.PrintingSystem.AfterBuildPages -= createDocumentCompletedHandler;

            if(buildCompleted)
                model.ExportCommand.Execute(format);
            else
                model.StopCommand.Execute(null);
        }
        PrintableControlLink CreateLink() {
            return new PrintableControlLink((IPrintableControl)View);
        }
        LinkPreviewModel CreateLinkPreviewModel(PrintableControlLink link) {
            return new LinkPreviewModel(link) { DialogService = new DevExpress.Xpf.Printing.DialogService(this) };
        }
        ExportProgressWaitIndicator CreateExportDialog(LinkPreviewModel model) {
            return new ExportProgressWaitIndicator() { DataContext = model, Owner = Application.Current.MainWindow };
        }

        #endregion
    }
    public class DemoModuleExportHelper {
        readonly TableView view;

        public DemoModuleExportHelper(TableView view) {
            this.view = view;
        }

        public void ExportToXlsx() {
            string fileName = GetFileName(new XlsxExportOptions());
            Export((file, options) => view.ExportToXlsx(file, options), fileName, new XlsxExportOptionsEx());
        }
        public void ExportToXls() {
            string fileName = GetFileName(new XlsExportOptions());
            Export((file, options) => view.ExportToXls(file, options), fileName, new XlsExportOptionsEx());
        }
        public void ExportToCsv() {
            string fileName = GetFileName(new CsvExportOptions());
            Export((file, options) => view.ExportToCsv(file, options), fileName, new CsvExportOptionsEx());
        }
        void Export<T>(Action<string, T> exportToFile, string fileName, T options) where T : ExportOptionsBase {
            if(string.IsNullOrEmpty(fileName))
                return;

            try {
                ExportCore(exportToFile, fileName, options);
            }
            catch(Exception e) {
                DXMessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void ExportCore<T>(Action<string, T> exportToFile, string fileName, T options) where T : ExportOptionsBase {
            DXSplashScreen.Show<ExportWaitIndicator>();
            ((IDataAwareExportOptions)options).ExportProgress += ExportProgress;
            try {
                exportToFile(fileName, options);
            }
            finally {
                ((IDataAwareExportOptions)options).ExportProgress -= ExportProgress;
                DXSplashScreen.Close();
            }
            if(ShouldOpenExportedFile())
                ProcessLaunchHelper.StartProcess(fileName, false);
        }
        void ExportProgress(ProgressChangedEventArgs ea) {
            DXSplashScreen.Progress(ea.ProgressPercentage);
        }
        static string GetFileName(ExportOptionsBase options) {
            return GetFileName(ExportOptionsControllerBase.GetControllerByOptions(options));
        }
        static string GetFileName(ExportOptionsControllerBase controller) {
            SaveFileDialog dlg = CreateSaveFileDialog(controller);
            if(dlg.ShowDialog() == true && !string.IsNullOrEmpty(dlg.FileName))
                return FileHelper.SetValidExtension(dlg.FileName, controller.FileExtensions[0], controller.FileExtensions);
            else
                return string.Empty;
        }
        static SaveFileDialog CreateSaveFileDialog(ExportOptionsControllerBase controller) {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title);
            dlg.ValidateNames = true;
            dlg.FileName = PrintPreviewOptions.DefaultFileNameDefault;
            dlg.Filter = controller.Filter;
            return dlg;
        }
        static bool ShouldOpenExportedFile() {
            return DXMessageBox.Show(
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestion),
                PreviewLocalizer.GetString(PreviewStringId.Msg_OpenFileQuestionCaption),
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
    public class PrintingIconImageSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string rawIconName = value as string;
            if(rawIconName == null)
                return null;
            string iconName = Regex.Replace(rawIconName, "[^a-zA-Z]", string.Empty);
            string iconPath = "Images/BarItems/" + iconName + "_32x32.png";
            return new PrintingResourceImageExtension() { ResourceName = iconPath }.ProvideValue(null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
