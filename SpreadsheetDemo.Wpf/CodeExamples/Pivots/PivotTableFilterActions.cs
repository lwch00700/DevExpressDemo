using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {
    public static class PivotTableFilterActions {
        static void SetItemFilter(IWorkbook workbook) {
            #region #Item Filter
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Fields[1].ShowSingleItem(0);

            #endregion #Item Filter
        }

        static void SetItemVisibilityFilter(IWorkbook workbook) {
            #region #Item Visibility
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotItemCollection pivotFieldItems = pivotTable.Fields[1].Items;
            pivotFieldItems[0].Visible = false;

            #endregion #Item Visibility
        }

        static void SetLabelFilter(IWorkbook workbook) {
            #region #Label Filter
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotField field = pivotTable.Fields[0];
            pivotTable.Filters.Add(field, PivotFilterType.CaptionEqual, "South");

            #endregion #Label Filter
        }

        static void SetValueFilter(IWorkbook workbook) {
            #region #Value Filter
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotField field = pivotTable.Fields[1];
            pivotTable.Filters.Add(field, pivotTable.DataFields[0], PivotFilterType.ValueBetween, 6000, 13000);

            #endregion #Value Filter
        }

        static void SetTop10Filter(IWorkbook workbook) {
            #region #Top10 Filter
            Worksheet worksheet = workbook.Worksheets["Report4"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotField field = pivotTable.Fields[1];
            PivotFilter filter = pivotTable.Filters.Add(field, pivotTable.DataFields[0], PivotFilterType.Count, 2);
            filter.Top10Type = PivotFilterTop10Type.Bottom;

            #endregion #Top10 Filter
        }

        static void SetDateFilter(IWorkbook workbook) {
            #region #Date Filter
            Worksheet worksheet = workbook.Worksheets["Report6"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            PivotField field = pivotTable.Fields[0];
            pivotTable.Filters.Add(field, PivotFilterType.SecondQuarter);
            #endregion #Date Filter
        }

        static void SetMultipleFilter(IWorkbook workbook) {
            #region #Multiple Filters
            Worksheet worksheet = workbook.Worksheets["Report6"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            PivotTable pivotTable = worksheet.PivotTables["PivotTable1"];
            pivotTable.Behavior.AllowMultipleFieldFilters = true;
            PivotField field1 = pivotTable.Fields[0];
            pivotTable.Filters.Add(field1, PivotFilterType.SecondQuarter);
            PivotFilter filter = pivotTable.Filters.Add(field1, pivotTable.DataFields[0], PivotFilterType.Count, 2);
            filter.Top10Type = PivotFilterTop10Type.Bottom;

            #endregion #Multiple Filters
        }
    }
}
