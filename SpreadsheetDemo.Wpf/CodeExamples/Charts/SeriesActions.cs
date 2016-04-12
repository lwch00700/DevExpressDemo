using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class SeriesActions {
        static void RemoveSeries(IWorkbook workbook) {
            #region #RemoveSeries
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:E6"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.RemoveAt(1);

            #endregion #RemoveSeries
        }

        static void ChangeSeriesOrder(IWorkbook workbook) {
            #region #ChangeSeriesOrder
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D6"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series[1].BringForward();

            #endregion #ChangeSeriesOrder
        }

        static void UseSecondaryAxes(IWorkbook workbook) {
            #region #UseSecondaryAxes
            Worksheet worksheet = workbook.Worksheets["chartTask5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.LineMarker, worksheet["B2:D8"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.Series[1].AxisGroup = AxisGroup.Secondary;
            chart.Legend.Position = LegendPosition.Top;

            #endregion #UseSecondaryAxes
        }

        static void ChangeSeriesType(IWorkbook workbook) {
            #region #ChangeSeriesType
            Worksheet worksheet = workbook.Worksheets["chartTask5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.LineMarker, worksheet["B2:D8"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.Series[1].ChangeType(ChartType.ColumnClustered);
            chart.Series[1].AxisGroup = AxisGroup.Secondary;
            chart.Legend.Position = LegendPosition.Top;

            #endregion #ChangeSeriesType
        }
    }
}
