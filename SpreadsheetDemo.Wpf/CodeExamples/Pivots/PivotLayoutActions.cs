using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotLayoutActions {

        static void ColumnGrandTotals(IWorkbook workbook) {
            #region #Column Grand Totals
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.ColumnFields.Add(pivotTable.Fields["Region"]);
            pivotTable.Layout.ShowColumnGrandTotals = false;

            #endregion #Column Grand Totals
        }

        static void RowGrandTotals(IWorkbook workbook) {
            #region #Row Grand Totals
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.ShowRowGrandTotals = false;

            #endregion #Row Grand Totals
        }

        static void DataOnRows(IWorkbook workbook) {
            #region #Multiple data fields
            Worksheet worksheet = workbook.Worksheets["Report2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.DataOnRows = false;

            #endregion #Multiple data fields
        }
        static void MergeTitles(IWorkbook workbook) {
   #region #Merge Titles
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.SetReportLayout(PivotReportLayout.Tabular);
            pivotTable.Layout.MergeTitles = true;

            #endregion #Merge Titles
        }

  static void ShowAllSubtotals(IWorkbook workbook) {
   #region #Show All Subtotals
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.ShowAllSubtotals(true);

            #endregion #Show All Subtotals
        }

  static void HideAllSubtotals(IWorkbook workbook) {
   #region #Hide All Subtotals
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.HideAllSubtotals();

            #endregion #Hide All Subtotals
        }

        static void SetCompactReportLayout(IWorkbook workbook) {
            #region #Compact Report Layout
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.SetReportLayout(PivotReportLayout.Compact);

            #endregion #Compact Report Layout
        }

        static void SetOutlineReportLayout(IWorkbook workbook) {
            #region #Outline Report Layout
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.SetReportLayout(PivotReportLayout.Outline);

            #endregion #Outline Report Layout
        }

        static void SetTabularReportLayout(IWorkbook workbook) {
            #region #Tabular Report Layout
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.SetReportLayout(PivotReportLayout.Tabular);

            #endregion #Tabular Report Layout
        }

  static void RepeatAllItemLabels(IWorkbook workbook) {
   #region #Repeat All Item Labels
            Worksheet worksheet = workbook.Worksheets["Report5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.RepeatAllItemLabels(true);

            #endregion #Repeat All Item Labels
        }

  static void InsertBlankRows(IWorkbook workbook) {
   #region #Insert Blank Rows
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.InsertBlankRows();

            #endregion #Insert Blank Rows
        }

  static void RemoveBlankRows(IWorkbook workbook) {
   #region #Remove Blank Rows
            Worksheet worksheet = workbook.Worksheets["Report1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Layout.InsertBlankRows();
            pivotTable.Layout.RemoveBlankRows();

            #endregion #Remove Blank Rows
        }
    }
}
