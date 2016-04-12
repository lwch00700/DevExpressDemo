using System;
using System.Data;
using System.IO;
using System.Windows;
using DevExpress.DemoData.Helpers;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.PivotGrid;
using DevExpress.XtraPivotGrid.Data;

namespace PivotGridDemo.PivotGrid.Helpers {
    public enum InitializerDataSource {
        OlapCube,
        TableDataSource,
    }
    public abstract class AsynchronousPivotInitializer {
        static string adventureWorksFileName;
        protected const int DefaultFieldWidth = 90;

        public static void Initialize(PivotGridControl pivot, AsyncCompletedHandler initCompletedCallback, InitializerDataSource dataSource) {
            AsynchronousPivotInitializer initializer;
            switch(dataSource) {
                case InitializerDataSource.OlapCube:
                    initializer = new AsynchronousPivotInitializerSampleOlapDB(pivot, initCompletedCallback);
                    initializer.Initialize();
                    break;
                case InitializerDataSource.TableDataSource:
                    initializer = new AsynchronousPivotInitializerGeneratedDB(pivot, initCompletedCallback);
                    initializer.Initialize();
                    break;
                default:
                    throw new ArgumentException("Incorrect InitializerDataSource enum value.", "dataSource");
            }
        }
        protected AsynchronousPivotInitializer(PivotGridControl pivot, AsyncCompletedHandler initCompletedCallback) {
            PivotGrid = pivot;
            ResetPivotGrid(PivotGrid);
            InitializationCompletedCallback = initCompletedCallback;
        }
        protected PivotGridControl PivotGrid { get; set; }
        protected AsyncCompletedHandler InitializationCompletedCallback { get; set; }
        static string AdventureWorksSampleFileName {
            get {
                if(string.IsNullOrEmpty(adventureWorksFileName)) {
                    adventureWorksFileName = Path.GetFullPath(DataFilesHelper.FindFile(GetAdventureWorksSampleFileName(), DataFilesHelper.DataPath));
                    if(File.Exists(adventureWorksFileName))
                        File.SetAttributes(adventureWorksFileName, FileAttributes.Normal);
                }
                return adventureWorksFileName;
            }
        }
        static string GetAdventureWorksSampleFileName() {
            return "AdventureWorks.cub";
        }
        protected static string OLAPSampleConnectionString {
            get {
                if(string.IsNullOrEmpty(AdventureWorksSampleFileName)) return null;
                return "Provider=msolap;Data Source=" + AdventureWorksSampleFileName + ";Initial Catalog=Adventure Works;Cube Name=Adventure Works";
            }
        }
        static void ResetPivotGrid(PivotGridControl pivot) {
            if(string.IsNullOrEmpty(pivot.OlapConnectionString))
                pivot.Groups.Clear();
            pivot.Fields.Clear();
            pivot.DataSource = null;
        }
        protected abstract void Initialize();
    }

    class AsynchronousPivotInitializerGeneratedDB : AsynchronousPivotInitializer {
        public AsynchronousPivotInitializerGeneratedDB(PivotGridControl pivot, AsyncCompletedHandler initCompletedCallback)
            : base(pivot, initCompletedCallback) {
        }
        protected override void Initialize() {
            DatabaseHelper.GetDataSetAsync(DatabaseHelper.AsyncDataSetSql, SetDataSourceGeneratedDatabase);
        }
        void SetDataSourceGeneratedDatabase(DataSet dataSet) {
            if(dataSet == null) {
                PivotGrid.IsEnabled = false;
                InitializationCompletedCallback.Invoke(null);
                return;
            }
            PivotGrid.SetDataSourceAsync(dataSet.Tables[0], InitPivotLayoutGeneratedDB);
        }
        void InitPivotLayoutGeneratedDB(AsyncOperationResult result) {
            PivotGrid.BeginUpdate();
            PivotGridField fieldSalesPerson = CreatePivotGridField("SalesPersonName", "Sales Person", FieldArea.RowArea);
            PivotGridField fieldCustomer = CreatePivotGridField("CustomerName", "Customer", FieldArea.FilterArea);
            PivotGridField fieldQuantity = CreatePivotGridField("Quantity", "Quantity", FieldArea.DataArea);
            PivotGridField fieldUnitPrice = CreatePivotGridField("UnitPrice", "Unit Price", FieldArea.DataArea);
            fieldUnitPrice.SummaryType = FieldSummaryType.Average;
            fieldUnitPrice.ShowSummaryTypeName = true;
            PivotGridField fieldOrderID = CreatePivotGridField("OrderID", "Order ID", FieldArea.FilterArea);
            PivotGridField fieldCategory = CreatePivotGridField("CategoryName", "Category", FieldArea.RowArea);
            PivotGridField fieldProduct = CreatePivotGridField("ProductName", "Product", FieldArea.RowArea);
            PivotGridField fieldOrderDateYear = CreatePivotGridField("OrderDate", "Year", FieldArea.ColumnArea);
            fieldOrderDateYear.GroupInterval = FieldGroupInterval.DateYear;
            PivotGridField fieldOrderDateMonth = CreatePivotGridField("OrderDate", "Month", FieldArea.ColumnArea);
            fieldOrderDateMonth.GroupInterval = FieldGroupInterval.DateMonth;
            PivotGridField fieldPrice = new PivotGridField();
            fieldPrice.Area = FieldArea.DataArea;
            fieldPrice.Caption = "Price";
            fieldPrice.UnboundType = FieldUnboundColumnType.Decimal;

            fieldQuantity.Width = DefaultFieldWidth;
            fieldUnitPrice.Width = DefaultFieldWidth;
            fieldPrice.Width = DefaultFieldWidth;

            PivotGrid.Fields.AddRange(new PivotGridField[] { fieldSalesPerson, fieldCustomer, fieldQuantity, fieldUnitPrice,
                                fieldOrderID, fieldCategory, fieldProduct, fieldOrderDateYear, fieldOrderDateMonth, fieldPrice });
            PivotGrid.Groups.Add(fieldOrderDateYear, fieldOrderDateMonth);

            fieldPrice.UnboundExpression = "[" + fieldQuantity.ExpressionFieldName + "] * [" + fieldUnitPrice.ExpressionFieldName + "]";
            PivotGrid.EndUpdateAsync(InitializationCompletedCallback);
        }
        PivotGridField CreatePivotGridField(string fieldName, string caption, FieldArea area) {
            PivotGridField field = new PivotGridField(fieldName, area);
            field.Caption = caption;
            return field;
        }
    }

    class AsynchronousPivotInitializerSampleOlapDB : AsynchronousPivotInitializer {
        const string YearFieldName = "[Date].[Calendar].[Calendar Year]";
        const string CategoryFieldName = "[Product].[Product Categories].[Category]";
        const string TotalCostFieldName = "[Measures].[Total Product Cost]";
        const string FreightFieldName = "[Measures].[Freight Cost]";
        const string QuantityOrderFieldName = "[Measures].[Order Quantity]";

        public AsynchronousPivotInitializerSampleOlapDB(PivotGridControl pivot, AsyncCompletedHandler initCompletedCallback)
            : base(pivot, initCompletedCallback) {
        }
        protected override void Initialize() {
            try {
                PivotGrid.SetOlapConnectionStringAsync(OLAPSampleConnectionString, InitPivotLayoutSampleOlapDB);
            } catch(OLAPConnectionException) {
                PivotGrid.OlapConnectionString = null;
                PivotGrid.IsEnabled = false;
                InitializationCompletedCallback.Invoke(null);
            }
        }
        void InitPivotLayoutSampleOlapDB(AsyncOperationResult result) {
            PivotGrid.RetrieveFields(FieldArea.FilterArea, false);
            if(PivotGrid.Fields.Count == 0) {
                InitializationCompletedCallback.Invoke(null);
                return;
            }
            PivotGrid.BeginUpdate();
            PivotGridField fieldProduct = PivotGrid.Fields[CategoryFieldName],
                fieldYear = PivotGrid.Fields[YearFieldName],
                fieldTotalCost = PivotGrid.Fields[TotalCostFieldName],
                fieldFreightCost = PivotGrid.Fields[FreightFieldName],
                fieldOrderQuantity = PivotGrid.Fields[QuantityOrderFieldName];
            PivotGrid.Groups[1].Caption = "Calendar";
            PivotGrid.Groups[2].Caption = "Products";

            fieldProduct.Area = FieldArea.RowArea;
            fieldYear.Area = FieldArea.ColumnArea;
            fieldYear.SortOrder = FieldSortOrder.Descending;
            fieldTotalCost.Width = DefaultFieldWidth;
            fieldTotalCost.CellFormat = "c2";
            fieldFreightCost.Width = DefaultFieldWidth;
            fieldFreightCost.CellFormat = "c2";
            fieldOrderQuantity.Width = DefaultFieldWidth;

            fieldProduct.Visible = true;
            fieldYear.Visible = true;
            fieldTotalCost.Visible = true;
            fieldFreightCost.Visible = true;
            fieldOrderQuantity.Visible = true;

            PivotGrid.EndUpdateAsync(ExpandAll);
        }
        void ExpandAll(AsyncOperationResult result) {
            PivotGrid.ExpandAllAsync(InitializationCompletedCallback);
        }
    }
}
