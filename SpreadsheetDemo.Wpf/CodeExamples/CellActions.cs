using System;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using System.Globalization;

namespace SpreadsheetExamples {
    public static class CellActions {
        static void ChangeCellValue(IWorkbook workbook) {
            #region #CellValue
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Cells["B2"].Value = DateTime.Now;
            worksheet.Cells["B3"].Value = Math.PI;
            worksheet.Cells["B4"].Value = "Have a nice day!";
            worksheet.Cells["B5"].Value = CellValue.ErrorReference;
            worksheet.Cells["B6"].Value = true;
            worksheet.Cells["B7"].Value = float.MaxValue;
            worksheet.Cells["B8"].Value = 'a';
            worksheet.Cells["B9"].Value = Int32.MaxValue;
            worksheet.Range["B12:C12"].Value = 10;

            worksheet.Cells["A2"].Value = "dateTime";
            worksheet.Cells["A3"].Value = "double";
            worksheet.Cells["A4"].Value = "string";
            worksheet.Cells["A5"].Value = "error constant";
            worksheet.Cells["A6"].Value = "boolean";
            worksheet.Cells["A7"].Value = "float";
            worksheet.Cells["A8"].Value = "char";
            worksheet.Cells["A9"].Value = "int32";
            worksheet.Cells["A12"].Value = "fill range";

            Range header = worksheet.Range["A1:B1"];
            header[0].Value = "Type";
            header[1].Value = "Value";
            header.ColumnWidthInCharacters = 25;
            header.Style = workbook.Styles["Header"];

            workbook.Options.Culture = CultureInfo.InvariantCulture;
            #endregion #CellValue
        }

        static void AddHyperlink(IWorkbook workbook) {
            #region #AddHyperlink
            Worksheet worksheet = workbook.Worksheets[0];
            Worksheet worksheet2 = workbook.Worksheets[1];
            worksheet.Range["A:B"].ColumnWidthInCharacters = 12;
            Cell cell = worksheet.Cells["A1"];
            worksheet.Hyperlinks.Add(cell, "http://www.devexpress.com/", true, "DevExpress");
            Range range = worksheet.Range["C3:D4"];
            Hyperlink cellHyperlink = worksheet.Hyperlinks.Add(range, "Sheet2!B2:E7", false, "Select Range");
            cellHyperlink.TooltipText = "Click Me";

            worksheet2.Hyperlinks.Add(worksheet2.Range["C9:D9"], "Sheet1!A1", false, "Back to Sheet1");
            #endregion #AddHyperlink
        }

        static void CopyCellDataAndStyle(IWorkbook workbook) {

            #region #CopyCell
            Worksheet worksheet = workbook.Worksheets[0];
            Style style = workbook.Styles[BuiltInStyleId.Input];
            worksheet.Cells["A1"].Value = "Source Cell";
            Cell sourceCell = worksheet.Cells["B1"];
            sourceCell.Formula = "= PI()";
            sourceCell.NumberFormat = "0.0000";
            sourceCell.Style = style;
            sourceCell.Font.Color = DXColor.Blue;
            sourceCell.Font.Bold = true;
            sourceCell.Borders.SetOutsideBorders(DXColor.Black, BorderLineStyle.Thin);
            worksheet.Cells["A3"].Value = "Copy All";
            worksheet.Cells["B3"].CopyFrom(sourceCell);
            worksheet.Cells["A4"].Value = "Copy Values";
            worksheet.Cells["B4"].CopyFrom(sourceCell, PasteSpecial.Values);
            worksheet.Cells["A5"].Value = "Copy Values and Number Formats";
            worksheet.Cells["B5"].CopyFrom(sourceCell, PasteSpecial.Values | PasteSpecial.NumberFormats);
            worksheet.Cells["A6"].Value = "Copy Formats";
            worksheet.Cells["B6"].CopyFrom(sourceCell, PasteSpecial.Formats);
            worksheet.Cells["A7"].Value = "Copy All Except Borders";
            worksheet.Cells["B7"].CopyFrom(sourceCell, PasteSpecial.All & ~PasteSpecial.Borders);
            worksheet.Cells["A8"].Value = "Copy Borders";
            worksheet.Cells["B8"].CopyFrom(sourceCell, PasteSpecial.Borders);

            worksheet.Columns["A"].AutoFit();
            worksheet.Columns["B"].AutoFit();
            #endregion #CopyCell
        }

        static void MergeAndSplitCells(IWorkbook workbook) {
            #region #MergeCells
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Cells["A1"].FillColor = DXColor.LightGray;
            worksheet.Cells["B2"].Value = "B2";
            worksheet.Cells["B2"].FillColor = DXColor.LightGreen;
            worksheet.Cells["C3"].Value = "C3";
            worksheet.Cells["C3"].FillColor = DXColor.LightGreen;
            Range range = worksheet.Range["A1:C5"];
            range.Merge();
            #endregion #MergeCells
        }

    }
}
