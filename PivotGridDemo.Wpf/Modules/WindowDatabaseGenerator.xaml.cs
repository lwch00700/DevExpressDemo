using DevExpress.Xpf.Core;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using PivotGridDemo.PivotGrid.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PivotGridDemo.PivotGrid {
    public partial class WindowDatabaseGenerator : DXWindow {
        bool windowWasClosed;

        public WindowDatabaseGenerator()
            : this(null, "Start Demo") {
        }
        public WindowDatabaseGenerator(UserControl control, string demoString) {
            InitializeComponent();
            DemoString = demoString;
            if(control != null) {
                Owner = Window.GetWindow(control);
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
            } else
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowInTaskbar = false;
            btnGenerateDatabase.Content = (string)btnGenerateDatabase.Content + DemoString;
            pbPassword.Password = ServerParameters.Password;
            pbPassword.PasswordChanged += (s, e) => { ServerParameters.Password = pbPassword.Password; };
            Closed += (s, e) => {
                windowWasClosed = true;
                if(DatabaseHelper.IsGenerating)
                    DatabaseHelper.CancelDatabaseGenerationAsync();
                ServerParameters.SaveParameters();
                Mouse.OverrideCursor = null;
            };
        }
        protected string DemoString { get; set; }
        void rdWindowsAuthentication_Checked(object sender, RoutedEventArgs e) {
            if(tbLogin != null)
                tbLogin.IsEnabled = pbPassword.IsEnabled = false;
        }
        void rdSqlServerAuthentication_Checked(object sender, RoutedEventArgs e) {
            tbLogin.IsEnabled = pbPassword.IsEnabled = true;
        }
        void tbRecordsCount_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e) {
            int temp;
            if(!int.TryParse(tbRecordsCount.Text, out temp))
                tbRecordsCount.Text = "250000";
        }
        void btnExit_Click(object sender, RoutedEventArgs e) {
            DialogResult = DatabaseHelper.IsGenerating;
            Close();
        }
        void btnGenerateDatabase_Click(object sender, RoutedEventArgs e) {
            GenerateRecords();
        }
        void DisableControls() {
            btnGenerateDatabase.IsEnabled = false;
            btnTestConfiguration.IsEnabled = false;
            rdSqlServerAuthentication.IsEnabled = rdWindowsAuthentication.IsEnabled = false;
            tbLogin.IsEnabled = false;
            pbPassword.IsEnabled = false;
            tbServer.IsEnabled = false;
            tbRecordsCount.IsEnabled = false;
        }
        void GenerateRecords() {
            Mouse.OverrideCursor = Cursors.Wait;
            if(!DatabaseHelper.CreateDataLayer()) {
                Mouse.OverrideCursor = null;
                DXMessageBox.Show("Failed to connect to the server.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Title = "Generating records...";
            int rowsTotal = int.Parse(tbRecordsCount.Text);
            progressBar1.Maximum = rowsTotal;
            progressBar1.Value = 0;
            DisableControls();
            DatabaseHelper.GenerateDatabaseAsync(rowsTotal, UpdateProgress, OnDatabaseGenerated);
        }
        void UpdateProgress(int rowsGenerated) {
            progressBar1.Value = rowsGenerated;
            btnExit.Content = string.Format("{0} rows is enough. {1}", rowsGenerated, DemoString);
        }
        void OnDatabaseGenerated() {
            if(!windowWasClosed)
                DialogResult = true;
            Close();
        }
        void btnTestConfiguration_Click(object sender, RoutedEventArgs e) {
            lblRecordsCount.Content = string.Empty;
            int recordsCount = DatabaseHelper.CalculateRecordCount();
            if(recordsCount != -1)
                lblRecordsCount.Content = string.Format("Current record count = {0}", recordsCount);
            else
                DXMessageBox.Show(
                    string.Format("Unable to connect to the database. Make sure that connection parameters are correct or use the '{0}' button to generate a sample database.", btnGenerateDatabase.Content),
                    "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error
                );
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInverterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if(targetType != typeof(bool) && targetType != typeof(bool?))
                throw new InvalidOperationException("The target must be a boolean");
            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
