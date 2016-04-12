using System;
using System.IO;
using DevExpress.Spreadsheet;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace SpreadsheetExamples {
    public static class ImportExportActions {
        static void ImportArrays(IWorkbook workbook) {
            #region #ImportData
            Worksheet worksheet = workbook.Worksheets[0];

            worksheet.Cells["A1"].Value = "Import an array horizontally:";
            string[] array = new string[] { "AAA", "BBB", "CCC", "DDD" };
            worksheet.Import(array, 0, 1, false);

            worksheet.Cells["A3"].Value = "Import a two-dimensional array:";
            String[,] names = new String[2, 4]{
            {"Ann", "Edward", "Angela", "Alex"},
            {"Rachel", "Bruce", "Barbara", "George"}
                 };
            worksheet.Import(names, 2, 1);

            worksheet.Cells["A6"].Value = "Import data from ArrayList vertically:";
            List<string> cities = new List<string>();
            cities.Add("New York");
            cities.Add("Rome");
            cities.Add("Beijing");
            cities.Add("Delhi");
            worksheet.Import(cities, 5, 1, true);

            Worksheet sheet = workbook.Worksheets[0];
            sheet.Cells["A11"].Value = "Import data from a DataTable:";
            DataTable table = new DataTable("Employees");
            table.Columns.Add("FirstN", typeof(string));
            table.Columns.Add("LastN", typeof(string));
            table.Columns.Add("JobTitle", typeof(string));
            table.Columns.Add("Age", typeof(Int32));

            table.Rows.Add("Nancy", "Davolio", "recruiter", 32);
            table.Rows.Add("Andrew", "Fuller", "engineer", 28);
            sheet.Import(table, true, 10, 1);
            for(int i = 1; i < 5; i++) {
                worksheet.Cells[10, i].FillColor = Color.LightGray;
            }
            sheet["A:D"].AutoFitColumns();
            #endregion #ImportData
        }

        static void ExportToPdf(IWorkbook workbook) {
            #region #ExportToPdf
            Worksheet firstSheet = workbook.Worksheets[0];
            firstSheet.Cells["B2"].Value = "This document is exported to the PDF format";
            Table table = firstSheet.Tables.Add(firstSheet["A1:H30"], false);
            table.Style = workbook.TableStyles[BuiltInTableStyleId.TableStyleMedium14];
            table.ShowTotals = true;
            table.Columns[0].TotalRowLabel = "Total";

            using (FileStream pdfFileStream = new FileStream("Document_PDF.pdf", FileMode.Create)) {
                workbook.ExportToPdf(pdfFileStream);
            }
            System.Diagnostics.Process.Start("Document_PDF.pdf");
            #endregion #ExportToPdf
        }

    }
}
