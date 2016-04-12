using System;
using System.Collections.Generic;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotFormattingActions {

        static void ChangeStylePivotTable(IWorkbook workbook) {
            #region #Set Style
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Style = workbook.TableStyles[BuiltInPivotStyleId.PivotStyleDark7];

            #endregion #Set Style
        }


        static void BandedColumns(IWorkbook workbook) {
            #region #Banded Columns
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.BandedColumns = true;

            #endregion #Banded Columns
        }

        static void BandedRows(IWorkbook workbook) {
            #region #Banded Rows
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.BandedRows = true;

            #endregion #Banded Rows
        }

        static void ShowColumnHeaders(IWorkbook workbook) {
            #region #Column Headers
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.ShowColumnHeaders = false;

            #endregion #Column Headers
        }

        static void ShowRowHeaders(IWorkbook workbook) {
            #region #Row Headers
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.ColumnFields.Add(pivotTable.Fields["Region"]);
            pivotTable.ShowRowHeaders = false;

            #endregion #Row Headers
        }
    }
}
