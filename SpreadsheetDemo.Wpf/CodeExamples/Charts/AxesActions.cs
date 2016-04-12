using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class AxesActions {
        static void MinAndMaxValues(IWorkbook workbook) {
            #region #MinAndMaxValues
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            Axis axis = chart.PrimaryAxes[1];
            axis.Scaling.AutoMax = false;
            axis.Scaling.Max = 1;
            axis.Scaling.AutoMin = false;
            axis.Scaling.Min = 0;
            chart.Legend.Visible = false;

            #endregion #MinAndMaxValues
        }

        static void MajorUnits(IWorkbook workbook) {
            #region #MajorUnits
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarFullStacked);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.SelectData(worksheet["B4:C8"], ChartDataDirection.Row);
            chart.PrimaryAxes[1].MajorUnit = 0.2;
            chart.Legend.Visible = false;

            #endregion #MajorUnits
        }

        static void MajorAndMinorGridlines(IWorkbook workbook) {
            #region #MajorAndMinorGridlines
            Worksheet worksheet = workbook.Worksheets["chartTask5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Line, worksheet["B2:C8"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.PrimaryAxes[0].MajorGridlines.Visible = true;
            chart.PrimaryAxes[1].MinorGridlines.Visible = true;
            chart.Legend.Visible = false;

            #endregion #MajorAndMinorGridlines
        }

        static void LabelsNumberFormat(IWorkbook workbook) {
            #region #LabelsNumberFormat
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            Axis axis = chart.PrimaryAxes[1];
            axis.NumberFormat.FormatCode = "0%";
            axis.NumberFormat.IsSourceLinked = false;
            chart.Legend.Visible = false;

            #endregion #LabelsNumberFormat
        }

        static void HideTickMarks(IWorkbook workbook) {
            #region #HideTickMarks
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            Axis axis = chart.PrimaryAxes[0];
            axis.MajorTickMarks = AxisTickMarks.None;
            axis = chart.PrimaryAxes[1];
            axis.MajorTickMarks = AxisTickMarks.None;
            chart.Legend.Visible = false;

            #endregion #HideTickMarks
        }

        static void HideAxisLine(IWorkbook workbook) {
            #region #HideAxisLine
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.PrimaryAxes[1].Outline.SetNoFill();
            chart.Legend.Visible = false;

            #endregion #HideAxisLine
        }

        static void Position(IWorkbook workbook) {
            #region #AxisPosition
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.PrimaryAxes[1].Position = AxisPosition.Right;
            chart.Legend.Visible = false;

            #endregion #AxisPosition
        }

        static void Orientation(IWorkbook workbook) {
            #region #AxisOrientation
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B3:C5"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.PrimaryAxes[0].Scaling.Orientation = AxisOrientation.MaxMin;
            chart.Legend.Visible = false;

            #endregion #AxisOrientation
        }

        static void LogScale(IWorkbook workbook) {
            #region #LogScale
            Worksheet worksheet = workbook.Worksheets["chartTask5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Line, worksheet["B2:D8"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.PrimaryAxes[1].Scaling.LogScale = true;
            chart.PrimaryAxes[1].Scaling.LogBase = 10;
            chart.Legend.Position = LegendPosition.Bottom;

            #endregion #LogScale
        }
    }
}
