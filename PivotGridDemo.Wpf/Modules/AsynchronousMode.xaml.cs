using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PivotGrid;
using PivotGridDemo.PivotGrid.Helpers;
using System.Diagnostics;

namespace PivotGridDemo.PivotGrid {
    public partial class AsynchronousMode : PivotGridDemoModule {
        const string TableConnectError = "A connection error occurred. Please make sure that you have generated a data source and provided proper connection settings.";
        const string OlapConnectError = "A connection error occurred. Please make sure that you have provided proper connection settings.\nTo connect to OLAP cubes, you should have Microsoft SQL Server 2005 Analysis Services 9.0 OLE DB provider installed on your system.";
        bool isDataBaseGeneratorRunned = false;
        InitializerDataSource currentDataSource;
        double lastSplitterY = 200;

        public AsynchronousMode() {
            InitializeComponent();
        }
        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            ServerParameters.LoadParameters();
            radioOlapCube.IsChecked = true;
        }
        void pivotGrid_AsyncOperationCompleted(object sender, RoutedEventArgs e) {
            EnableControls();
        }
        void pivotGrid_AsyncOperationStarting(object sender, RoutedEventArgs e) {
            DisableControls();
        }
        void btnGenerateTableData_Click(object sender, RoutedEventArgs e) {
            RunDBGenerator("Return to Demo");
        }
        void RunDBGenerator(string demoString) {
            WindowDatabaseGenerator windowDBGenerator = new WindowDatabaseGenerator(this, demoString);
            bool? cancelInitialization = windowDBGenerator.ShowDialog();
            if(cancelInitialization == false && radioTableDataSource.IsChecked.Value) {
                DisableControls();
                isDataBaseGeneratorRunned = true;
                Initialize(InitializerDataSource.TableDataSource);
            }
        }
        void radioOlapCube_Checked(object sender, RoutedEventArgs e) {
            DisableControls();
            Initialize(InitializerDataSource.OlapCube);
        }
        void radioTableDataSource_Checked(object sender, RoutedEventArgs e) {
            DisableControls();
            if(isDataBaseGeneratorRunned)
                Initialize(InitializerDataSource.TableDataSource);
            else {
                RunDBGenerator("Start Demo");
                EnableControls();
            }
        }
        void Initialize(InitializerDataSource dataSourceType) {
            currentDataSource = dataSourceType;
            AsynchronousPivotInitializer.Initialize(pivotGrid, ProcessAsyncResult, currentDataSource);
        }
        void DisableControls() {
            radioTableDataSource.IsEnabled = false;
            radioOlapCube.IsEnabled = false;
            btnGenerateTableData.IsEnabled = false;
            DispatcherHelper.DoEvents();
        }
        void EnableControls() {
            radioTableDataSource.IsEnabled = true;
            radioOlapCube.IsEnabled = true;
            btnGenerateTableData.IsEnabled = true;
        }
        void ShowErrorInfo() {
            if(currentDataSource == InitializerDataSource.OlapCube) {
                tbOlap.Visibility = System.Windows.Visibility.Visible;
                DXMessageBox.Show(OlapConnectError, "Could not connect to OLAP cube", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
                DXMessageBox.Show(TableConnectError, "Could not connect to data source", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void ProcessAsyncResult(AsyncOperationResult result) {
            if(result == null) {
                if(currentDataSource == InitializerDataSource.OlapCube || isDataBaseGeneratorRunned)
                    ShowErrorInfo();
                else
                    RunDBGenerator("Start Demo");
            }
            EnableControls();
        }
        void pivotGrid_CellDoubleClick(object sender, PivotCellEventArgs e) {
            AsyncCompletedHandler showDrillDownHandler = delegate(AsyncOperationResult result) {
                try {
                    if(result.Exception != null)
                        DXMessageBox.Show(result.Exception.Message);
                    else
                        ShowDrillDown((PivotDrillDownDataSource)result.Value);
                } catch(Exception ex) {
                    DXMessageBox.Show(ex.Message);
                }
            };
            pivotGrid.CreateDrillDownDataSourceAsync(e.ColumnIndex, e.RowIndex, showDrillDownHandler);
        }
        void ShowDrillDown(PivotDrillDownDataSource drillDownDataSource) {
            if(drillDownDataSource.RowCount == 0) {
                DXMessageBox.Show("DrillDown operation returned no rows");
                return;
            }
            GridControl grid = new GridControl();
            grid.View = new TableView() { AllowPerPixelScrolling = true };
            grid.ItemsSource = drillDownDataSource;
            grid.PopulateColumns();

            FloatingContainer popupContainer = FloatingWindowContainer.ShowDialog(grid, this, new Size(520, 300),
             new FloatingContainerParameters() {
                 AllowSizing = true,
                 CloseOnEscape = true,
                 Title = String.Format("Drill Down Results: {0} Rows", drillDownDataSource.RowCount),
                 ClosedDelegate = null,
             });
            AddLogicalChild(popupContainer);
        }

        void HyperlinkOlap_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        void OnPivotGridSizeChanged(object sender, SizeChangedEventArgs e) {
            PivotGridControl pivot = sender as PivotGridControl;
            if(pivot == null || pivot.RenderSize.Height < 200 || lastSplitterY != pivot.FieldListSplitterY)
                return;
            pivot.FieldListSplitterY = Math.Round((pivot.RenderSize.Height - 90) * 0.5);
            lastSplitterY = pivot.FieldListSplitterY;
        }

        void OnPivotGridOlapException(object sender, PivotOlapExceptionEventArgs e) {
            e.Handled = true;
        }
    }
}
