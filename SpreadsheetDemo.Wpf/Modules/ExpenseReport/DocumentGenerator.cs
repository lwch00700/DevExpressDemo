using System.Drawing;
using System.Globalization;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;

namespace DevExpress.Docs.Demos {
    public class ExpenseReportDocumentGenerator {
        IWorkbook book;
        Worksheet sheet;
        ColumnCollection columns;
        RowCollection rows;
        CellCollection cells;
        string shortDatePattern;
        char separator;

        public ExpenseReportDocumentGenerator() {
            this.book = new Workbook();
            InitCultureParams();
        }
        public ExpenseReportDocumentGenerator(IWorkbook book) {
            this.book = book;
            InitCultureParams();
        }
        void InitCultureParams() {
            CultureInfo culture = this.book.Options.Culture;
            this.shortDatePattern = culture.DateTimeFormat.ShortDatePattern;
            this.separator = culture.TextInfo.ListSeparator[0];
        }
        public IWorkbook Generate() {
            InitializeBook();
            return book;
        }

        void InitWorkbook() {
            book.Unit = Office.DocumentUnit.Point;

            book.Styles.DefaultStyle.Font.Name = "Segoe UI";
            book.Styles.DefaultStyle.Font.Size = 8;

            sheet = book.Worksheets[0];
            columns = sheet.Columns;
            rows = sheet.Rows;
            cells = sheet.Cells;

            sheet.DefaultRowHeight = 9.5;
            sheet.Name = "Expense report";

            sheet.ActiveView.ShowGridlines = false;
            sheet.ActiveView.Orientation = PageOrientation.Portrait;
            sheet.PrintOptions.FitToPage = true;

            sheet.DefinedNames.Add("_xlnm.Print_Area", "'Expense report'!B2:O30");
        }
        void InitializeBook() {
            InitWorkbook();
            SetRowHeight();
            SetColumnWidth();
            SetFont();
            CreateStyles();
            ApplyStyles();
            FormatCells();
            SetBorders();
            FillData();
            MergeCells();
        }
        void SetFont() {
            Range range1 = sheet["B2:O29"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Font.Color = Color.FromArgb(255, 0, 0, 0);
            rangeFormatting1.Font.Name = "Segoe UI";
            rangeFormatting1.Font.Size = 9;
            range1.EndUpdateFormatting(rangeFormatting1);
        }
        void FillData() {
            FillHeader();
            FillTable();
            FillFooter();
        }
        void CreateStyles() {
            StyleCollection styles = book.Styles;

            Style style1 = styles.Add("Style 1");
            style1.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            style1.Font.Color = Color.FromArgb(255, 180, 180, 180);
            style1.Font.Size = 9;
            style1.Font.Name = "Segoe UI";

            Style style2 = styles.Add("Style 2");
            style2.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            style2.Font.Color = Color.FromArgb(255, 52, 150, 151);
            style2.Font.Size = 9;
            style2.Font.Name = "Segoe UI";

            Style style3 = styles.Add("Style 3");
            style3.Fill.PatternType = PatternType.Solid;
            style3.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            style3.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style3.Flags.Font = false;

            Style style4 = styles.Add("Style 4");
            style4.Fill.PatternType = PatternType.Solid;
            style4.Fill.BackgroundColor = Color.FromArgb(255, 237, 237, 237);
            style4.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style4.Flags.Font = false;

            Style style5 = styles.Add("Style 5");
            style5.Fill.PatternType = PatternType.Solid;
            style5.Fill.BackgroundColor = Color.FromArgb(255, 241, 241, 241);
            style5.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style5.Flags.Font = false;

            Style style6 = styles.Add("Style 6");
            style6.Fill.PatternType = PatternType.Solid;
            style6.Fill.BackgroundColor = Color.FromArgb(255, 242, 252, 252);
            style6.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style6.Flags.Font = false;

            Style style7 = styles.Add("Style 7");
            style7.Fill.PatternType = PatternType.Solid;
            style7.Fill.BackgroundColor = Color.FromArgb(255, 229, 238, 238);
            style7.Font.Color = Color.FromArgb(255, 0, 0, 0);
            style7.Flags.Font = false;
        }
        void ApplyStyles() {
            ApplyStylesInHeader();
            ApplyStylesInTable();
            ApplyStylesInFooter();
        }
        void ApplyStylesInFooter() {
            sheet["B28"].Style = book.Styles["Style 1"];
            sheet["H28"].Style = book.Styles["Style 1"];
        }
        void ApplyStylesInTable() {
            sheet["B10:O10"].Style = book.Styles["Style 2"];
            sheet["F26:N26"].Style = book.Styles["Style 2"];
            sheet["O27:O29"].Style = book.Styles["Style 6"];

            ApplyStylesInTableRows();
        }
        void ApplyStylesInTableRows() {
            for (int rowIndex = 11; rowIndex < 26; rowIndex++) {
                if (rowIndex % 2 != 0)
                    ApplyStyleInUnevenTableRow(rowIndex);
                else
                    ApplyStyleInEvenTableRow(rowIndex);
            }
        }
        void ApplyStyleInEvenTableRow(int row) {
            sheet.Cells["B" + row].Style = book.Styles["Style 4"];
            sheet.Cells["D" + row].Style = book.Styles["Style 4"];
            sheet.Cells["H" + row].Style = book.Styles["Style 4"];
            sheet.Cells["J" + row].Style = book.Styles["Style 4"];
            sheet.Cells["M" + row].Style = book.Styles["Style 4"];

            sheet.Cells["C" + row].Style = book.Styles["Style 5"];
            sheet.Cells["F" + row].Style = book.Styles["Style 5"];
            sheet.Cells["I" + row].Style = book.Styles["Style 5"];
            sheet.Cells["L" + row].Style = book.Styles["Style 5"];
            sheet.Cells["N" + row].Style = book.Styles["Style 5"];

            sheet.Cells["O" + row].Style = book.Styles["Style 7"];
        }
        void ApplyStyleInUnevenTableRow(int row) {
            sheet.Cells["B" + row].Style = book.Styles["Style 3"];
            sheet.Cells["D" + row].Style = book.Styles["Style 3"];
            sheet.Cells["H" + row].Style = book.Styles["Style 3"];
            sheet.Cells["J" + row].Style = book.Styles["Style 3"];
            sheet.Cells["M" + row].Style = book.Styles["Style 3"];

            sheet.Cells["O" + row].Style = book.Styles["Style 6"];
        }
        void ApplyStylesInHeader() {
            sheet["B2:B4"].Style = book.Styles["Style 1"];
            sheet["B7:B8"].Style = book.Styles["Style 1"];
            sheet["E7:E8"].Style = book.Styles["Style 1"];
            sheet["I7:I8"].Style = book.Styles["Style 1"];
        }
        void FillTable() {
            FillTableHeader();
            FillTableRows();

            sheet["O11:O25"].Formula = "=SUM(F11:N11)";
            sheet["H26:I26"].Formula = "=SUM(H10:H25)";
            sheet["L26:N26"].Formula = "=SUM(L11:L25)";
            cells["J26"].Formula = "=SUM(J11:K25)";
            cells["F26"].Formula = "=SUM(F11:G25)";
            cells["O27"].Formula = "=SUM(O11:O25)";
            cells["O29"].Formula = "=(O27-O28)";

            cells["N27"].Value = "Subtotal:";
            cells["N28"].Value = "Advances:";
            cells["N29"].Value = "TOTAL:";
        }
        void FillTableRows() {
            FillTableRow(11, "=Now()-14", 212340, "Business trip", 250, 130, 12.42, 50, 8, 0, 19.3);
            FillTableRow(12, "=Now()-13", 289043, "Business trip", 250, 0, 26.6, 45, 7.8, 0, 29.3);
            FillTableRow(13, "=Now()-12", 212340, "Holiday", 0, 10, 0, 58, 2.79, 90, 12.3);
            FillTableRow(14, "=Now()-11", 216049, "Holiday", 0, 30, 0, 60, 9.79, 120, 122.3);
            FillTableRow(15, "=Now()-10", 292352, "Business trip", 295.5, 150, 10.48, 45, 9.32, 0, 59.0);
            FillTableRow(16, "=Now()-9", 567384, "Business trip", 295.5, 30, 20.37, 50, 9.12, 0, 30.07);
            FillTableRow(17, "=Now()-8", 890733, "Business trip", 349, 70, 15.07, 45, 14.05, 0, 100.93);
            FillTableRow(18, "=Now()-7", 578292, "Business trip", 349, 0, 6.8, 60, 3.7, 0, 302.8);
            FillTableRow(19, "=Now()-6", 199123, "Business trip", 349, 90, 13.6, 50, 2.6, 0, 23);
            FillTableRow(20, "=Now()-5", 423509, "Holiday", 0, 0, 37.5, 60, 2.04, 104.04, 20);
            FillTableRow(21, "=Now()-4", 543288, "Holiday", 0, 90, 14.2, 70, 0.2, 60.2, 12);
            FillTableRow(22, "=Now()-3", 457382, "Business trip", 205, 125, 16, 45, 14, 0, 35.39);
            FillTableRow(23, "=Now()-2", 584839, "Business trip", 205, 0, 10.03, 40, 12.01, 0, 30);
            FillTableRow(24, "=Now()-1", 483922, "Business trip", 205, 0, 26, 55, 9.2, 0, 60);
            FillTableRow(25, "=Now()", 890763, "Business trip", 205, 125, 9.5, 45, 1.03, 0, 143);
        }
        void FillTableHeader() {
            cells["B10"].Value = "Date";
            cells["C10"].Value = "Account";
            cells["D10"].Value = "Description";
            cells["F10"].Value = "Hotel";
            cells["H10"].Value = "Transport";
            cells["I10"].Value = "Fuel";
            cells["J10"].Value = "Meals";
            cells["L10"].Value = "Phone";
            cells["M10"].Value = "Entertainment";
            cells["N10"].Value = "Misc.";
            cells["O10"].Value = "Total";
        }
        void FillTableRow(int rowIndex, string date, double account, string description, double hotel, double transport, double fuel, double meals, double phone, double entertainment, double misc) {
            cells["B" + rowIndex].Formula = date;
            cells["C" + rowIndex].Value = account;
            cells["D" + rowIndex].Value = description;
            cells["F" + rowIndex].Value = hotel;
            cells["H" + rowIndex].Value = transport;
            cells["I" + rowIndex].Value = fuel;
            cells["J" + rowIndex].Value = meals;
            cells["L" + rowIndex].Value = phone;
            cells["M" + rowIndex].Value = entertainment;
            cells["N" + rowIndex].Value = misc;
        }
        void SetRowHeight() {
            SetRowHeightInHeader();
            SetRowHeightInTableRow();
            SetRowHeightInFooter();
        }
        void SetRowHeightInFooter() {
            sheet.Rows[25].Height = 31.5;
            sheet.Rows[26].Height = 31.5;
            sheet.Rows[27].Height = 31.5;
            sheet.Rows[28].Height = 31.5;
        }
        void SetRowHeightInHeader() {
            sheet.Rows[0].Height = 24;
            sheet.Rows[1].Height = 18.75;
            sheet.Rows[2].Height = 18.75;
            sheet.Rows[3].Height = 18.75;
            sheet.Rows[4].Height = 34.5;
            sheet.Rows[5].Height = 18.75;
            sheet.Rows[6].Height = 24.75;
            sheet.Rows[7].Height = 17.25;
            sheet.Rows[8].Height = 24.75;
            sheet.Rows[9].Height = 31.5;
        }
        void SetRowHeightInTableRow() {
            for (int i = 10; i < 25; i++)
                sheet.Rows[i].Height = 21;
        }
        void SetColumnWidth() {
            sheet.Columns[0].WidthInCharacters = 2.5;
            sheet.Columns[1].WidthInCharacters = 13;
            sheet.Columns[2].WidthInCharacters = 11.14063;
            sheet.Columns[3].WidthInCharacters = 8.140625;
            sheet.Columns[4].WidthInCharacters = 4.555469;
            sheet.Columns[5].WidthInCharacters = 6.570313;
            sheet.Columns[6].WidthInCharacters = 6;
            sheet.Columns[7].WidthInCharacters = 12;
            sheet.Columns[8].WidthInCharacters = 11.85547;
            sheet.Columns[9].WidthInCharacters = 3.425781;
            sheet.Columns[10].WidthInCharacters = 7.710938;
            sheet.Columns[11].WidthInCharacters = 11.5;
            sheet.Columns[12].WidthInCharacters = 15.42578;
            sheet.Columns[13].WidthInCharacters = 13;
            sheet.Columns[14].WidthInCharacters = 15;
        }
        void FillHeader() {
            cells["B2"].Value = "PURPOSE:";
            cells["D2"].Value = "HR-Conference";
            cells["J2"].Value = "EXPENSE REPORT";
            cells["B3"].Value = "STATEMENT NUMBER:";
            cells["D3"].Value = 534084310;
            cells["B4"].Value = "PAY PERIOD:";
            string separator = new string(this.separator, 1);
            cells["D4"].Formula = "=CONCATENATE(\"from \"" + separator +
                                  "TEXT(MIN(B11:B25)" + separator + " \"" + shortDatePattern + "\")" + separator +
                                  " \" to \"" + separator +
                                  " TEXT(MAX(B11:B25)" + separator + " \"" + shortDatePattern + "\"))";
            cells["B6"].Value = "EMPLOYEE INFORMATION:";
            cells["B7"].Value = "NAME:";
            cells["C7"].Value = "Tom Nilson";
            cells["E7"].Value = "POSITION:";
            cells["G7"].Value = "HR-manager";
            cells["I7"].Value = "SSN:";
            cells["K7"].Value = "078-05-1120";
            cells["B8"].Value = "DEPARTMENT:";
            cells["C8"].Value = "HR";
            cells["E8"].Value = "MANAGER:";
            cells["G8"].Value = "Nick Ellison";
            cells["I8"].Value = "EMPLOYEE ID:";
            cells["K8"].Value = 9547320;

            sheet["O12:O25"].Formula = "=SUM(F12:N12)";
            sheet["M26:N26"].Formula = "=SUM(M11:M25)";
        }
        void FillFooter() {
            cells["B28"].Value = "APPROVED:";
            cells["H28"].Value = "NOTES: ";
        }
        void FormatCells() {
            FormatCellsInHeader();
            FormatCellsInTable();
            FormatCellsInFooter();
        }
        void FormatCellsInHeader() {
            Range range1 = sheet["J2:O3"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Font.Color = Color.FromArgb(255, 52, 150, 151);
            rangeFormatting1.Font.Size = 31.5;
            rangeFormatting1.Font.Name = "Segoe UI Light";
            rangeFormatting1.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Center;
            rangeFormatting1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["D2:H4"];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet["E4:H4"];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting3.NumberFormat = shortDatePattern;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet["B6"];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting4.Font.Color = Color.FromArgb(255, 52, 150, 151);
            rangeFormatting4.Font.Size = 12;
            range4.EndUpdateFormatting(rangeFormatting4);

            Range range5 = sheet["C6:O6"];
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Font.Color = Color.FromArgb(255, 0, 0, 0);
            rangeFormatting5.Font.Name = "Segoe UI";
            range5.EndUpdateFormatting(rangeFormatting5);

            Range range6 = sheet["K7:K8"];
            Formatting rangeFormatting6 = range6.BeginUpdateFormatting();
            rangeFormatting6.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            range6.EndUpdateFormatting(rangeFormatting6);

            Range range7 = sheet["C7:C8"];
            Formatting rangeFormatting7 = range7.BeginUpdateFormatting();
            rangeFormatting7.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting7.Alignment.Indent = 2;
            range7.EndUpdateFormatting(rangeFormatting7);

            Range range8 = sheet["G7:G8"];
            Formatting rangeFormatting8 = range8.BeginUpdateFormatting();
            rangeFormatting8.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting8.Alignment.Indent = 1;
            range8.EndUpdateFormatting(rangeFormatting8);
        }
        void FormatCellsInTable() {
            Range range1 = sheet["B10"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Fill.PatternType = PatternType.Solid;
            rangeFormatting1.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            rangeFormatting1.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Center;
            rangeFormatting1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["C10"];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Center;
            rangeFormatting2.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet["D10:E10"];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Fill.PatternType = PatternType.Solid;
            rangeFormatting3.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            rangeFormatting3.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet["F10:O26"];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting4.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting4.Alignment.Indent = 1;
            range4.EndUpdateFormatting(rangeFormatting4);

            Range range5 = sheet["H10"];
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Fill.PatternType = PatternType.Solid;
            rangeFormatting5.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            range5.EndUpdateFormatting(rangeFormatting5);

            Range range6 = sheet["J10:K10"];
            Formatting rangeFormatting6 = range6.BeginUpdateFormatting();
            rangeFormatting6.Fill.PatternType = PatternType.Solid;
            rangeFormatting6.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            range6.EndUpdateFormatting(rangeFormatting6);

            Range range7 = sheet["M10"];
            Formatting rangeFormatting7 = range7.BeginUpdateFormatting();
            rangeFormatting7.Fill.PatternType = PatternType.Solid;
            rangeFormatting7.Fill.BackgroundColor = Color.FromArgb(255, 251, 251, 251);
            range7.EndUpdateFormatting(rangeFormatting7);

            Range range8 = sheet["O10"];
            Formatting rangeFormatting8 = range8.BeginUpdateFormatting();
            rangeFormatting8.Fill.PatternType = PatternType.Solid;
            rangeFormatting8.Fill.BackgroundColor = Color.FromArgb(255, 242, 252, 252);
            range8.EndUpdateFormatting(rangeFormatting8);

            Range range9 = sheet["B11:B25"];
            Formatting rangeFormatting9 = range9.BeginUpdateFormatting();
            rangeFormatting9.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Center;
            rangeFormatting9.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting9.NumberFormat = shortDatePattern;
            range9.EndUpdateFormatting(rangeFormatting9);

            Range range10 = sheet["C11:C25"];
            Formatting rangeFormatting10 = range10.BeginUpdateFormatting();
            rangeFormatting10.Font.Color = Color.FromArgb(255, 0, 0, 0);
            rangeFormatting10.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Center;
            rangeFormatting10.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range10.EndUpdateFormatting(rangeFormatting10);

            Range range11 = sheet["D10:E25"];
            Formatting rangeFormatting11 = range11.BeginUpdateFormatting();
            rangeFormatting11.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting11.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting11.Alignment.Indent = 1;
            range11.EndUpdateFormatting(rangeFormatting11);

            Range range12 = sheet["F11:O25"];
            Formatting rangeFormatting12 = range12.BeginUpdateFormatting();
            rangeFormatting12.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting12.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting12.Alignment.Indent = 1;
            rangeFormatting12.NumberFormat = "_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(_)";
            range12.EndUpdateFormatting(rangeFormatting12);

            Range range13 = sheet["O11:O29"];
            Formatting rangeFormatting13 = range13.BeginUpdateFormatting();
            rangeFormatting13.Font.Color = Color.FromArgb(255, 52, 150, 151);
            range13.EndUpdateFormatting(rangeFormatting13);
        }
        void FormatCellsInFooter() {
            Range range1 = sheet["F26:N26"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Fill.PatternType = PatternType.Solid;
            rangeFormatting1.Fill.BackgroundColor = Color.FromArgb(255, 242, 252, 252);
            rangeFormatting1.NumberFormat = "_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(_)";
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["O27:O29"];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Font.Color = Color.FromArgb(255, 52, 150, 151);
            rangeFormatting2.Font.Size = 9;
            rangeFormatting2.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Left;
            rangeFormatting2.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting2.Alignment.Indent = 1;
            rangeFormatting2.NumberFormat = "_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(_)";
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet["N27:N28"];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting3.Font.Color = Color.FromArgb(255, 52, 150, 151);
            rangeFormatting3.Font.Size = 9;
            rangeFormatting3.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Right;
            rangeFormatting3.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting3.Alignment.Indent = 1;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet["O26"];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Fill.PatternType = PatternType.Solid;
            rangeFormatting4.Fill.BackgroundColor = Color.FromArgb(255, 230, 249, 249);
            range4.EndUpdateFormatting(rangeFormatting4);

            Range range5 = sheet["O26:O28"];
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Font.Size = 9;
            rangeFormatting5.Font.FontStyle = SpreadsheetFontStyle.Bold;
            range5.EndUpdateFormatting(rangeFormatting5);

            Range range6 = sheet["N29"];
            Formatting rangeFormatting6 = range6.BeginUpdateFormatting();
            rangeFormatting6.Font.FontStyle = DevExpress.Spreadsheet.SpreadsheetFontStyle.Bold;
            rangeFormatting6.Font.Color = Color.FromArgb(255, 52, 150, 151);
            rangeFormatting6.Font.Size = 10.5;
            rangeFormatting6.Alignment.Horizontal =  SpreadsheetHorizontalAlignment.Right;
            rangeFormatting6.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting6.Alignment.Indent = 1;
            range6.EndUpdateFormatting(rangeFormatting6);

            Range range7 = sheet["O29"];
            Formatting rangeFormatting7 = range7.BeginUpdateFormatting();
            rangeFormatting7.Font.Size = 10.5;
            rangeFormatting7.Font.FontStyle = SpreadsheetFontStyle.Bold;
            range7.EndUpdateFormatting(rangeFormatting7);
        }
        void SetBorders() {
            sheet["B10:O10"].Borders.BottomBorder.Color = Color.FromArgb(255, 52, 150, 151);
            sheet["B10:O10"].Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;

            sheet["C28:F28"].Borders.BottomBorder.Color = Color.FromArgb(255, 180, 180, 180);
            sheet["C28:F28"].Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;

            sheet["C29:F29"].Borders.BottomBorder.Color = Color.FromArgb(255, 180, 180, 180);
            sheet["C29:F29"].Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;

            sheet["I28:L28"].Borders.BottomBorder.Color = Color.FromArgb(255, 180, 180, 180);
            sheet["I28:L28"].Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;

            sheet["I29:L29"].Borders.BottomBorder.Color = Color.FromArgb(255, 180, 180, 180);
            sheet["I29:L29"].Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
        }
        void MergeCells() {
            for (int i = 10; i < 27; i++) {
                sheet["D" + i + ":E" + i].Merge();
                sheet["F" + i + ":G" + i].Merge();
                sheet["J" + i + ":K" + i].Merge();
            }
            sheet["K8:L8"].Merge();
            sheet["J2:O3"].Merge();
            sheet["D4:H4"].Merge();
            sheet["D3:E3"].Merge();
            sheet["C28:F28"].Merge();
            sheet["C29:F29"].Merge();
            sheet["I28:L28"].Merge();
            sheet["I29:L29"].Merge();
        }
    }
}
