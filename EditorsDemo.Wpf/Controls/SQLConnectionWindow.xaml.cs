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
using System.Windows.Shapes;
using System.Xml;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Threading;
using System.Threading;
using GridDemo.Controls;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using DevExpress.DemoData.Helpers;
using System.Data.Linq;

namespace GridDemo {
    public static class ServerModeOptions {
        public static bool IsCorrectConnection;
        public static string SQLConnectionString = string.Empty;
        public static string failedConnection = "Failed to connect to the server.";
        public static string failedConnectionCaption = "Connection Error";
        public static string dataAdding = "Adding next portion of data...";
        public static string recordCount = "Current record count = {0}";
        static string descriptionSQLConnection =
            "This demo illustrates the capabilities of the {0} that are bound to a large amount of data. In this demo the {1} to be connected to a data table on an MS SQL server. Please use this Configuration Window to configure data and connection settings.\r\n\r\n" +
            "On the first run:\r\n" +
            "1) Specify the name of an existing SQL Server, which will contain the target database;\r\n" +
            "2) Specify the amount of records in the target table and click the \"Generate Table and Start Demo\" button. A new table will be generated and the demo will start with the {2} bound to the generated recordset.\r\n\r\n" +
            "On subsequent runs, you can add more records to the database or just start the demo without generating additional data.";
        public static string GetGridDescription() {
            return string.Format(descriptionSQLConnection, "DXGrid control", "DXGrid control needs", "DXGrid control");
        }
        public static string GetComboBoxDescription() {
            return string.Format(descriptionSQLConnection, "ComboBoxEdit, LookUpEdit and ListBoxEdit controls", "editors need", "editors");
        }
        public static string GetLookUpDescription() {
            return string.Format(descriptionSQLConnection, "LookUpEdit control", "LookUpEdit control needs", "LookUpEdit control");
        }
    }
    public partial class SQLConnectionWindow : DXWindow {
        string defaultDB = "DXGridServerModeDB";
        string serverParameters = "SQLParameters.xml";
        public string ConnectionStringParameters {
            get {
                return string.Format("{0};{1};{2};{3}", tbServer.Text, rbWindowsAuthentication.IsChecked.Value ? 0 : 1, tbLogin.Text, tbPassword.Text);
            }
        }
        public int RecordCount {
            get {
                int count = Convert.ToInt32(tbRecords.EditValue);
                return count >= 0 ? count : 0;
            }
        }
        public ProgressBar ProgressBar { get { return progressBarCore; } }
        public string Description {
            get { return tbDescription.Text; }
            set {
                tbDescription.Text = value;
            }
        }

        ServerModeRecordsGeneratorProviderBase generatorProvider;

        public SQLConnectionWindow(string demoString) : this(demoString, null) { }
        public SQLConnectionWindow(string demoString, ServerModeRecordsGeneratorProviderBase provider) {
            InitializeComponent();
            generatorProvider = provider ?? CreateDefaultProvider();
            tbServer.Text = DbEngineDetector.GetSqlServerInstanceName();
            rbWindowsAuthentication.Checked += new RoutedEventHandler(radioGroup1_SelectedIndexChanged);
            rbWindowsAuthentication.Unchecked += new RoutedEventHandler(radioGroup1_SelectedIndexChanged);
            cbDatabase.EditValueChanged += new EditValueChangedEventHandler(cbDatabase_EditValueChanged);
            tbServer.EditValueChanged += new EditValueChangedEventHandler(teServer_EditValueChanged);
            bGenerateDB.Click += new RoutedEventHandler(sbGenerateDB_Click);
            bAddRecords.Click += new RoutedEventHandler(sbAddRecords_Click);
            bExit.Click += new RoutedEventHandler(sbExit_Click);
            UpdateAuthenticationFields();
            UpdateGenerateDBButton();
            UpdateDatabaseTextEdit();

            cbDatabase.Text = defaultDB;
            bAddRecords.Content = (string)bAddRecords.Content + demoString;
            bGenerateDB.Content = (string)bGenerateDB.Content + demoString;
            bExit.Content = (string)bExit.Content + demoString;
            ShowParameters();
        }
        ServerModeRecordsGeneratorProviderBase CreateDefaultProvider() {
            return new OutlookGeneratorProvider();
        }

        void cbDatabase_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateGenerateDBButton();
        }

        void ShowParameters() {
            if(!System.IO.File.Exists(serverParameters))
                return;
            bExit.IsEnabled = true;
            XmlDocument doc = new XmlDocument();
            try {
                doc.Load(serverParameters);
            } catch { }
            if(doc.DocumentElement.Name == "Parameters") {
                string[] prm = doc.DocumentElement.InnerText.Split(new char[] { ';' });
                tbServer.Text = prm[0];
                rbWindowsAuthentication.IsChecked = Convert.ToInt32(prm[1]) == 0;
                rbSQLServerAuthentication.IsChecked = !rbWindowsAuthentication.IsChecked.Value;
                tbLogin.Text = prm[2];
                tbPassword.Text = prm[3];
            }
            CheckRecords();
        }
        void CheckRecords() {
            int num = generatorProvider.CalcRecordCount(GetDataBaseConnectionString());
            if(num > 0) {
                bAddRecords.IsEnabled = true;
                lRecords.Content = string.Format(ServerModeOptions.recordCount, num);
            }
        }
        public string GetDataBaseConnectionString() {
            string connectionString = GetServerConnectionString();
            return connectionString + ";initial catalog=" + cbDatabase.Text;
        }
        string GetServerConnectionString() {
            string connectionString = String.Format("data source={0};integrated security=SSPI", tbServer.Text);
            if(!rbWindowsAuthentication.IsChecked.Value)
                connectionString = String.Format("data source={0};user id={1};password={2}", tbServer.Text, tbLogin.Text, tbPassword.Text);
            return connectionString;
        }
        void radioGroup1_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            UpdateAuthenticationFields();
        }
        void UpdateAuthenticationFields() {
            bool disable = rbWindowsAuthentication.IsChecked.Value;
            DisableSQLServerAuthentication(disable);
        }
        void DisableSQLServerAuthentication(bool disable) {
            tbLogin.IsEnabled = !disable;
            tbPassword.IsEnabled = !disable;
        }
        void UpdateGenerateDBButton() {
            bGenerateDB.IsEnabled = cbDatabase.Text == defaultDB;
        }
        void teServer_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            UpdateDatabaseTextEdit();
        }
        private void UpdateDatabaseTextEdit() {
            cbDatabase.Text = defaultDB;
        }
        void sbGenerateDB_Click(object sender, RoutedEventArgs e) {
            GenerateRecords(true);
        }
        void sbAddRecords_Click(object sender, RoutedEventArgs e) {
            GenerateRecords(false);
        }
        void GenerateRecords(bool clearRecords) {
            if(!CorrectConnection(GetServerConnectionString()))
                return;
            generatorProvider.Create(this, clearRecords).GenerateRecords();
        }
        void sbExit_Click(object sender, RoutedEventArgs e) {
            CloseForm();
        }
        internal void CloseForm() {
            if(!CorrectConnection(GetServerConnectionString()))
                return;
            try {
                using(XmlTextWriter tw = new XmlTextWriter(serverParameters, System.Text.Encoding.UTF8)) {
                    tw.WriteElementString("Parameters", ConnectionStringParameters);
                }
            } catch { }
            this.Close();
        }
        bool CorrectConnection(string serverConnectionString) {
            Cursor cur = this.Cursor;
            this.Cursor = Cursors.Wait;
            using(SqlConnection connection = new SqlConnection(serverConnectionString)) {
                try {
                    connection.Open();
                    connection.Close();
                } catch {
                    MessageBox.Show(ServerModeOptions.failedConnection, ServerModeOptions.failedConnectionCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                    ServerModeOptions.IsCorrectConnection = false;
                    return false;
                    ;
                } finally {
                    this.Cursor = cur;
                }
            }
            ServerModeOptions.IsCorrectConnection = true;
            return true;
        }
    }

    public abstract class ServerModeRecordsGeneratorBase {
        int generatedItemsCount = 0;
        SQLConnectionWindow parentWindow;
        bool clearRecords;
        public ServerModeRecordsGeneratorBase(SQLConnectionWindow parentWindow, bool clearRecords) {
            this.parentWindow = parentWindow;
            this.clearRecords = clearRecords;
        }

        protected abstract DataContext DataBaseContext { get; }

        public void GenerateRecords() {
            parentWindow.Cursor = Cursors.Wait;
            SetupDataContext(parentWindow.GetDataBaseConnectionString());
            ValidateDatabase();
            parentWindow.Title = ServerModeOptions.dataAdding;
            if(clearRecords) {
                try {
                    Clear();
                    DataBaseContext.SubmitChanges();
                } catch { }
            }
            CreateItems();
        }
        void ValidateDatabase() {
            if(!DataBaseContext.DatabaseExists()) {
                DataBaseContext.CreateDatabase();
                CreateIndices();
            } else
                DataBaseContext.Connection.Open();
            if(!TableExists()) {
                CreateTable();
                CreateIndices();
            }
        }
        bool TableExists() {
            DataTable schema = DataBaseContext.Connection.GetSchema("Tables");
            foreach(DataRowView row in schema.DefaultView) {
                if((string)row["TABLE_NAME"] == TableName)
                    return true;
            }
            return false;
        }
        void CreateItems() {
            int step = (int)(parentWindow.RecordCount / 100);
            if(step == 0)
                step = parentWindow.RecordCount;
            int i;
            for(i = generatedItemsCount; i < parentWindow.RecordCount && i < generatedItemsCount + 100; i++) {
                AddNewItem();
                if(i % step == 0) {
                    DataBaseContext.SubmitChanges();
                    parentWindow.ProgressBar.Value = (i * 100 / parentWindow.RecordCount);
                    parentWindow.ProgressBar.UpdateLayout();
                }
            }
            generatedItemsCount = i;
            if(i >= parentWindow.RecordCount) {
                DataBaseContext.SubmitChanges();
                DataBaseContext.Dispose();
                parentWindow.CloseForm();
            } else {
                parentWindow.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(CreateItems));
            }
        }

        protected abstract void SetupDataContext(string connection);
        protected abstract void AddNewItem();
        protected abstract string TableName { get; }
        protected virtual void CreateIndices() { }
        protected abstract void CreateTable();
        protected abstract void Clear();
    }
    public class ServerModeOutlookDataGenerator : ServerModeRecordsGeneratorBase {
        public ServerModeOutlookDataGenerator(SQLConnectionWindow parentWindow, bool clearRecords) : base(parentWindow, clearRecords) { }

        DataGridTestClassesDataContext context;

        protected override DataContext DataBaseContext { get { return context; } }

        protected override void SetupDataContext(string connection) {
            context = CreateContext(connection);
        }
        DataGridTestClassesDataContext CreateContext(string connection) {
            return new DataGridTestClassesDataContext(connection);
        }

        protected override void AddNewItem() {
            context.WpfServerSideGridTests.InsertOnSubmit(OutlookDataGenerator.CreateNewServerObject());
        }

        protected override string TableName { get { return typeof(WpfServerSideGridTest).Name; } }

        protected override void CreateIndices() {
            CreateIndex("Sent");
            CreateIndex("HasAttachment");
            CreateIndex("Size");
            CreateIndex("From");
            CreateIndex("Subject");
            context.SubmitChanges();
        }
        void CreateIndex(string fieldName) {
            DataBaseContext.ExecuteCommand(string.Format(@"CREATE INDEX {1}_idx ON {0} ([{1}]);", TableName, fieldName));
        }
        protected override void CreateTable() {
            DataBaseContext.ExecuteCommand(string.Format(@"
        CREATE TABLE {0}(
        OID Int NOT NULL IDENTITY,
        Sent DateTime,
        HasAttachment Bit,
        Size BigInt,
        [From] NVarChar(100),
        Subject NVarChar(100),
        CONSTRAINT {0}_pk PRIMARY KEY (OID)
        );
        ", TableName));
        }

        protected override void Clear() {
            DataBaseContext.ExecuteCommand("DELETE FROM " + TableName + " WHERE Oid>=0");
        }
    }

    public abstract class ServerModeRecordsGeneratorProviderBase {
        public abstract int CalcRecordCount(string serverConnectionString);
        public abstract ServerModeRecordsGeneratorBase Create(SQLConnectionWindow parentWindow, bool clearRecords);

        protected int CalcRecordCountCore<T, U>(Func<T> dataContextFactory, Func<T, IQueryable<U>> tableSelector) where T : DataContext {
            try {
                using(var dataContext = dataContextFactory()) {
                    return Queryable.Count(tableSelector(dataContext));
                }
            } catch {
                return 0;
            }
        }
    }
    public class OutlookGeneratorProvider : ServerModeRecordsGeneratorProviderBase {
        public override ServerModeRecordsGeneratorBase Create(SQLConnectionWindow parentWindow, bool clearRecords) {
            return new ServerModeOutlookDataGenerator(parentWindow, clearRecords);
        }

        public override int CalcRecordCount(string serverConnectionString) {
            return CalcRecordCountCore(() => new DataGridTestClassesDataContext(serverConnectionString), x => x.WpfServerSideGridTests);
        }
    }
}
