using System;
using System.Linq;
using System.Windows;
using GridDemo.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Data.Linq;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    [CodeFile("Controls/SQLConnectionWindow(.SL).xaml.(cs)")]
    [CodeFile("Controls/DataGridTestClasses.designer.(cs)")]
    [CodeFile("ModuleResources/LinqServerModeTemplates(.SL).xaml")]
    public partial class LINQToSQLServer : GridDemoModule {
        DataGridTestClassesDataContext linqDataContext;
        public LINQToSQLServer() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(OnLoaded);

        }

        void CreateLinqDataContextIfNull() {
            DataContext = linqDataContext = new DataGridTestClassesDataContext(ServerModeOptions.SQLConnectionString);
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            if(DevExpress.Xpf.DemoBase.DemoTesting.DemoTestingHelper.IsTesting) {
                linqServerModeDataSource.QueryableSource = OutlookDataGenerator.CreateServerObjectsList(30000).AsQueryable();
                grid.ItemsSource = linqServerModeDataSource.Data;
                return;
            }
            if(String.IsNullOrEmpty(ServerModeOptions.SQLConnectionString))
                ShowConnectionWizard();
            ValidateSource();
        }
        void ChangeInstantFeedBack(object sender, RoutedEventArgs e) {
            ValidateSource();
        }
        void Configure(object sender, RoutedEventArgs e) {
            ShowConnectionWizard("Return");
            ValidateSource();
        }

        private void ShowConnectionWizard() {
            ShowConnectionWizard("Start Demo");
        }
        private void ShowConnectionWizard(string windowTitle) {
            DataContext = linqDataContext = null;
            SQLConnectionWindow window = new SQLConnectionWindow(windowTitle) { Description = ServerModeOptions.GetGridDescription() };
            if(Application.Current != null) window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
            ServerModeOptions.SQLConnectionString = window.GetDataBaseConnectionString();
        }
        void ValidateSource() {
            if(!ServerModeOptions.IsCorrectConnection) {
                grid.ItemsSource = null;
                return;
            }
            CreateLinqDataContextIfNull();
            if(chkInstantFeedBack.IsChecked.Value)
                grid.ItemsSource = linqInstantFeedbackDataSource.Data;
            else
                grid.ItemsSource = linqServerModeDataSource.Data;
        }

        void CustomizeWaitIndicator(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(e.NewValue.ToString() == "Custom") {
                view.WaitIndicatorType = WaitIndicatorType.Panel;
                view.WaitIndicatorStyle = Resources["CustomWaitIndicatorStyle"] as Style;
            }
            else {
                view.ClearValue(GridViewBase.WaitIndicatorStyleProperty);
                view.WaitIndicatorType = (WaitIndicatorType)e.NewValue;
            }
        }
    }
}
