using System.Data;
using System.Diagnostics;
using System.Windows;
using DevExpress.Data.Linq;
using DevExpress.Xpf.Core;
using PivotGridDemo.Controls;
using PivotGridDemo.PivotGrid.Helpers;
using System;
using DevExpress.Xpf.Core.ServerMode;

namespace PivotGridDemo.PivotGrid {
    public partial class ServerMode : PivotGridDemoModule {
        static bool IsLoad;
        readonly NoDataState stateNoData;
        readonly Linq2SqlState stateLinq2Sql;
        readonly Stopwatch timer = new Stopwatch();
        DemoState currentState;

        public bool InteractiveControlsEnabled {
            set {
                pivotGrid.IsEnabled = value;
            }
        }
        DemoState State {
            get { return currentState; }
            set {
                if(currentState == value)
                    return;
                if(currentState != null) {
                    currentState.Leave();
                    currentState = null;
                }
                if(!value.Enter()) {
                    stateNoData.Enter();
                    currentState = stateNoData;
                    RunDBGenerator(stateLinq2Sql);
                }
                else
                    currentState = value;
            }
        }

        public ServerMode() {
            InitializeComponent();

            stateNoData = new NoDataState(this);
            stateLinq2Sql = new Linq2SqlState(this);

            State = stateNoData;
        }

        void RunDBGenerator(DemoState nextState) {
            WindowDatabaseGenerator windowDBGenerator = new WindowDatabaseGenerator(this, State.DBGeneratorString);
            if(windowDBGenerator.ShowDialog() == true) {
                if(State == nextState)
                    pivotGrid.ReloadDataAsync();
                else
                    State = nextState;
            }
        }

        private void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            IsLoad = true;
            State = stateLinq2Sql;
            IsLoad = false;
        }

        private void btnGenerateTableData_Click(object sender, RoutedEventArgs e) {
            RunDBGenerator(State);
        }

        abstract class DemoState {
            protected ServerMode demo;

            public abstract string DBGeneratorString { get; }

            public DemoState(ServerMode demo) {
                this.demo = demo;
            }
            public abstract bool Enter();
            public abstract void Leave();
        }

        class NoDataState : DemoState {
            public override string DBGeneratorString { get { return "Start Demo"; } }

            public NoDataState(ServerMode demo)
                : base(demo) {
            }

            public override bool Enter() {
                demo.InteractiveControlsEnabled = false;
                demo.btnGenerateTableData.IsEnabled = true;
                return true;
            }
            public override void Leave() {
            }
        }

        abstract class DataState : DemoState {
            public override string DBGeneratorString { get { return "Return to demo"; } }

            public DataState(ServerMode demo)
                : base(demo) {
            }

            protected abstract bool LoadData();
            protected abstract void CleanData();

            public override bool Enter() {
                demo.InteractiveControlsEnabled = false;
                demo.btnGenerateTableData.IsEnabled = false;
                bool result = LoadData();
                demo.btnGenerateTableData.IsEnabled = true;
                if(result) {
                    demo.InteractiveControlsEnabled = true;
                }
                else {
                    if(!IsLoad)
                        DXMessageBox.Show("Failed to load data. Failed to load data. Use the Generate Data Table button to check the connection parameters or generate a new sample database.", "Server Mode Demo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return result;
            }

            public override void Leave() {
                CleanData();
            }
        }

        class Linq2SqlState : DataState {
            PivotGridDemoDBDataContext dataContext;

            public Linq2SqlState(ServerMode demo)
                : base(demo) {
            }
            protected override bool LoadData() {
                dataContext = DatabaseHelper.GetContext();
                if(dataContext == null)
                    return false;
                try {
                    LinqServerModeDataSource dataSource = new LinqServerModeDataSource();
                    dataSource.QueryableSource = dataContext.Sales;
                    demo.pivotGrid.SetDataSourceAsync(dataSource);
                }
                catch {
                    return false;
                }
                return true;
            }
            protected override void CleanData() {
                demo.pivotGrid.DataSource = null;
                if(dataContext != null) {
                    dataContext.Dispose();
                    dataContext = null;
                }
            }
        }

        void pivotGrid_AsyncOperationStarting(object sender, RoutedEventArgs e) {
            tbTimeTaken.Text = "...";
            timer.Restart();
        }

        void pivotGrid_AsyncOperationCompleted(object sender, RoutedEventArgs e) {
            timer.Stop();
            tbTimeTaken.Text = timer.ElapsedMilliseconds.ToString();
        }
    }
}
