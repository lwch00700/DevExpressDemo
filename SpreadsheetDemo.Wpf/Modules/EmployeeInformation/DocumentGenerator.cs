using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Spreadsheet;
using System.Globalization;
using System.Drawing;

namespace DevExpress.Spreadsheet.Demos {

    public class EmployeeInformationDocumentGenerator {
        char separator;
        IWorkbook book = null;
        Worksheet sheet1 = null;
        Worksheet sheet2 = null;
        Worksheet sheet3 = null;
        string path = String.Empty;
        string shortDatePattern;
        Color blueFontColor;
        Color blueFillColor;
        Color blueBorderColor;
        Color darkBlueColor;
        const int NameColumnIndex = 2;
        const int PaystubRowCount = 24;
        const int PaystubColumnCount = 7;
        const int FirstHeaderColumn = 2;
        const int FirstDataColumn = 3;
        const int SecondHeaderColumn = 5;
        const int SecondDataColumn = 6;

        public EmployeeInformationDocumentGenerator(CultureInfo culture) {
            this.book = new Workbook();
            this.book.Options.Culture = culture;
            InitColors();
            InitCultureParams(culture);
        }
        public EmployeeInformationDocumentGenerator(IWorkbook workbook) {
            this.book = workbook;
            InitColors();
            InitCultureParams(workbook.Options.Culture);
        }
        void InitColors() {
            blueFontColor = Color.FromArgb(255, 212, 233, 251);
            blueFillColor = Color.FromArgb(255, 101, 168, 196);
            blueBorderColor = Color.FromArgb(255, 84, 158, 189);
            darkBlueColor = Color.FromArgb(255, 18, 92, 156);
        }
        void InitCultureParams(CultureInfo culture) {
            this.separator = culture.TextInfo.ListSeparator[0];
            this.shortDatePattern = culture.DateTimeFormat.ShortDatePattern;
        }
        public IWorkbook Generate(string path) {
            this.path = path;
            InitializeBook();
            return this.book;
        }

        void InitializeBook() {
            InitWorkbook();
            AddStyles();
            SetColumnsWidth();
            SetPeriodEnding();
            CreateIndividualPaystubs();
        }
        void InitWorkbook() {
            this.book.Unit = DevExpress.Office.DocumentUnit.Point;
            this.book.Styles.DefaultStyle.Font.Name = "Segoe UI";
            this.book.Styles.DefaultStyle.Font.Size = 11;
            this.book.LoadDocument(this.path);

            sheet1 = book.Worksheets[0];
            sheet2 = book.Worksheets[1];

            sheet3 = book.Worksheets[2];
            sheet3.ActiveView.Zoom = 100;
            sheet3.DefaultRowHeight = 12.75;
            sheet3.Name = "Individual paystubs";
            sheet3.ActiveView.ShowGridlines = false;

            sheet3.Cells.Font.Name = "Segoe UI";

            sheet1.ActiveView.Orientation = PageOrientation.Landscape;
            sheet1.PrintOptions.FitToPage = true;

            sheet2.ActiveView.Orientation = PageOrientation.Landscape;
            sheet2.PrintOptions.FitToPage = true;
            sheet2.PrintOptions.FitToWidth = 1;
            sheet2.PrintOptions.FitToHeight = 0;

            sheet3.ActiveView.Orientation = PageOrientation.Landscape;
            sheet3.PrintOptions.FitToWidth = 1;
            sheet3.PrintOptions.FitToHeight = 0;
            sheet3.PrintOptions.FitToPage = true;
        }
        void SetPeriodEnding() {
            string separator = new string(this.separator, 1);
            sheet2.Cells["B4"].Formula = "=CONCATENATE(\"PERIOD ENDING: \"" + separator + "TEXT(NOW()" + separator + " \"" + shortDatePattern + "\"))";
        }
        void CreateIndividualPaystubs() {
            for(int paystubID = 1; paystubID < 10; paystubID++) {
                int topPaystubRow = (paystubID - 1) * 16;
                SetRowsHeightInPaystub(topPaystubRow);
                FormatCellsInPaystub(topPaystubRow);
                ApplyStylesInPaystub(topPaystubRow);
                SetBordersInPaystub(topPaystubRow);
                SetPaystubDataInHeader(topPaystubRow);
                SetPaystubData(topPaystubRow, paystubID);
                MergeCells(topPaystubRow);
            }
        }
        void MergeCells(int topPaystubRow) {
            sheet3.Range.FromLTRB(2, topPaystubRow + 2, 3, topPaystubRow + 2).Merge();
            sheet3.Range.FromLTRB(5, topPaystubRow + 2, 6, topPaystubRow + 2).Merge();
        }
        void SetRowsHeightInPaystub(int topPaystubRow) {
            sheet3.Rows[topPaystubRow + 0].Height = 26.25;
            sheet3.Rows[topPaystubRow + 1].Height = 16;
            sheet3.Rows[topPaystubRow + 2].Height = 30;
            sheet3.Rows[topPaystubRow + 3].Height = 19.5;
            sheet3.Rows[topPaystubRow + 4].Height = 17.25;
            for(int i = 5; i < 15; i++)
                sheet3.Rows[topPaystubRow + i].Height = 24;
            sheet3.Rows[topPaystubRow + 15].Height = 21.75;
        }
        void SetColumnsWidth() {
            for (int paystubOrderInRow = 0; paystubOrderInRow < 3; paystubOrderInRow++) {
                int firstPaystubColumn = paystubOrderInRow * (PaystubColumnCount + 1);
                sheet3.Columns[firstPaystubColumn + 0].WidthInCharacters = 4.5;
                sheet3.Columns[firstPaystubColumn + 1].WidthInCharacters = 3.35;
                sheet3.Columns[firstPaystubColumn + 2].WidthInCharacters = 40;
                sheet3.Columns[firstPaystubColumn + 3].WidthInCharacters = 14.53;
                sheet3.Columns[firstPaystubColumn + 4].WidthInCharacters = 3.3;
                sheet3.Columns[firstPaystubColumn + 5].WidthInCharacters = 40;
                sheet3.Columns[firstPaystubColumn + 6].WidthInCharacters = 14.53;
                sheet3.Columns[firstPaystubColumn + 7].WidthInCharacters = 3.35;
            }
        }
        void SetPaystubData(int topPaystubRow, int paystubID) {
            topPaystubRow++;
            string employeeIdColumn1 = "D";
            string employeeIdColumn2 = "G";
            sheet3.Cells[topPaystubRow + 4, FirstDataColumn].Value = paystubID;
            string employeeInformationTable = GetEmployeeInformationTable();
            string payrollCalculatorTable = GetPayrollCalculatorTable();
            string separator = new string(this.separator, 1);
            sheet3.Cells[topPaystubRow + 5, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "4" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 6, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "3" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 7, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "8" + separator + "FALSE)*" + employeeIdColumn2 + (topPaystubRow + 12);
            sheet3.Cells[topPaystubRow + 8, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "9" + separator + "FALSE)*" + employeeIdColumn2 + (topPaystubRow + 12);
            sheet3.Cells[topPaystubRow + 9, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "11" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 10, FirstDataColumn].Formula = "=SUM(" + employeeIdColumn1 + (topPaystubRow + 8).ToString() + ":" + employeeIdColumn1 + (topPaystubRow + 10)
                                                                   + ")+SUM(" + employeeIdColumn2 + (topPaystubRow + 5).ToString() + ":" + employeeIdColumn2 + (topPaystubRow + 6)
                                                                   + ")+" + employeeIdColumn1 + (topPaystubRow + 14).ToString();
            sheet3.Cells[topPaystubRow + 11, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "5" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 12, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "7" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 13, FirstDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 6).ToString() + separator + employeeInformationTable + separator + "7" + separator + "FALSE)*" + employeeIdColumn2 + (topPaystubRow + 12);
            sheet3.Cells[topPaystubRow + 4, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "6" + separator + "FALSE)*" + employeeIdColumn2 + (topPaystubRow + 12);
            sheet3.Cells[topPaystubRow + 5, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "12" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 6, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "10" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 7, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "3" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 8, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "5" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 9, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "4" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 10, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "6" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 11, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "8" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 12, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "9" + separator + "FALSE)+" + employeeIdColumn2 + (topPaystubRow + 7);
            sheet3.Cells[topPaystubRow + 13, SecondDataColumn].Formula = "=HLOOKUP(" + employeeIdColumn1 + (topPaystubRow + 5).ToString() + separator + payrollCalculatorTable + separator + "11" + separator + "FALSE)";
        }
        string GetPayrollCalculatorTable() {
            return sheet2["B5:K15"].GetReferenceA1(ReferenceElement.ColumnAbsolute | ReferenceElement.RowAbsolute | ReferenceElement.IncludeSheetName);
        }
        string GetEmployeeInformationTable() {
            return sheet1["C5:K17"].GetReferenceA1(ReferenceElement.ColumnAbsolute | ReferenceElement.RowAbsolute | ReferenceElement.IncludeSheetName);
        }
        void SetPaystubDataInHeader(int topPaystubRow) {
            string employeeInformationTable = GetEmployeeInformationTable();
            topPaystubRow++;
            string separator = new string(this.separator, 1);
            sheet3.Cells[topPaystubRow + 1, FirstHeaderColumn].Formula = "=HLOOKUP(" + "D" + (topPaystubRow + 5).ToString() + separator + employeeInformationTable + separator + "2" + separator + "FALSE)";
            sheet3.Cells[topPaystubRow + 2, FirstHeaderColumn].Value = "DevExpress";
            sheet3.Cells[topPaystubRow + 4, FirstHeaderColumn].Value = "EMPLOYEE ID";
            sheet3.Cells[topPaystubRow + 5, FirstHeaderColumn].Value = "Tax Status";
            sheet3.Cells[topPaystubRow + 6, FirstHeaderColumn].Value = "Hourly Rate";
            sheet3.Cells[topPaystubRow + 7, FirstHeaderColumn].Value = "Social Security Tax";
            sheet3.Cells[topPaystubRow + 8, FirstHeaderColumn].Value = "Medicare Tax";
            sheet3.Cells[topPaystubRow + 9, FirstHeaderColumn].Value = "Insurance Deduction";
            sheet3.Cells[topPaystubRow + 10, FirstHeaderColumn].Value = "Total Taxes and Regular Deductions ";
            sheet3.Cells[topPaystubRow + 11, FirstHeaderColumn].Value = "Federal Allowance (From W-4)";
            sheet3.Cells[topPaystubRow + 12, FirstHeaderColumn].Value = "Overtime Rate";
            sheet3.Cells[topPaystubRow + 13, FirstHeaderColumn].Value = "Federal Income Tax";
            sheet3.Cells[topPaystubRow + 1, SecondHeaderColumn + 1].Formula = "=CONCATENATE(\"Period: \"" + separator + "TEXT(NOW()" + separator + " \"" + shortDatePattern + "\"))";
            sheet3.Cells[topPaystubRow + 4, SecondHeaderColumn].Value = "State Tax";
            sheet3.Cells[topPaystubRow + 5, SecondHeaderColumn].Value = "Other Regular Deduction";
            sheet3.Cells[topPaystubRow + 6, SecondHeaderColumn].Value = "Other Deduction";
            sheet3.Cells[topPaystubRow + 7, SecondHeaderColumn].Value = "Hours Worked";
            sheet3.Cells[topPaystubRow + 8, SecondHeaderColumn].Value = "Sick Hours";
            sheet3.Cells[topPaystubRow + 9, SecondHeaderColumn].Value = "Vacation Hours";
            sheet3.Cells[topPaystubRow + 10, SecondHeaderColumn].Value = "Overtime Hours";
            sheet3.Cells[topPaystubRow + 11, SecondHeaderColumn].Value = "Gross Pay";
            sheet3.Cells[topPaystubRow + 12, SecondHeaderColumn].Value = "Total Taxes and Deductions";
            sheet3.Cells[topPaystubRow + 13, SecondHeaderColumn].Value = "NET PAY";
        }
        void AddStyles() {
            StyleCollection styles = book.Styles;
            Style style1 = styles.Add("Style 1");
            style1.Fill.PatternType = PatternType.Solid;
            style1.Fill.BackgroundColor = blueFillColor;
            style1.Font.Color = blueFontColor;
            style1.Font.Name = "Segoe UI";
            style1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            style1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            style1.Alignment.Indent = 2;

            Style style2 = styles.Add("Style 2");
            style2.Fill.PatternType = PatternType.Solid;
            style2.Fill.BackgroundColor = blueBorderColor;
            style2.Font.Color = blueFontColor;
            style2.Font.Name = "Segoe UI";
            style2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            style2.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            style2.Alignment.Indent = 2;

            Style style3 = styles.Add("Style 3");
            style3.Fill.PatternType = PatternType.Solid;
            style3.Fill.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            style3.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style3.Font.Name = "Segoe UI";
            style3.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            style3.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            style3.Flags.Number = false;
        }
        void ApplyStylesInPaystub(int topPaystubRow) {
            ApplyStylesInPaystub(topPaystubRow + 6, 0, topPaystubRow + 15);
            ApplyStylesInPaystub(topPaystubRow + 5, 3, topPaystubRow + 14);
        }
        void ApplyStylesInPaystub(int startPaystubRow, int leftPaystubColumn, int endPaystubRow) {
            for(int currentRow = startPaystubRow; currentRow < endPaystubRow; currentRow++) {
                if(currentRow % 2 != 0)
                    ApplyStyles2(leftPaystubColumn, currentRow);
                else {
                    ApplyStyles1(leftPaystubColumn, currentRow);
                    ApplyStyles3(leftPaystubColumn, currentRow);
                }
            }
        }
        void ApplyStyles3(int leftPaystubColumn, int currentRow) {
            if(book.Styles.Contains("Style 3"))
                sheet3.Range.FromLTRB(leftPaystubColumn + 3, currentRow, leftPaystubColumn + 3, currentRow).Style = book.Styles["Style 3"];
        }
        void ApplyStyles1(int leftPaystubColumn, int currentRow) {
            if(book.Styles.Contains("Style 1"))
                sheet3.Range.FromLTRB(leftPaystubColumn + 2, currentRow, leftPaystubColumn + 2, currentRow).Style = book.Styles["Style 1"];
        }
        void ApplyStyles2(int leftPaystubColumn, int currentRow) {
            if(book.Styles.Contains("Style 2"))
                sheet3.Range.FromLTRB(leftPaystubColumn + 2, currentRow, leftPaystubColumn + 2, currentRow).Style = book.Styles["Style 2"];
        }
        void FormatCellsInPaystub(int topPaystubRow) {
            FormatCellsInPaystubHeader(topPaystubRow);
            FormatCellsInPaystubTableHeader(topPaystubRow);
            FormatCellsInPaystubTableDataColumn(topPaystubRow);
        }
        void FormatCellsInPaystubTableHeader(int topHeaderRow) {
            Range range1 = sheet3.Range.FromLTRB(FirstHeaderColumn, topHeaderRow + 5, FirstHeaderColumn, topHeaderRow + 5);
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Fill.PatternType = PatternType.Solid;
            rangeFormatting1.Fill.BackgroundColor = blueBorderColor;
            rangeFormatting1.Font.Color = blueFontColor;
            rangeFormatting1.Font.Name = "Segoe UI Semibold";
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet3.Range.FromLTRB(FirstHeaderColumn, topHeaderRow + 5, FirstHeaderColumn, topHeaderRow + 14);
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting2.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting2.Alignment.Indent = 2;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range4 = sheet3.Range.FromLTRB(SecondHeaderColumn, topHeaderRow + 5, SecondHeaderColumn, topHeaderRow + 14);
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting4.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting4.Alignment.Indent = 2;
            range4.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet3.Range.FromLTRB(FirstHeaderColumn, topHeaderRow + 14, FirstHeaderColumn, topHeaderRow + 14);
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Fill.PatternType = PatternType.Solid;
            rangeFormatting3.Fill.BackgroundColor = blueFillColor;
            rangeFormatting3.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting3.Font.Color = blueFontColor;
            rangeFormatting3.Font.Size = 10.5;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range5 = sheet3.Range.FromLTRB(SecondHeaderColumn, topHeaderRow + 14, SecondHeaderColumn, topHeaderRow + 14);
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Fill.PatternType = PatternType.Solid;
            rangeFormatting5.Fill.BackgroundColor = blueFillColor;
            rangeFormatting5.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting5.Font.Color = blueFontColor;
            rangeFormatting5.Font.Size = 10.5;
            range5.EndUpdateFormatting(rangeFormatting3);
        }
        void FormatCellsInPaystubTableDataColumn(int topPaystubRow) {
            Range range1 = sheet3.Range.FromLTRB(FirstDataColumn, topPaystubRow + 5, FirstDataColumn, topPaystubRow + 14);
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            rangeFormatting1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range11 = sheet3.Range.FromLTRB(SecondDataColumn, topPaystubRow + 5, SecondDataColumn, topPaystubRow + 14);
            Formatting rangeFormatting11 = range11.BeginUpdateFormatting();
            rangeFormatting11.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            rangeFormatting11.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range11.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet3.Range.FromLTRB(SecondDataColumn, topPaystubRow + 14, SecondDataColumn, topPaystubRow + 14);
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Font.Color = Color.FromArgb(255, 26, 127, 169);
            rangeFormatting2.Fill.BackgroundColor = Color.FromArgb(255, 234, 244, 251);
            rangeFormatting2.Font.Size = 13;
            range2.EndUpdateFormatting(rangeFormatting2);

            SetMoneyFormat(FirstDataColumn, topPaystubRow + 7, FirstDataColumn, topPaystubRow + 11);
            SetMoneyFormat(FirstDataColumn, topPaystubRow + 13, FirstDataColumn, topPaystubRow + 14);
            SetMoneyFormat(SecondDataColumn, topPaystubRow + 5, SecondDataColumn, topPaystubRow + 7);
            SetMoneyFormat(SecondDataColumn, topPaystubRow + 12, SecondDataColumn, topPaystubRow + 14);
        }
        void SetMoneyFormat(int leftColumnIndex, int topRowIndex, int rightColumnIndex, int bottomRowIndex) {
            Range range = sheet3.Range.FromLTRB(leftColumnIndex, topRowIndex, rightColumnIndex, bottomRowIndex);
            Formatting rangeFormatting = range.BeginUpdateFormatting();
            rangeFormatting.NumberFormat = "\"$\"#,##0.00";
            range.EndUpdateFormatting(rangeFormatting);
        }
        void FormatCellsInPaystubHeader(int topPaystubRow) {
            Color grayFillColor = Color.FromArgb(255, 248, 248, 248);

            Range range1 = sheet3.Range.FromLTRB(1, topPaystubRow + 1, PaystubColumnCount, topPaystubRow + 15);
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Fill.PatternType = PatternType.Solid;
            rangeFormatting1.Fill.BackgroundColor = grayFillColor;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet3.Range.FromLTRB(2, topPaystubRow + 2, 3, topPaystubRow + 2);
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Fill.PatternType = PatternType.Solid;
            rangeFormatting2.Fill.BackgroundColor = grayFillColor;
            rangeFormatting2.Font.Color = darkBlueColor;
            rangeFormatting2.Font.Size = 27;
            rangeFormatting2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting2.Alignment.WrapText = true;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet3.Range.FromLTRB(2, topPaystubRow + 3, 2, topPaystubRow + 3);
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Font.Size = 15.5;
            rangeFormatting3.Font.Name = "Segoe UI Light";
            rangeFormatting2.Alignment.Vertical = SpreadsheetVerticalAlignment.Top;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range5 = sheet3.Range.FromLTRB(6, topPaystubRow + 2, 6, topPaystubRow + 2);
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Font.Color = darkBlueColor;
            rangeFormatting5.Font.Size = 12;
            rangeFormatting5.Alignment.Vertical = SpreadsheetVerticalAlignment.Top;
            rangeFormatting5.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
            range5.EndUpdateFormatting(rangeFormatting5);
        }
        void SetBordersInPaystub(int topPaystubRow) {
            Range range1 = sheet3.Range.FromLTRB(1, topPaystubRow + 1, PaystubColumnCount, topPaystubRow + 1);
            range1.Borders.TopBorder.Color = darkBlueColor;
            range1.Borders.TopBorder.LineStyle = BorderLineStyle.Thick;

            Range range2 = sheet3.Range.FromLTRB(5, topPaystubRow + 14, 6, topPaystubRow + 14);
            range2.Borders.TopBorder.Color = blueBorderColor;
            range2.Borders.TopBorder.LineStyle = BorderLineStyle.Thin;
            range2.Borders.BottomBorder.Color = blueBorderColor;
            range2.Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
        }
    }
}
