using System;
using System.Drawing;
using DevExpress.Spreadsheet;
using System.Collections.Generic;
using Formatting = DevExpress.Spreadsheet.Formatting;

namespace SpreadsheetExamples {
    public static class TableActions {

        static void CreateListObject(IWorkbook workbook) {
            #region #CreateTable
            Worksheet worksheet = workbook.Worksheets[0];
            Table table = worksheet.Tables.Add(worksheet["A1:F12"], false);
            table.Style = workbook.TableStyles[BuiltInTableStyleId.TableStyleMedium20];
            #endregion #CreateTable
        }

        static void TableRanges(IWorkbook workbook) {
            #region #TableRanges
            Worksheet worksheet = workbook.Worksheets["TableRanges"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Table table = worksheet.Tables[0];
            TableColumn productColumn = table.Columns[0];
            TableColumn priceColumn = table.Columns[1];
            TableColumn quantityColumn = table.Columns[2];
            TableColumn discountColumn = table.Columns[3];
            TableColumn amountColumn = table.Columns.Add();
            amountColumn.Name = "Amount";
            amountColumn.Formula = "=[Price]*[Quantity]*(1-[Discount])";
            table.ShowTotals = true;
            discountColumn.TotalRowLabel = "Total:";
            amountColumn.TotalRowFunction = TotalRowFunction.Sum;
            priceColumn.DataRange.NumberFormat = "$#,##0.00";
            discountColumn.DataRange.NumberFormat = "0.0%";
            amountColumn.Range.NumberFormat = "$#,##0.00;$#,##0.00;\"\";@";
            table.HeaderRowRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            table.TotalRowRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            for (int i = 1; i < table.Columns.Count; i++) {
                table.Columns[i].DataRange.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            }
            table.Range.ColumnWidthInCharacters = 10;
            worksheet.Visible = true;
            #endregion #TableRanges
        }
        static void FormatTable(IWorkbook workbook) {
            #region #FormatTable
            Worksheet worksheet = workbook.Worksheets["FormatTable"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Table table = worksheet.Tables[0];
            TableStyleCollection tableStyles = workbook.TableStyles;
            TableStyle tableStyle = tableStyles[BuiltInTableStyleId.TableStyleMedium21];
            table.Style = tableStyle;
            table.ShowHeaders = true;
            table.ShowTotals = true;
            table.ShowTableStyleRowStripes = false;
            table.ShowTableStyleColumnStripes = true;
            table.ShowTableStyleFirstColumn = true;
            worksheet.Visible = true;
            #endregion #FormatTable
        }


        static void CustomTableStyle(IWorkbook workbook) {
            #region #CustomTableStyle
            Worksheet worksheet = workbook.Worksheets["Custom Table Style"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Table table = worksheet.Tables[0];

            String styleName = "testTableStyle";
            if (workbook.TableStyles.Contains(styleName)) {
                table.Style = workbook.TableStyles[styleName];
            } else {
                TableStyle customTableStyle = workbook.TableStyles.Add("testTableStyle");
                customTableStyle.BeginUpdate();
                try {
                    customTableStyle.TableStyleElements[TableStyleElementType.WholeTable].Font.Color = Color.FromArgb(107, 107, 107);
                    TableStyleElement headerRowStyle = customTableStyle.TableStyleElements[TableStyleElementType.HeaderRow];
                    headerRowStyle.Fill.BackgroundColor = Color.FromArgb(64, 66, 166);
                    headerRowStyle.Font.Color = Color.White;
                    headerRowStyle.Font.Bold = true;
                    TableStyleElement totalRowStyle = customTableStyle.TableStyleElements[TableStyleElementType.TotalRow];
                    totalRowStyle.Fill.BackgroundColor = Color.FromArgb(115, 193, 211);
                    totalRowStyle.Font.Color = Color.White;
                    totalRowStyle.Font.Bold = true;
                    TableStyleElement secondRowStripeStyle = customTableStyle.TableStyleElements[TableStyleElementType.SecondRowStripe];
                    secondRowStripeStyle.Fill.BackgroundColor = Color.FromArgb(234, 234, 234);
                    secondRowStripeStyle.StripeSize = 1;
                }
                finally {
                    customTableStyle.EndUpdate();
                }
                table.Style = customTableStyle;
            }

            worksheet.Visible = true;
            #endregion #CustomTableStyle
        }

        static void DuplicateTableStyle(IWorkbook workbook) {
            #region #DuplicateTableStyle
            Worksheet worksheet = workbook.Worksheets["Duplicate Table Style"];
            workbook.Worksheets.ActiveWorksheet = worksheet;
            Table table1 = worksheet.Tables[0];
            Table table2 = worksheet.Tables[1];
            TableStyle sourceTableStyle = workbook.TableStyles[BuiltInTableStyleId.TableStyleMedium19];
            TableStyle newTableStyle = sourceTableStyle.Duplicate();
            newTableStyle.TableStyleElements[TableStyleElementType.HeaderRow].Clear();

            table1.Style = sourceTableStyle;
            table2.Style = newTableStyle;

            worksheet.Visible = true;
            #endregion #DuplicateTableStyle
        }
    }
}
