using System;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Charts;
using DevExpress.Spreadsheet.Drawings;
using DevExpress.Utils;

namespace SpreadsheetExamples {
    public static class Charts {
        static void PieChart(IWorkbook workbook) {
            #region #PieChart
            Worksheet worksheet = workbook.Worksheets["chartTask1"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.PieExploded, worksheet["B2:C7"]);
            chart.TopLeftCell = worksheet.Cells["E2"];
            chart.BottomRightCell = worksheet.Cells["K15"];
            chart.Style = ChartStyle.ColorGradient;
            chart.Legend.Visible = false;
            chart.Views[0].FirstSliceAngle = 100;
            DataLabelOptions dataLabels = chart.Views[0].DataLabels;
            dataLabels.ShowCategoryName = true;
            dataLabels.ShowPercent = true;
            dataLabels.Separator = "\n";

            #endregion #PieChart
        }

        static void BarChart(IWorkbook workbook) {
            #region #BarChart
            Worksheet worksheet = workbook.Worksheets["chartTask2"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.BarFullStacked);
            chart.TopLeftCell = worksheet.Cells["E3"];
            chart.BottomRightCell = worksheet.Cells["K14"];
            chart.SelectData(worksheet["B3:C8"], ChartDataDirection.Row);
            chart.Title.Visible = true;
            chart.Title.SetReference(worksheet["B1"]);
            chart.Legend.Position = LegendPosition.Bottom;
            chart.PrimaryAxes[0].Visible = false;
            chart.PrimaryAxes[1].MajorUnit = 0.2;

            #endregion #BarChart
        }

        static void ColumnChart(IWorkbook workbook) {
            #region #ColumnChart
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.Add(worksheet["D2"], worksheet["B3:B6"], worksheet["D3:D6"]);
            chart.Series.Add(worksheet["F2"], worksheet["B3:B6"], worksheet["F3:F6"]);
            chart.Title.Visible = true;
            chart.Title.SetValue("Mobile OS market share");
            Axis axis = chart.PrimaryAxes[0];
            axis.MajorTickMarks = AxisTickMarks.None;
            axis = chart.PrimaryAxes[1];
            axis.Outline.SetNoFill();
            axis.MajorTickMarks = AxisTickMarks.None;
            axis.NumberFormat.FormatCode = "0%";
            axis.NumberFormat.IsSourceLinked = false;
            axis.Scaling.AutoMax = false;
            axis.Scaling.Max = 1;
            axis.Scaling.AutoMin = false;
            axis.Scaling.Min = 0;
            ChartView view = chart.Views[0];
            view.GapWidth = 75;
            view.DataLabels.ShowValue = true;
            view.DataLabels.NumberFormat.FormatCode = "0%";
            view.DataLabels.NumberFormat.IsSourceLinked = false;
            chart.Style = ChartStyle.ColorGradient;

            #endregion #ColumnChart
        }

        static void ComplexChart(IWorkbook workbook) {
            #region #ComplexChart
            Worksheet worksheet = workbook.Worksheets["chartTask5"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ColumnClustered, worksheet["B2:D8"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.Series[1].ChangeType(ChartType.Line);
            chart.Series[1].AxisGroup = AxisGroup.Secondary;
            chart.Style = ChartStyle.ColorGradient;
            chart.Legend.Position = LegendPosition.Bottom;

            #endregion #ComplexChart
        }

        static void DoughnutChart(IWorkbook workbook) {
            #region #DoughnutChart
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Doughnut);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.Add(worksheet["E2"], worksheet["B3:B6"], worksheet["E3:E6"]);
            chart.Title.Visible = true;
            chart.Title.SetValue("Mobile OS market share Q4'13");
            chart.Views[0].HoleSize = 60;
            chart.Views[0].DataLabels.ShowPercent = true;

            #endregion #DoughnutChart
        }

        static void Pie3dChart(IWorkbook workbook) {
            #region #Pie3dChart
            Worksheet worksheet = workbook.Worksheets["chartTask3"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.Pie3D);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N14"];
            chart.Series.Add(worksheet["E2"], worksheet["B3:B6"], worksheet["E3:E6"]);
            chart.Series[0].CustomDataPoints.Add(2).Explosion = 25;
            chart.View3D.YRotation = 255;
            chart.Style = ChartStyle.ColorGradient;

            #endregion #Pie3dChart
        }

        static void ScatterChart(IWorkbook workbook) {
            #region #ScatterChart
            Worksheet worksheet = workbook.Worksheets["chartScatter"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.ScatterLineMarkers, worksheet["C2:D52"]);
            chart.TopLeftCell = worksheet.Cells["F2"];
            chart.BottomRightCell = worksheet.Cells["L15"];
            chart.Series[0].Marker.Symbol = MarkerStyle.Circle;
            Axis axis = chart.PrimaryAxes[0];
            axis.Scaling.AutoMax = false;
            axis.Scaling.AutoMin = false;
            axis.Scaling.Max = 60.0;
            axis.Scaling.Min = -60.0;
            axis.MajorGridlines.Visible = true;
            axis = chart.PrimaryAxes[1];
            axis.Scaling.AutoMax = false;
            axis.Scaling.AutoMin = false;
            axis.Scaling.Max = 50.0;
            axis.Scaling.Min = -50.0;
            axis.MajorUnit = 10.0;

            #endregion #ScatterChart
        }

        static void StockChart(IWorkbook workbook) {
            #region #StockChart
            Worksheet worksheet = workbook.Worksheets["chartStock"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Chart chart = worksheet.Charts.Add(ChartType.StockOpenHighLowClose, worksheet["B2:F7"]);
            chart.TopLeftCell = worksheet.Cells["H2"];
            chart.BottomRightCell = worksheet.Cells["N15"];
            chart.Title.Visible = true;
            chart.Title.SetValue("NASDAQ:MSFT");
            chart.Legend.Visible = false;
            Axis axis = chart.PrimaryAxes[1];
            axis.Scaling.AutoMax = false;
            axis.Scaling.Max = 40.5;
            axis.Scaling.AutoMin = false;
            axis.Scaling.Min = 38.5;
            axis.MajorUnit = 0.25;
            axis.NumberFormat.FormatCode = "#0.00";
            axis.NumberFormat.IsSourceLinked = false;
            axis.Title.Visible = true;
            axis.Title.SetValue("Price in USD");

            #endregion #StockChart
        }
    }
}
