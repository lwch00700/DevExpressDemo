using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;

namespace DevExpress.Docs.Demos {
    public class ShiftScheduleGenerator {
        Worksheet sheet;
        ColumnCollection columns;
        RowCollection rows;
        CellCollection cells;
        IWorkbook book;
        char separator;

        const int shiftScheduleRowCount = 7;
        static List<string> weekDays = CreateWeekDays();

        public ShiftScheduleGenerator(CultureInfo culture) {
            this.book = new Workbook();
            this.book.Options.Culture = culture;
            InitCultureParams(culture);
        }

        public ShiftScheduleGenerator(IWorkbook book) {
            this.book = book;
            InitCultureParams(book.Options.Culture);
        }
        void InitCultureParams(CultureInfo culture) {
            this.separator = culture.TextInfo.ListSeparator[0];
        }
        public IWorkbook Generate() {
            return CreateBook();
        }

        static List<string> CreateWeekDays() {
            List<string> result = new List<string>();
            result.Add("Sunday");
            result.Add("Monday");
            result.Add("Tuesday");
            result.Add("Wednesday");
            result.Add("Thursday");
            result.Add("Friday");
            result.Add("Saturday");
            return result;
        }

        private IWorkbook CreateBook() {
            InitWorkbook();

            sheet = book.Worksheets[0];
            columns = sheet.Columns;
            rows = sheet.Rows;
            cells = sheet.Cells;

            AddStyles(book);
            SetColumnsWidth();
            MergeCells();
            CreateHeader();
            CreateShiftSchedules();
            return book;
        }

        void InitWorkbook() {
            book.Unit = DevExpress.Office.DocumentUnit.Point;
            book.Styles.DefaultStyle.Font.Name = "Calibri";
            book.Styles.DefaultStyle.Font.Size = 11;

            Worksheet sheet = book.Worksheets[0];
            sheet.Name = "Shift Schedule";
            sheet.DefaultRowHeight = 15;
            sheet.ActiveView.Orientation = PageOrientation.Portrait;
            sheet.PrintOptions.FitToPage = true;
            sheet.ActiveView.ShowGridlines = false;
        }

        void CreateShiftSchedules() {
            int startRow = 7;
            foreach (string weekDay in weekDays) {
                ApplyStylesInShiftSchedule(startRow);
                FormatCellsInShiftSchedule(startRow);
                SetBordersInShiftSchedule(startRow);
                FillShiftSchedule(startRow, weekDay);
                SetRowsHeightInShiftSchedules(startRow);

                startRow += shiftScheduleRowCount + 1;
            }
        }
        void SetRowsHeightInShiftSchedules(int startRow) {
            sheet.Rows[startRow - 1].Height = 21;
            sheet.Rows[startRow].Height = 17.45;
            sheet.Rows[startRow + 1].Height = 17.45;
            sheet.Rows[startRow + 2].Height = 17.45;
            sheet.Rows[startRow + 3].Height = 17.45;
            sheet.Rows[startRow + 4].Height = 17.45;
            sheet.Rows[startRow + 5].Height = 17.45;
            sheet.Rows[startRow + 6].Height = 42.75;
            sheet.Rows[startRow + 7].Height = 15;
        }
        void CreateHeader() {
            SetRowsHeightInHeader();
            FormatCellsInHeader();
            FillHeader();
        }
        void FillHeader() {
            cells["B2"].Value = "SHIFT SCHEDULE";
            cells["K2"].Value = "For the Week of:";
            cells["M2"].Formula = "=NOW()";
            cells["K3"].Value = "Department Name:";
        }
        void FillShiftSchedule(int startRow, string weekDay) {
            FillTableHeader(startRow, weekDay);
            SetArrayFormulasInShiftSchedule(startRow);
            FillTableData(startRow);
        }
        void FillTableData(int startRow) {
            FillTableRow1(startRow);
            FillTableRow2(startRow);
            FillTableRow3(startRow);
            FillTableRow4(startRow);
            FillTableRow5(startRow);
            FillTableRow6(startRow);
        }
        void FillTableRow6(int startRow) {
            cells["B" + (startRow + 6)].Value = "Teresa A";
            cells["H" + (startRow + 6)].Value = "cashier";
            cells["I" + (startRow + 6)].Value = "cashier";
            cells["J" + (startRow + 6)].Value = "cashier";
            cells["K" + (startRow + 6)].Value = "cashier";
        }
        void FillTableRow5(int startRow) {
            cells["B" + (startRow + 5)].Value = "Sean P";
            cells["L" + (startRow + 5)].Value = "Sick";
        }
        void FillTableRow4(int startRow) {
            cells["B" + (startRow + 4)].Value = "Jon M";
            cells["D" + (startRow + 4)].Value = "front desk";
            cells["E" + (startRow + 4)].Value = "front desk";
            cells["F" + (startRow + 4)].Value = "front desk";
            cells["G" + (startRow + 4)].Value = "front desk ";
            cells["H" + (startRow + 4)].Value = "front desk";
            cells["I" + (startRow + 4)].Value = "front desk";
            cells["J" + (startRow + 4)].Value = "front desk";
        }
        void FillTableRow3(int startRow) {
            cells["B" + (startRow + 3)].Value = "James S";
            cells["D" + (startRow + 3)].Value = "front desk";
            cells["E" + (startRow + 3)].Value = "front desk";
            cells["F" + (startRow + 3)].Value = "front desk";
            cells["G" + (startRow + 3)].Value = "front desk ";
            cells["H" + (startRow + 3)].Value = "front desk";
            cells["I" + (startRow + 3)].Value = "front desk";
            cells["J" + (startRow + 3)].Value = "front desk";
        }
        void FillTableRow2(int startRow) {
            cells["B" + (startRow + 2)].Value = "Tom Y";
            cells["D" + (startRow + 2)].Value = "cashier";
            cells["E" + (startRow + 2)].Value = "cashier";
            cells["F" + (startRow + 2)].Value = "cashier";
            cells["G" + (startRow + 2)].Value = "cashier";
        }
        void FillTableRow1(int startRow) {
            cells["B" + (startRow + 1)].Value = "Kelly F";
            cells["C" + (startRow + 1)].Value = "manager";
            cells["D" + (startRow + 1)].Value = "manager";
            cells["E" + (startRow + 1)].Value = "manager";
            cells["F" + (startRow + 1)].Value = "manager";
            cells["G" + (startRow + 1)].Value = "manager";
            cells["H" + (startRow + 1)].Value = "manager";
            cells["I" + (startRow + 1)].Value = "manager";
            cells["J" + (startRow + 1)].Value = "manager";
            cells["K" + (startRow + 1)].Value = "manager";
        }
        void FillTableHeader(int startRow, string weekDay) {
            cells[startRow - 1, 1].Value = weekDay;

            cells["C" + startRow].Formula = String.Format("=TIME(7{0}0{0}0)", separator);
            cells["D" + startRow].Formula = String.Format("=TIME(8{0}0{0}0)", separator);
            cells["E" + startRow].Formula = String.Format("=TIME(9{0}0{0}0)", separator);
            cells["F" + startRow].Formula = String.Format("=TIME(10{0}0{0}0)", separator);
            cells["G" + startRow].Formula = String.Format("=TIME(11{0}0{0}0)", separator);
            cells["H" + startRow].Formula = String.Format("=TIME(12{0}0{0}0)", separator);
            cells["I" + startRow].Formula = String.Format("=TIME(13{0}0{0}0)", separator);
            cells["J" + startRow].Formula = String.Format("=TIME(14{0}0{0}0)", separator);
            cells["K" + startRow].Formula = String.Format("=TIME(15{0}0{0}0)", separator);
            cells["L" + startRow].Value = "SICK?";
            cells["M" + startRow].Value = "TOTAL";
        }
        void SetArrayFormulasInShiftSchedule(int startRow) {
            sheet["M" + (startRow + 1)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 1) + ":K" + (startRow + 1) + "){0}1{0}0))", separator);
            sheet["M" + (startRow + 2)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 2) + ":K" + (startRow + 2) + "){0}1{0}0))", separator);
            sheet["M" + (startRow + 3)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 3) + ":K" + (startRow + 3) + "){0}1{0}0))", separator);
            sheet["M" + (startRow + 4)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 4) + ":K" + (startRow + 4) + "){0}1{0}0))", separator);
            sheet["M" + (startRow + 5)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 5) + ":K" + (startRow + 5) + "){0}1{0}0))", separator);
            sheet["M" + (startRow + 6)].ArrayFormula = String.Format("=SUM(IF(ISTEXT(C" + (startRow + 6) + ":K" + (startRow + 6) + "){0}1{0}0))", separator);
        }
        void SetRowsHeightInHeader() {
            sheet["A1:A3"].RowHeight = 18.75;
        }
        void SetColumnsWidth() {
            columns[0].WidthInCharacters = 3.77;
            columns[1].WidthInCharacters = 16.01;
            columns[2].WidthInCharacters = 10.09;
            columns[3].WidthInCharacters = 10.09;
            columns[4].WidthInCharacters = 10.09;
            columns[5].WidthInCharacters = 10.09;
            columns[6].WidthInCharacters = 10.09;
            columns[7].WidthInCharacters = 10.09;
            columns[8].WidthInCharacters = 10.09;
            columns[9].WidthInCharacters = 10.09;
            columns[10].WidthInCharacters = 10.09;
            columns[11].WidthInCharacters = 7.8;
            columns[12].WidthInCharacters = 11;
        }
        void AddStyles(IWorkbook book) {
            StyleCollection styles = book.Styles;
            Style style1 = styles.Add("Style 1");
            style1.Fill.PatternType = PatternType.Solid;
            style1.Fill.BackgroundColor = Color.FromArgb(255, 246, 246, 246);

            Style style2 = styles.Add("Style 10");
            style2.Fill.PatternType = PatternType.Solid;
            style2.Fill.BackgroundColor = Color.FromArgb(255, 226, 239, 251);

            Style style3 = styles.Add("Style 11");
            style3.Fill.PatternType = PatternType.Solid;
            style3.Fill.BackgroundColor = Color.FromArgb(255, 232, 232, 232);

            Style style4 = styles.Add("Style 12");
            style4.Fill.PatternType = PatternType.Solid;
            style4.Fill.BackgroundColor = Color.FromArgb(255, 222, 222, 222);

            Style style5 = styles.Add("Style 2");
            style5.Fill.PatternType = PatternType.Solid;
            style5.Fill.BackgroundColor = Color.FromArgb(255, 249, 249, 249);

            Style style6 = styles.Add("Style 3");
            style6.Fill.PatternType = PatternType.Solid;
            style6.Fill.BackgroundColor = Color.FromArgb(255, 255, 243, 196);

            Style style7 = styles.Add("Style 4");
            style7.Fill.PatternType = PatternType.Solid;
            style7.Fill.BackgroundColor = Color.FromArgb(255, 255, 238, 170);

            Style style8 = styles.Add("Style 5");
            style8.Fill.PatternType = PatternType.Solid;
            style8.Fill.BackgroundColor = Color.FromArgb(255, 203, 243, 175);

            Style style9 = styles.Add("Style 6");
            style9.Fill.PatternType = PatternType.Solid;
            style9.Fill.BackgroundColor = Color.FromArgb(255, 219, 247, 199);

            Style style10 = styles.Add("Style 7");
            style10.Fill.PatternType = PatternType.Solid;
            style10.Fill.BackgroundColor = Color.FromArgb(255, 219, 239, 255);

            Style style11 = styles.Add("Style 8");
            style11.Fill.PatternType = PatternType.Solid;
            style11.Fill.BackgroundColor = Color.FromArgb(255, 230, 244, 255);

            Style style12 = styles.Add("Style 9");
            style12.Fill.PatternType = PatternType.Solid;
            style12.Fill.BackgroundColor = Color.FromArgb(255, 230, 244, 255);

        }
        void ApplyStylesInShiftSchedule(int startRow) {
            ApplyStylesInTableRow1(startRow);
            ApplyStylesInTableRow2(startRow);
            ApplyStylesInTableRow3(startRow);
            ApplyStylesInTableRow4(startRow);
            ApplyStylesInTableRow5(startRow);
            ApplyStylesInTableRow6(startRow);
        }
        void ApplyStylesInTableRow6(int startRow) {
            sheet["B" + (startRow + 6)].Style = book.Styles["Style 1"];
            sheet["C" + (startRow + 6)].Style = book.Styles["Style 2"];
            sheet["D" + (startRow + 6)].Style = book.Styles["Style 1"];
            sheet["E" + (startRow + 6)].Style = book.Styles["Style 2"];
            sheet["F" + (startRow + 6)].Style = book.Styles["Style 1"];
            sheet["G" + (startRow + 6)].Style = book.Styles["Style 2"];
            sheet["H" + (startRow + 6)].Style = book.Styles["Style 5"];
            sheet["I" + (startRow + 6)].Style = book.Styles["Style 6"];
            sheet["J" + (startRow + 6)].Style = book.Styles["Style 5"];
            sheet["K" + (startRow + 6)].Style = book.Styles["Style 6"];
            sheet["L" + (startRow + 6)].Style = book.Styles["Style 1"];
            sheet["M" + (startRow + 6)].Style = book.Styles["Style 2"];
        }
        void ApplyStylesInTableRow5(int startRow) {
            sheet["C" + (startRow + 5)].Style = book.Styles["Style 11"];
            sheet["D" + (startRow + 5)].Style = book.Styles["Style 12"];
            sheet["E" + (startRow + 5)].Style = book.Styles["Style 11"];
            sheet["F" + (startRow + 5)].Style = book.Styles["Style 12"];
            sheet["G" + (startRow + 5)].Style = book.Styles["Style 11"];
            sheet["H" + (startRow + 5)].Style = book.Styles["Style 12"];
            sheet["I" + (startRow + 5)].Style = book.Styles["Style 11"];
            sheet["J" + (startRow + 5)].Style = book.Styles["Style 12"];
            sheet["K" + (startRow + 5)].Style = book.Styles["Style 11"];
            sheet["L" + (startRow + 5)].Style = book.Styles["Style 12"];
        }
        void ApplyStylesInTableRow4(int startRow) {
            sheet["B" + (startRow + 4)].Style = book.Styles["Style 1"];
            sheet["C" + (startRow + 4)].Style = book.Styles["Style 2"];
            sheet["D" + (startRow + 4)].Style = book.Styles["Style 9"];
            sheet["E" + (startRow + 4)].Style = book.Styles["Style 10"];
            sheet["F" + (startRow + 4)].Style = book.Styles["Style 9"];
            sheet["G" + (startRow + 4)].Style = book.Styles["Style 10"];
            sheet["H" + (startRow + 4)].Style = book.Styles["Style 9"];
            sheet["I" + (startRow + 4)].Style = book.Styles["Style 10"];
            sheet["J" + (startRow + 4)].Style = book.Styles["Style 9"];
            sheet["K" + (startRow + 4)].Style = book.Styles["Style 2"];
            sheet["L" + (startRow + 4)].Style = book.Styles["Style 1"];
            sheet["M" + (startRow + 4)].Style = book.Styles["Style 2"];
        }
        void ApplyStylesInTableRow3(int startRow) {
            sheet["D" + (startRow + 3)].Style = book.Styles["Style 7"];
            sheet["E" + (startRow + 3)].Style = book.Styles["Style 8"];
            sheet["F" + (startRow + 3)].Style = book.Styles["Style 7"];
            sheet["G" + (startRow + 3)].Style = book.Styles["Style 8"];
            sheet["H" + (startRow + 3)].Style = book.Styles["Style 7"];
            sheet["I" + (startRow + 3)].Style = book.Styles["Style 8"];
            sheet["J" + (startRow + 3)].Style = book.Styles["Style 7"];
        }
        void ApplyStylesInTableRow2(int startRow) {
            sheet["B" + (startRow + 2)].Style = book.Styles["Style 1"];
            sheet["C" + (startRow + 2)].Style = book.Styles["Style 2"];
            sheet["D" + (startRow + 2)].Style = book.Styles["Style 5"];
            sheet["E" + (startRow + 2)].Style = book.Styles["Style 6"];
            sheet["F" + (startRow + 2)].Style = book.Styles["Style 5"];
            sheet["G" + (startRow + 2)].Style = book.Styles["Style 6"];
            sheet["H" + (startRow + 2)].Style = book.Styles["Style 1"];
            sheet["I" + (startRow + 2)].Style = book.Styles["Style 2"];
            sheet["J" + (startRow + 2)].Style = book.Styles["Style 1"];
            sheet["K" + (startRow + 2)].Style = book.Styles["Style 2"];
            sheet["L" + (startRow + 2)].Style = book.Styles["Style 1"];
            sheet["M" + (startRow + 2)].Style = book.Styles["Style 2"];
        }
        void ApplyStylesInTableRow1(int startRow) {
            sheet["C" + (startRow + 1)].Style = book.Styles["Style 3"];
            sheet["D" + (startRow + 1)].Style = book.Styles["Style 4"];
            sheet["E" + (startRow + 1)].Style = book.Styles["Style 3"];
            sheet["F" + (startRow + 1)].Style = book.Styles["Style 4"];
            sheet["G" + (startRow + 1)].Style = book.Styles["Style 3"];
            sheet["H" + (startRow + 1)].Style = book.Styles["Style 4"];
            sheet["I" + (startRow + 1)].Style = book.Styles["Style 3"];
            sheet["J" + (startRow + 1)].Style = book.Styles["Style 4"];
            sheet["K" + (startRow + 1)].Style = book.Styles["Style 3"];
        }
        void FormatCellsInHeader() {
            Range range1 = sheet["B2:G2"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Font.Color = Color.FromArgb(255, 95, 95, 95);
            rangeFormatting1.Font.Size = 36;
            rangeFormatting1.Font.Name = "Segoe UI";
            rangeFormatting1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["K2:M3"];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Font.Color = Color.FromArgb(255, 95, 95, 95);
            rangeFormatting2.Font.Size = 10;
            rangeFormatting2.Font.Name = "Segoe UI";
            rangeFormatting2.NumberFormat = "m/d/yy";
            rangeFormatting2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            range2.EndUpdateFormatting(rangeFormatting2);
        }
        void FormatCellsInShiftSchedule(int startRow) {
            Range range1 = sheet["B" + startRow];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting1.Font.Color = Color.FromArgb(255, 95, 95, 95);
            rangeFormatting1.Font.Size = 14;
            rangeFormatting1.Font.Name = "Segoe UI";
            rangeFormatting1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting1.Alignment.Indent = 1;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["C" + startRow + ":M" + startRow];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting2.Font.Color = Color.FromArgb(255, 183, 183, 183);
            rangeFormatting2.Font.Size = 9;
            rangeFormatting2.Font.Name = "Segoe UI";
            rangeFormatting2.NumberFormat = "hh:mm AM/PM";
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet["B" + (startRow + 1) + ":B" + (startRow + 6)];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Font.Name = "Segoe UI Semibold";
            rangeFormatting3.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting3.Alignment.Indent = 1;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet["C" + startRow + ":M" + (startRow + 6)];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            range4.EndUpdateFormatting(rangeFormatting4);

            Range range5 = sheet["C" + (startRow + 1) + ":L" + (startRow + 6)];
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Font.Size = 10;
            rangeFormatting5.Font.Name = "Segoe UI";
            range5.EndUpdateFormatting(rangeFormatting5);

            Range range6 = sheet["M" + (startRow + 1) + ":M" + (startRow + 6)];
            Formatting rangeFormatting6 = range6.BeginUpdateFormatting();
            rangeFormatting6.Font.Name = "Segoe UI Semibold";
            rangeFormatting6.Font.Size = 11;
            range6.EndUpdateFormatting(rangeFormatting6);

            Range range7 = sheet["B" + startRow + ":M" + (startRow + 6)];
            Formatting rangeFormatting7 = range7.BeginUpdateFormatting();
            rangeFormatting7.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range7.EndUpdateFormatting(rangeFormatting7);
        }
        void SetBordersInShiftSchedule(int startRow) {
            sheet.Cells["B" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["B" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["C" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["C" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["D" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["D" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["E" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["E" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["F" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["F" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["G" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["G" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["H" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["H" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["I" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["I" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["J" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["J" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["K" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["K" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["L" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 95, 95, 95);
            sheet.Cells["L" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
            sheet.Cells["M" + startRow].Borders.BottomBorder.Color = Color.FromArgb(255, 143, 143, 143);
            sheet.Cells["M" + startRow].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
        }
        void MergeCells() {
            sheet["B2:G3"].Merge();
        }
    }
}
