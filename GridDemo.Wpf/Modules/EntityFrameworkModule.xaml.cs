using System;
using System.Linq;
using System.Windows;
using GridDemo.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;

namespace GridDemo {
    [CodeFile("ModuleResources/EntityFrameworkModuleTemplates(.SL).xaml")]
    [CodeFile("ModuleResources/SharedResources(.SL).xaml")]
    [CodeFile("Controls/Converters.(cs)")]
    [CodeFile("Controls/EFServerModeModel1.Designer.(cs)")]
    public partial class EntityFrameworkModule : GridDemoModule {
        DXGridServerModeDBEntities objectContext;
        public EntityFrameworkModule() {
            InitializeComponent();
            Loaded += new RoutedEventHandler(OnLoaded);
        }

        void CreateObjectContextIfNull() {
            if(objectContext != null) return;
            System.Data.EntityClient.EntityConnectionStringBuilder builder = new System.Data.EntityClient.EntityConnectionStringBuilder();
            builder.ProviderConnectionString = ServerModeOptions.SQLConnectionString;
            builder.Metadata = @"res://*/Controls.EFServerModeModel.csdl|res://*/Controls.EFServerModeModel.ssdl|res://*/Controls.EFServerModeModel.msl";
            builder.Provider = "System.Data.SqlClient";
            DataContext = objectContext = new DXGridServerModeDBEntities(builder.ToString());
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            if(DevExpress.Xpf.DemoBase.DemoTesting.DemoTestingHelper.IsTesting) {
                entityServerModeDataSource.ElementType = typeof(WpfServerSideGridTest);
                entityServerModeDataSource.QueryableSource = OutlookDataGenerator.CreateServerObjectsList(30000).AsQueryable();
                grid.ItemsSource = entityServerModeDataSource.Data;
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

        void ShowConnectionWizard() {
            ShowConnectionWizard("Start Demo");
        }
        void ShowConnectionWizard(string windowTitle) {
            DataContext = objectContext = null;
            SQLConnectionWindow window = new SQLConnectionWindow(windowTitle) { Description = ServerModeOptions.GetGridDescription()};
            if(Application.Current != null) window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
            ServerModeOptions.SQLConnectionString = window.GetDataBaseConnectionString();
        }

        void ValidateSource() {
            if(!ServerModeOptions.IsCorrectConnection) {
                grid.ItemsSource = null;
                return;
            }
            CreateObjectContextIfNull();
            if(chkInstantFeedBack.IsChecked.Value)
                grid.ItemsSource = entityInstantFeedbackDataSource.Data;
            else
                grid.ItemsSource = entityServerModeDataSource.Data;
        }

        void CustomizeWaitIndicator(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            if(e.NewValue.ToString() == "Custom") {
                view.WaitIndicatorType = WaitIndicatorType.Panel;
                view.WaitIndicatorStyle = Resources["CustomWaitIndicatorStyle"] as Style;
            } else {
                view.ClearValue(GridViewBase.WaitIndicatorStyleProperty);
                view.WaitIndicatorType = (WaitIndicatorType)e.NewValue;
            }
        }
    }
}
