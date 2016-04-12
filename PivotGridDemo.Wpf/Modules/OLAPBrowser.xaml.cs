using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo.PivotGrid {
    public partial class OLAPBrowser : PivotGridDemoModule {

        const string YearFieldName = "[Date].[Calendar].[Calendar Year]";
        const string CategoryFieldName = "[Product].[Product].[Product]";
        const string TotalCostFieldName = "[Measures].[Total Product Cost]";
        const string FreightFieldName = "[Measures].[Freight Cost]";
        const string QuantityOrderFieldName = "[Measures].[Order Quantity]";
        protected const int DefaultFieldWidth = 90;
        double lastSplitterY = 200;

        static string GetSampleCubeFileName() {
            return "AdventureWorks.cub";
        }
        static string sampleFileName;
        readonly int DataSourceDialohHeight = 230;
        string SampleConnectionString {
            get { return "Provider=msolap;Data Source=" + SampleFileName + ";Initial Catalog=Adventure Works;Cube Name=Adventure Works"; }
        }
        protected static string SampleFileName {
            get {
                if(string.IsNullOrEmpty(sampleFileName)) {
                    sampleFileName = Path.GetFullPath(DataFilesHelper.FindFile(GetSampleCubeFileName(), DataFilesHelper.DataPath));
                    if(File.Exists(sampleFileName))
                        File.SetAttributes(sampleFileName, FileAttributes.Normal);
                }
                return sampleFileName;
            }
        }

        string PivotConnectionString() {
            return pivotGrid.OlapConnectionString;
        }
        bool IsSampleCube() {
            return pivotGrid.OlapConnectionString.Contains("Cube Name=Adventure Works");
        }
        static OLAPBrowser() {
            Type ownerType = typeof(OLAPBrowser);
        }

        public OLAPBrowser() {
            InitializeComponent();
        }

        void OnPivotGridLoaded(object sender, RoutedEventArgs e) {
            InitPivotGrid(SampleConnectionString);
        }

        void OnPivotGridSizeChanged(object sender, SizeChangedEventArgs e) {
            PivotGridControl pivot = sender as PivotGridControl;
            if(pivot == null || pivot.RenderSize.Height < 200 || lastSplitterY != pivot.FieldListSplitterY)
                return;
            pivot.FieldListSplitterY = Math.Round((pivot.RenderSize.Height - 90) * 0.5);
            lastSplitterY = pivot.FieldListSplitterY;
        }

        void InitPivotLayoutSampleOlapDB(AsyncOperationResult result) {
            if(pivotGrid.Fields.Count == 0 || !IsSampleCube()) return;
            pivotGrid.BeginUpdate();
            PivotGridField fieldProduct = pivotGrid.Fields[CategoryFieldName],
                fieldYear = pivotGrid.Fields[YearFieldName],
                fieldTotalCost = pivotGrid.Fields[TotalCostFieldName],
                fieldFreightCost = pivotGrid.Fields[FreightFieldName],
                fieldOrderQuantity = pivotGrid.Fields[QuantityOrderFieldName];
            if(fieldProduct == null ||
                fieldYear == null ||
                fieldTotalCost == null ||
                fieldFreightCost == null ||
                fieldOrderQuantity == null) {
                pivotGrid.EndUpdateAsync();
                return;
            }
            fieldProduct.Area = FieldArea.RowArea;
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.SortOrder = FieldSortOrder.Descending;
            fieldTotalCost.Width = DefaultFieldWidth + 20;
            fieldTotalCost.CellFormat = "c2";
            fieldFreightCost.Width = DefaultFieldWidth;
            fieldFreightCost.CellFormat = "c2";
            fieldOrderQuantity.Width = DefaultFieldWidth + 5;
            fieldProduct.Visible = true;
            fieldYear.Visible = true;
            fieldTotalCost.Visible = true;
            fieldFreightCost.Visible = true;
            fieldOrderQuantity.Visible = true;
            pivotGrid.EndUpdateAsync();
        }

        void InitPivotGrid(string connectionString) {
            if(string.IsNullOrWhiteSpace(connectionString)) {
                pivotGrid.DataSource = null;
                return;
            }
            if(PivotConnectionString() == connectionString) return;
            pivotGrid.BeginUpdate();
            pivotGrid.Fields.Clear();
            pivotGrid.Groups.Clear();
            pivotGrid.OlapConnectionString = connectionString;
            pivotGrid.EndUpdateAsync(delegate(AsyncOperationResult result) {
                if(pivotGrid.Fields.Count == 0)
                    pivotGrid.RetrieveFieldsAsync(FieldArea.FilterArea, false, InitPivotLayoutSampleOlapDB);
            });
        }

        DataSourceDialog dialog;
        void OnShowConnectionClick(object sender, RoutedEventArgs e) {
            if(pivotGrid == null || pivotGrid.IsAsyncInProgress)
                return;
            errorBorder.Visibility = System.Windows.Visibility.Collapsed;
            dialog = new DataSourceDialog();
            dialog.Style = (Style)ResourceHelper.FindResource(this, "DataSourceDialogStyle");
            FloatingContainerParameters pars = new FloatingContainerParameters();
            pars.AllowSizing = false;
            pars.CloseOnEscape = true;
            pars.Title = "OLAP Connection";
            pars.ClosedDelegate = DialogClosed;
            FloatingContainer.ShowDialogContent(dialog, this, new Size(600, DataSourceDialohHeight), pars);
        }

        void DialogClosed(bool? close) {
            Application.Current.MainWindow.Activate();
            if(dialog == null)
                return;
            String connectionString = dialog.GetConnectionString();
            dialog = null;
            if(close != true)
                return;
            if(string.IsNullOrWhiteSpace(connectionString)) {
                return;
            }
            InitPivotGrid(connectionString);

        }

        private void pivotGrid_CellDoubleClick(object sender, PivotCellEventArgs e) {
            AsyncCompletedHandler showDrillDownHandler = delegate(AsyncOperationResult result) {
                try {
                    if(result.Exception != null)
                        ShowMessageBox(result.Exception.Message);
                    else
                        ShowDrillDown((PivotDrillDownDataSource)result.Value);
                } catch(Exception ex) {
                    ShowMessageBox(ex.Message);
                }
            };
            pivotGrid.CreateDrillDownDataSourceAsync(e.ColumnIndex, e.RowIndex, showDrillDownHandler);
        }

        void ShowDrillDown(PivotDrillDownDataSource drillDownDataSource) {
            if(drillDownDataSource.RowCount == 0) {
                ShowMessageBox("DrillDown operation returned no rows");
                return;
            }
            GridControl grid = new GridControl();
            grid.View = new TableView() { AllowPerPixelScrolling = true, ShowGroupPanel = false };
            grid.ItemsSource = drillDownDataSource;
            grid.PopulateColumns();
            grid.ShowBorder = false;
            FloatingWindowContainer.ShowDialogContent(grid, this, new Size(520, 300),
             new FloatingContainerParameters() {
                 AllowSizing = true,
                 CloseOnEscape = true,
                 Title = String.Format("Drill Down Results: {0} Rows", drillDownDataSource.RowCount),
                 ClosedDelegate = null,
             });
        }

        void ShowMessageBox(string message) {
            FloatingContainerParameters pars = new FloatingContainerParameters();
            pars.AllowSizing = false;
            pars.Title = "Error";
            TextBlock text = new TextBlock();
            text.Text = message;
            text.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            text.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            FloatingContainer.ShowDialogContent(text, this, new Size(420, 150), pars);
        }

        void OnPivotGridOlapException(object sender, PivotOlapExceptionEventArgs e) {
            e.Handled = true;
            pivotGrid.Dispatcher.BeginInvoke(
                new Action(delegate() {
                ShowOLAPErrorMessage(e);
            })
               );
        }

        void ShowOLAPErrorMessage(PivotOlapExceptionEventArgs e) {
            errorText.Text = (e.Exception.Message + "\r\n " + ((e.Exception.InnerException != null) ? e.Exception.InnerException.Message : string.Empty));
            VisualStateManager.GoToState(this, "ShowErrorMessage", true);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += OnTimerTick;
            timer.Start();
            e.Handled = true;
        }

        void OnTimerTick(object sender1, EventArgs e1) {
            DispatcherTimer self = sender1 as DispatcherTimer;
            if(self == null)
                return;
            self.Stop();
            self.Tick -= OnTimerTick;
            VisualStateManager.GoToState(this, "HideErrorMessage", true);
        }
    }
}
