using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotTableActions {

        static void CreatePivotTableFromRange(IWorkbook workbook) {
            #region #Create from Range
            Worksheet sourceWorksheet = workbook.Worksheets["Data1"];
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables.Add(sourceWorksheet["A1:D41"], worksheet["B2"]);
            pivotTable.RowFields.Add(pivotTable.Fields["Category"]);
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            pivotTable.DataFields.Add(pivotTable.Fields["Sales"]);
            pivotTable.Style = workbook.TableStyles.DefaultPivotStyle;

            #endregion #Create from Range
        }

        static void CreatePivotTableFromCache(IWorkbook workbook) {
            #region #Create from PivotCache
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotCache cache = workbook.Worksheets["Report1"].PivotTables["PivotTable1"].Cache;
            PivotTable pivotTable = worksheet.PivotTables.Add(cache, worksheet["B2"]);
            pivotTable.RowFields.Add(pivotTable.Fields["Category"]);
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            pivotTable.DataFields.Add(pivotTable.Fields["Sales"]);
            pivotTable.Style = workbook.TableStyles.DefaultPivotStyle;

            #endregion #Create from PivotCache
        }

        static void RemovePivotTable(IWorkbook workbook) {
            #region #Remove Table
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            worksheet.PivotTables.Remove(pivotTable);

            #endregion #Remove Table
        }
        static void ChangePivotTableLocation(IWorkbook workbook) {
            #region #Change Location
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.PivotTables["PivotTable1"].MoveTo(worksheet["A7"]);

            #endregion #Change Location
        }
        static void MovePivotTableToWorksheet(IWorkbook workbook) {
            #region #Move to Worksheet
            Worksheet worksheet = workbook.Worksheets["Report1"];
            Worksheet targetWorksheet = workbook.Worksheets.Add();
            worksheet.PivotTables["PivotTable1"].MoveTo(targetWorksheet["B2"]);

            workbook.Worksheets.ActiveWorksheet = targetWorksheet;

            #endregion #Move to Worksheet
        }

        static void ChangePivotTableDataSource(IWorkbook workbook) {
            #region #Change DataSource
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            Worksheet sourceWorksheet = workbook.Worksheets["Data2"];
            pivotTable.ChangeDataSource(sourceWorksheet["A1:H6367"]);
            pivotTable.RowFields.Add(pivotTable.Fields["State"]);
            PivotDataField dataField = pivotTable.DataFields.Add(pivotTable.Fields["Yearly Earnings"]);
            dataField.SummarizeValuesBy = PivotDataConsolidationFunction.Average;

            #endregion #Change DataSource
        }

        static void ClearPivotTable(IWorkbook workbook) {
            #region #Clear Table
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.PivotTables["PivotTable1"].Clear();

            #endregion #Clear Table
        }

        static void ChangeBehaviorOptions(IWorkbook workbook) {
            #region #Change Behavior Options
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            worksheet.Columns["B"].WidthInCharacters = 40;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotBehaviorOptions behaviorOptions = pivotTable.Behavior;
            behaviorOptions.AutoFitColumns = false;
            behaviorOptions.EnableFieldList = false;
            pivotTable.Cache.Refresh();

            #endregion #Change Behavior Options
        }
    }
}
