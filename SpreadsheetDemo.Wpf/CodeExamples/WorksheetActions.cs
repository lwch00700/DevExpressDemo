using System;
using DevExpress.Spreadsheet;

namespace SpreadsheetExamples {

    public static class WorksheetActions {
        static void AssignActiveWorksheet(IWorkbook workbook) {
            #region ActiveWorksheet
            workbook.Worksheets.ActiveWorksheet = workbook.Worksheets["Sheet2"];
            #endregion ActiveWorksheet
        }

        static void AddWorksheet(IWorkbook workbook) {
            #region AddWorksheet
            workbook.Worksheets.Add();
            workbook.Worksheets.Add().Name = "TestSheet1";

            workbook.Worksheets.Add("TestSheet2");
            workbook.Worksheets.Insert(1, "TestSheet3");

            workbook.Worksheets.Insert(3);

            #endregion AddWorksheet
        }

        static void RemoveWorksheet(IWorkbook workbook) {
            #region DeleteWorksheet
            workbook.Worksheets.Remove(workbook.Worksheets["Sheet2"]);
            workbook.Worksheets.RemoveAt(0);

            Worksheet lastWorksheet = workbook.Worksheets.ActiveWorksheet;
            Range range = lastWorksheet.Range["A1:B3"];
            range[0].Value = "Sheets: ";
            range[1].Value = workbook.Worksheets.Count;
            range[2].Value = "Name:";
            range[3].Value = lastWorksheet.Name;

            #endregion DeleteWorksheet
        }

        static void RenameWorksheet(IWorkbook workbook) {
            #region RenameWorksheet
            Worksheet sheet2 = workbook.Worksheets[1];
            sheet2.Name = "Renamed Sheet";
            #endregion RenameWorksheet
        }

        static void CopyWorksheetWithinWorkbook(IWorkbook workbook) {
        }

        static void CopyWorksheetBetweenWorkbooks(IWorkbook workbook) {
        }

        static void MoveWorksheet(IWorkbook workbook) {
            #region MoveWorksheet
            workbook.Worksheets[0].Move(workbook.Worksheets.Count - 1);
            #endregion MoveWorksheet
        }

        static void ShowHideWorksheet(IWorkbook workbook) {
            #region ShowHideWorksheet
            workbook.Worksheets["Sheet2"].VisibilityType = WorksheetVisibilityType.VeryHidden;
            workbook.Worksheets["Sheet3"].Visible = false;
            #endregion ShowHideWorksheet
        }

        static void ShowHideGridlines(IWorkbook workbook) {
            #region ShowHideGridlines
            workbook.Worksheets[0].ActiveView.ShowGridlines = false;
            #endregion ShowHideGridlines
        }

        static void ShowHideHeadings(IWorkbook workbook) {
            #region ShowHideHeadings
            workbook.Worksheets[0].ActiveView.ShowHeadings = false;
            #endregion ShowHideHeadings
        }

        static void SetPageOrientation(IWorkbook workbook) {
            #region PageOrientation
            workbook.Worksheets[0].ActiveView.Orientation = PageOrientation.Landscape;

            #endregion PageOrientation
        }

        static void SetPageMargins(IWorkbook workbook) {
            #region PageMargins
            workbook.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            Margins pageMargins = workbook.Worksheets[0].ActiveView.Margins;
            pageMargins.Left = 2;
            pageMargins.Top = 3;
            pageMargins.Right = 1;
            pageMargins.Bottom = 2;
            pageMargins.Header = 2;
            pageMargins.Footer = 1;
            #endregion PageMargins
        }

        static void SetPaperSize(IWorkbook workbook) {
            #region PaperSize
            workbook.Worksheets[0].ActiveView.PaperKind = System.Drawing.Printing.PaperKind.A4;
            #endregion PaperSize
        }

        static void ZoomWorksheet(IWorkbook workbook) {
            #region WorksheetZoom
            workbook.Worksheets[0].ActiveView.Zoom = 50;

            #endregion WorksheetZoom
        }

    }
}
