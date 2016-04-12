using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class TitlesActions {
        static void ShowChartTitle(IWorkbook workbook) {
            #region #ShowChartTitle
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.Title.Visible = true;
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #ShowChartTitle
        }

        static void SetChartTitleText(IWorkbook workbook) {
            #region #SetChartTitleText
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.Title.Visible = true;
            chart.Title.SetValue("Market share Q3'13");
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #SetChartTitleText
        }

        static void LinkChartTitleToCellRange(IWorkbook workbook) {
            #region #LinkChartTitleToCellRange
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.Title.Visible = true;
            chart.Title.SetReference(worksheet["B1"]);
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #LinkChartTitleToCellRange
        }


        static void ShowAxisTitle(IWorkbook workbook) {
            #region #ShowAxisTitle
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.PrimaryAxes[1].Title.Visible = true;
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #ShowAxisTitle
        }

        static void SetAxisTitleText(IWorkbook workbook) {
            #region #SetAxisTitleText
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.PrimaryAxes[1].Title.Visible = true;
            chart.PrimaryAxes[1].Title.SetValue("Shipment in millions of units");
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #SetAxisTitleText
        }

        static void LinkAxisTitleToCellRange(IWorkbook workbook) {
            #region #LinkAxisTitleToCellRange
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarClustered, worksheet["B4:C7"]);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.PrimaryAxes[1].Title.Visible = true;
            chart.PrimaryAxes[1].Title.SetReference(worksheet["C3"]);
            chart.Legend.Visible = false;
            chart.Views[0].VaryColors = true;

            #endregion #LinkAxisTitleToCellRange
        }
    }
}
