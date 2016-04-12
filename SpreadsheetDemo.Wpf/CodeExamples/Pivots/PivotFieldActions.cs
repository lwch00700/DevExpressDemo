using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotFieldActions {

        static void AddFieldToAxis(IWorkbook workbook) {
            #region #Add to Axis
            Worksheet sourceWorksheet = workbook.Worksheets["Data1"];
            Worksheet worksheet = workbook.Worksheets.Add();
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables.Add(sourceWorksheet["A1:D41"], worksheet["B2"]);
            pivotTable.RowFields.Add(pivotTable.Fields["Product"]);
            pivotTable.ColumnFields.Add(pivotTable.Fields["Category"]);
            PivotDataField dataField = pivotTable.DataFields.Add(pivotTable.Fields["Sales"], "Sales(Sum)");
            dataField.NumberFormat = @"_([$$-409]* #,##0.00_);_([$$-409]* (#,##0.00);_([$$-409]* "" - ""??_);_(@_)";
            pivotTable.PageFields.Add(pivotTable.Fields["Region"]);

            #endregion #Add to Axis
        }

        static void InsertFieldToAxis(IWorkbook workbook) {
            #region #Insert into Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.RowFields.Insert(0, pivotTable.Fields["Region"]);

            #endregion #Insert into Axis
        }

        static void MoveFieldToAxis(IWorkbook workbook) {
            #region #Move to Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.ColumnFields.Add(pivotTable.Fields["Region"]);

            #endregion #Move to Axis
        }

        static void MoveFieldUp(IWorkbook workbook) {
            #region #Move Up
            Worksheet worksheet = workbook.Worksheets["Report3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.RowFields["Category"].MoveUp();

            #endregion #Move Up
        }

        static void MoveFieldDown(IWorkbook workbook) {
            #region #Move Down
            Worksheet worksheet = workbook.Worksheets["Report3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.RowFields["Region"].MoveDown();

            #endregion #Move Down
        }

        static void RemoveFieldFromAxis(IWorkbook workbook) {
            #region #Remove from Axis
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.RowFields.Remove(pivotTable.RowFields["Product"]);

            #endregion #Remove from Axis
        }

    }
}
