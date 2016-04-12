using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class DataLabelsActions {
        static void ShowDataLabels(IWorkbook workbook) {
            #region #ShowDataLabels
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D4"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Views[0].DataLabels.ShowValue = true;

            #endregion #ShowDataLabels
        }

        static void SetDataLabelsPosition(IWorkbook workbook) {
            #region #SetDataLabelsPosition
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D4"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Views[0].DataLabels.ShowValue = true;
            chart.Views[0].DataLabels.LabelPosition = DataLabelPosition.Center;

            #endregion #SetDataLabelsPosition
        }

        static void DataLabelsNumberFormat(IWorkbook workbook) {
            #region #DataLabelsNumberFormat
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D4"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Views[0].DataLabels.ShowValue = true;
            chart.Views[0].DataLabels.LabelPosition = DataLabelPosition.Center;
            chart.Views[0].DataLabels.NumberFormat.FormatCode = "0%";
            chart.Views[0].DataLabels.NumberFormat.IsSourceLinked = false;

            #endregion #DataLabelsNumberFormat
        }

        static void DataLabelsPerSeries(IWorkbook workbook) {
            #region #DataLabelsPerSeries
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D4"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series[1].CustomDataLabels.ShowValue = true;
            chart.Series[1].UseCustomDataLabels = true;

            #endregion #DataLabelsPerSeries
        }

        static void DataLabelsPerPoint(IWorkbook workbook) {
            #region #DataLabelsPerPoint
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D4"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series[1].CustomDataLabels.Add(1).ShowValue = true;
            chart.Series[1].UseCustomDataLabels = true;

            #endregion #DataLabelsPerPoint
        }

        static void DataLabelsSeparator(IWorkbook workbook) {
            #region #DataLabelsSeparator
            Worksheet worksheet = workbook.Worksheets["chartTask1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Pie, worksheet["B2:C7"]);
            chart.TopLeftCell = worksheet.Cells["E2"];
            chart.BottomRightCell = worksheet.Cells["K15"];
            DataLabelOptions dataLabels = chart.Views[0].DataLabels;
            dataLabels.ShowCategoryName = true;
            dataLabels.ShowPercent = true;
            dataLabels.Separator = "\n";
            chart.Style = ChartStyle.ColorGradient;
            chart.Legend.Visible = false;
            chart.Views[0].FirstSliceAngle = 100;

            #endregion #DataLabelsSeparator
        }
    }
}
