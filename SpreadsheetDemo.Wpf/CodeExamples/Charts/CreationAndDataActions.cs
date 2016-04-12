using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class CreationAndDataActions {
        static void CreateChartFromRange(IWorkbook workbook) {
            #region #CreateChartFromRange
            Worksheet worksheet = workbook.Worksheets["chartTask1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Pie, worksheet["B2:C7"]);
            chart.TopLeftCell = worksheet.Cells["E2"];
            chart.BottomRightCell = worksheet.Cells["K15"];
            chart.Style = ChartStyle.ColorGradient;

            #endregion #CreateChartFromRange
        }

        static void CreateChartAndSelectData(IWorkbook workbook) {
            #region #CreateChartAndSelectData
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarFullStacked);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.SelectData(worksheet["B4:C8"], ChartDataDirection.Row);

            #endregion #CreateChartAndSelectData
        }

        static void CreateChartWithComplexRange(IWorkbook workbook) {
            #region #CreateChartWithComplexRange
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.Add(worksheet["D2"], worksheet["B3:B6"], worksheet["D3:D6"]);
            chart.Series.Add(worksheet["F2"], worksheet["B3:B6"], worksheet["F3:F6"]);

            #endregion #CreateChartWithComplexRange
        }

        static void CreateChartWithLiteralData(IWorkbook workbook) {
            #region #CreateChartWithLiteralData
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Columns[0].WidthInCharacters = 2.0;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered);
            chart.TopLeftCell = worksheet.Cells["B2"];
            chart.BottomRightCell = worksheet.Cells["H15"];
            Series serie = chart.Series.Add(
                new CellValue[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" },
                new CellValue[] { 50, 100, 30, 104, 87, 150 });

            #endregion #CreateChartWithLiteralData
        }

        static void ChangeDataReference(IWorkbook workbook) {
            #region #ChangeDataReference
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.Add(worksheet["D2"], worksheet["B3:B6"], worksheet["D3:D6"]);
            chart.Series.Add(worksheet["F2"], worksheet["B3:B6"], worksheet["F3:F6"]);
            chart.Series[1].Values = ChartData.FromRange(worksheet["E3:E6"]);
            chart.Series[1].SeriesName.SetReference(worksheet["E2"]);

            #endregion #ChangeDataReference
        }
    }
}
