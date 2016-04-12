using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using System.Drawing;
using SpreadsheetFormatting = DevExpress.Spreadsheet.Formatting;
using System.Globalization;
using DevExpress.Spreadsheet.Functions;
using DevExpress.Docs.Text;

namespace DevExpress.Spreadsheet.Demos
{
    internal class HomeAccountingDocumentGenerator {
        IWorkbook workbook;

        const string Accounting = "_(\\$* #,##0.00_);_(\\$ (#,##0.00);_(\\$* \" - \"??_);_(@_)";

        readonly HashSet<String> incomeCategorys = new HashSet<string>();
        readonly HashSet<String> expenseCategorys = new HashSet<string>();

        public HashSet<string> IncomeCategorys { get { return incomeCategorys; } }
        public HashSet<string> ExpenseCategorys { get { return expenseCategorys; } }

        public HomeAccountingDocumentGenerator(IWorkbook workbook) {
            this.workbook = workbook;
            incomeCategorys.Add("Uncategorised");
            expenseCategorys.Add("Uncategorised");
        }
        public void Generate() {
            InitializeWorkbook();
        }

        void InitializeWorkbook() {
            workbook.Unit = DevExpress.Office.DocumentUnit.Point;

            workbook.Styles.DefaultStyle.Font.Name = "Calibri";
            workbook.Styles.DefaultStyle.Font.Size = 11;

            GenerateWorksheets();
            InitializeSummary();
            AddPayments();

            AddDefinedNames();

        }
        void GenerateWorksheets() {
            workbook.Worksheets[0].Name = "January";
            workbook.Worksheets.Add("February");
            workbook.Worksheets.Add("March");
            workbook.Worksheets.Add("April");
            workbook.Worksheets.Add("May");
            workbook.Worksheets.Add("June");
            workbook.Worksheets.Add("July");
            workbook.Worksheets.Add("August");
            workbook.Worksheets.Add("September");
            workbook.Worksheets.Add("October");
            workbook.Worksheets.Add("November");
            workbook.Worksheets.Add("December");

            for (int i = 0; i < 12; i++) {
                Worksheet worksheet = workbook.Worksheets[i];
                worksheet.ActiveView.ShowGridlines = false;
                worksheet.DefaultRowHeight = 21;
                worksheet.DefaultColumnWidthInCharacters = 18;
                CellCollection cells = worksheet.Cells;

                worksheet.Rows[0].Height = 3;
                worksheet.Rows[1].Height = 18;
                worksheet.Rows[2].Height = 18;
                worksheet.Rows[3].Height = 32;
                worksheet.Rows[4].Height = 34;

                worksheet.Columns["A"].WidthInCharacters = 1;
                worksheet.Columns["B"].WidthInCharacters = 4;
                worksheet.Columns["C"].WidthInCharacters = 4;
                worksheet.Columns["G"].WidthInCharacters = 4;
                worksheet.Columns["H"].WidthInCharacters = 4;

                worksheet.DefinedNames.Add("BalanceAtTheBeginningOfTheMonth", "=" + worksheet.Name + "!$K$2");
                worksheet.DefinedNames.Add("BalanceAtTheEndOfTheMonth", "=" + worksheet.Name + "!$K$3");

                Range formattedRange = worksheet["B2:E3"];
                formattedRange.Merge();
                Formatting formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                    formatting.Font.Size = 32;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }

                cells["B2"].Value = worksheet.Name.ToUpper();

                formattedRange = worksheet["B4:E4"];
                formattedRange.Merge();
                cells["B4"].Font.Color = Color.FromArgb(114, 164, 77);
                cells["B4"].Font.Size = 18;
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                    formatting.Borders.BottomBorder.Color = Color.FromArgb(114, 164, 77);
                    formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }

                formattedRange = worksheet["B6:E6"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Borders.RightBorder.Color = Color.FromArgb(143, 192, 106);
                    formatting.Borders.RightBorder.LineStyle = BorderLineStyle.Thin;
                    formatting.Borders.LeftBorder.Color = Color.FromArgb(143, 192, 106);
                    formatting.Borders.LeftBorder.LineStyle = BorderLineStyle.Thin;
                    formatting.Borders.BottomBorder.Color = Color.FromArgb(125, 150, 83);
                    formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thick;
                    formatting.Font.Color = Color.White;
                    formatting.Font.Bold = true;
                    formatting.Fill.BackgroundColor = Color.FromArgb(143, 192, 106);
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }
                cells["D6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["D6"].Alignment.Indent = 2;
                cells["E6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                cells["B4"].Value = "Income";
                cells["B6"].Value = "№";
                cells["C6"].Value = "Day";
                cells["D6"].Value = "Category";
                cells["E6"].Value = "Sum";

                formattedRange = worksheet["G4:J4"];
                formattedRange.Merge();
                cells["G4"].Font.Color = Color.FromArgb(228, 63, 45);
                cells["G4"].Font.Size = 18;
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                    formatting.Borders.BottomBorder.Color = Color.FromArgb(228, 63, 45);
                    formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }

                formattedRange = worksheet["G6:J6"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Borders.RightBorder.Color = Color.FromArgb(248, 107, 91);
                    formatting.Borders.RightBorder.LineStyle = BorderLineStyle.Thin;
                    formatting.Borders.LeftBorder.Color = Color.FromArgb(248, 107, 91);
                    formatting.Borders.LeftBorder.LineStyle = BorderLineStyle.Thin;
                    formatting.Borders.BottomBorder.Color = Color.FromArgb(169, 73, 62);
                    formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thick;
                    formatting.Font.Color = Color.White;
                    formatting.Font.Bold = true;
                    formatting.Fill.BackgroundColor = Color.FromArgb(248, 107, 91);
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }
                cells["I6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["I6"].Alignment.Indent = 2;
                cells["J6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                cells["G4"].Value = "Expenses";
                cells["G6"].Value = "№";
                cells["H6"].Value = "Day";
                cells["I6"].Value = "Category";
                cells["J6"].Value = "Sum";

                formattedRange = worksheet["L5:O4"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Font.Size = 18;
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Alignment.Indent = 1;
                    formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }

                cells["L5"].Font.Color = Color.FromArgb(0, 0, 0);
                cells["L5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["L5"].Borders.BottomBorder.Color = Color.FromArgb(203, 203, 203);

                cells["M5"].Font.Color = Color.FromArgb(114, 164, 77);
                cells["M5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                cells["M5"].Borders.BottomBorder.Color = Color.FromArgb(114, 164, 77);

                cells["N5"].Font.Color = Color.FromArgb(0, 0, 0);
                cells["N5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["N5"].Borders.BottomBorder.Color = Color.FromArgb(203, 203, 203);

                cells["O5"].Font.Color = Color.FromArgb(228, 63, 45);
                cells["O5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                cells["O5"].Borders.BottomBorder.Color = Color.FromArgb(169, 73, 62);

                cells["L5"].Value = "Category";
                cells["M5"].Value = "Income";
                cells["N5"].Value = "Category";
                cells["O5"].Value = "Expenses";

                worksheet["B5:D5"].Merge();
                worksheet["G5:I5"].Merge();

                formattedRange = worksheet["B5:E5"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Font.Size = 14;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }

                cells["B5"].Value = "Total income:";
                cells["G5"].Value = "Total expenses:";

                formattedRange = worksheet["G5:J5"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                    formatting.Font.Size = 14;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }


                cells["E5"].Formula = "=SUM(E7:E100)";
                cells["E5"].Font.Bold = true;
                cells["E5"].Font.Color = Color.FromArgb(114, 164, 77);
                cells["E5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                cells["J5"].Formula = "=SUM(J7:J100)";
                cells["J5"].Font.Bold = true;
                cells["J5"].Font.Color = Color.FromArgb(228, 63, 45);
                cells["J5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                cells["G2"].Value = "Balance at the beginning of the month:";
                cells["G3"].Value = "Balance at the end of the month:";

                worksheet["G2:J2"].Merge();
                worksheet["G3:J3"].Merge();

                cells["E5"].NumberFormat = Accounting;
                cells["J5"].NumberFormat = Accounting;
                cells["K2"].NumberFormat = Accounting;
                cells["K3"].NumberFormat = Accounting;
                formattedRange = worksheet["K2:K3"];
                formatting = formattedRange.BeginUpdateFormatting();
                try {
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }
                if (i == 0) {
                    cells["K2"].Formula = "=BalanceAtTheBeginningOfTheYear";
                } else {
                    cells["K2"].Formula = "=" + workbook.Worksheets[i - 1].Name + "!BalanceAtTheEndOfTheMonth";
                }
                cells["K3"].Formula =
                    "=BalanceAtTheBeginningOfTheMonth+TotalIncomeFor" + worksheet.Name + "-TotalExpenseFor" + worksheet.Name;
            }
        }
        void InitializeSummary() {
            Worksheet worksheet = workbook.Worksheets.Add("Summary");
            worksheet.ActiveView.ShowGridlines = false;
            CellCollection cells = worksheet.Cells;

            worksheet.DefaultRowHeight = 21;
            worksheet.DefaultColumnWidthInCharacters = 14;

            worksheet.Rows[0].Height = 3;
            worksheet.Rows[1].Height = 18;
            worksheet.Rows[2].Height = 18;
            worksheet.Rows[3].Height = 32;
            worksheet.Rows[4].Height = 34;

            worksheet.Columns["A"].WidthInCharacters = 1;
            worksheet.Columns["B"].WidthInCharacters = 18;
            worksheet.Columns["C"].WidthInCharacters = 20;
            worksheet.Columns["D"].WidthInCharacters = 18;
            worksheet.Columns["E"].WidthInCharacters = 20;

            Range formattedRange = worksheet["B2:E3"];
            formattedRange.Merge();
            Formatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                formatting.Font.Size = 32;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }
            cells["B2"].Value = "SUMMARY REPORT";

            worksheet["B4:C4"].Merge();
            cells["B4"].Value = "Balance at the beginning of the year:";
            cells["B4"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

            cells["D4"].NumberFormat = Accounting;
            cells["D4"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;

            cells["B5"].Value = "Category";
            cells["C5"].Value = "Income";
            cells["D5"].Value = "Category";
            cells["E5"].Value = "Expenses";

            formattedRange = worksheet["B5:E5"];
            formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Font.Size = 18;
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                formatting.Alignment.Indent = 1;
                formatting.Borders.BottomBorder.LineStyle = BorderLineStyle.Thin;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            cells["B5"].Font.Color = Color.FromArgb(0, 0, 0);
            cells["B5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            cells["B5"].Borders.BottomBorder.Color = Color.FromArgb(203, 203, 203);

            cells["C5"].Font.Color = Color.FromArgb(114, 164, 77);
            cells["C5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
            cells["C5"].Borders.BottomBorder.Color = Color.FromArgb(114, 164, 77);

            cells["D5"].Font.Color = Color.FromArgb(0, 0, 0);
            cells["D5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            cells["D5"].Borders.BottomBorder.Color = Color.FromArgb(203, 203, 203);

            cells["E5"].Font.Color = Color.FromArgb(228, 63, 45);
            cells["E5"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
            cells["E5"].Borders.BottomBorder.Color = Color.FromArgb(169, 73, 62);

            cells["B6"].Value = "Total income:";
            cells["D6"].Value = "Total expenses:";

            formattedRange = worksheet["B6:E6"];
            formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                formatting.Font.Size = 14;
                formatting.Alignment.Indent = 1;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            cells["C6"].Font.Bold = true;
            cells["C6"].Font.Color = Color.FromArgb(114, 164, 77);
            cells["C6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

            cells["E6"].Font.Bold = true;
            cells["E6"].Font.Color = Color.FromArgb(228, 63, 45);
            cells["E6"].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

            cells["C6"].Formula = "=TotalIncomeForJanuary" +
                                  "+TotalIncomeForFebruary" +
                                  "+TotalIncomeForMarch" +
                                  "+TotalIncomeForApril" +
                                  "+TotalIncomeForMay" +
                                  "+TotalIncomeForJune" +
                                  "+TotalIncomeForJuly" +
                                  "+TotalIncomeForAugust" +
                                  "+TotalIncomeForSeptember" +
                                  "+TotalIncomeForOctober" +
                                  "+TotalIncomeForNovember" +
                                  "+TotalIncomeForDecember";

            cells["E6"].Formula = "=TotalExpenseForJanuary" +
                                  "+TotalExpenseForFebruary" +
                                  "+TotalExpenseForMarch" +
                                  "+TotalExpenseForApril" +
                                  "+TotalExpenseForMay" +
                                  "+TotalExpenseForJune" +
                                  "+TotalExpenseForJuly" +
                                  "+TotalExpenseForAugust" +
                                  "+TotalExpenseForSeptember" +
                                  "+TotalExpenseForOctober" +
                                  "+TotalExpenseForNovember" +
                                  "+TotalExpenseForDecember";


            cells["C6"].NumberFormat = Accounting;
            cells["E6"].NumberFormat = Accounting;
        }
        void AddDefinedNames() {
            workbook.DefinedNames.Add("BalanceAtTheBeginningOfTheYear", "=Summary!$D$4");
            workbook.DefinedNames.Add("TotalIncomeForJanuary", "=January!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForFebruary", "=February!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForMarch", "=March!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForApril", "=April!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForMay", "=May!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForJune", "=June!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForJuly", "=July!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForAugust", "=August!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForSeptember", "=September!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForOctober", "=October!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForNovember", "=November!$E$5");
            workbook.DefinedNames.Add("TotalIncomeForDecember", "=December!$E$5");

            workbook.DefinedNames.Add("TotalExpenseForJanuary", "=January!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForFebruary", "=February!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForMarch", "=March!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForApril", "=April!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForMay", "=May!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForJune", "=June!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForJuly", "=July!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForAugust", "=August!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForSeptember", "=September!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForOctober", "=October!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForNovember", "=November!$J$5");
            workbook.DefinedNames.Add("TotalExpenseForDecember", "=December!$J$5");

            workbook.DefinedNames.Add("IncomeCategorysInJanuary", "=January!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInFebruary", "=February!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInMarch", "=March!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInApril", "=April!$L$6:$LK$100");
            workbook.DefinedNames.Add("IncomeCategorysInMay", "=May!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInJune", "=June!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInJuly", "=July!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInAugust", "=August!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInSeptember", "=September!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInOctober", "=October!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInNovember", "=November!$L$6:$L$100");
            workbook.DefinedNames.Add("IncomeCategorysInDecember", "=December!$L$6:$L$100");

            workbook.DefinedNames.Add("IncomeByCategoryInJanuary", "=January!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInFebruary", "=February!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInMarch", "=March!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInApril", "=April!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInMay", "=May!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInJune", "=June!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInJuly", "=July!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInAugust", "=August!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInSeptember", "=September!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInOctober", "=October!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInNovember", "=November!$M$6:$M$100");
            workbook.DefinedNames.Add("IncomeByCategoryInDecember", "=December!$M$6:$M$100");

            workbook.DefinedNames.Add("ExpenseCategorysInJanuary", "=January!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInFebruary", "=February!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInMarch", "=March!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInApril", "=April!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInMay", "=May!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInJune", "=June!$N$6:$M$100");
            workbook.DefinedNames.Add("ExpenseCategorysInJuly", "=July!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInAugust", "=August!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInSeptember", "=September!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInOctober", "=October!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInNovember", "=November!$N$6:$N$100");
            workbook.DefinedNames.Add("ExpenseCategorysInDecember", "=December!$N$6:$N$100");

            workbook.DefinedNames.Add("ExpenseByCategoryInJanuary", "=January!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInFebruary", "=February!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInMarch", "=March!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInApril", "=April!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInMay", "=May!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInJune", "=June!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInJuly", "=July!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInAugust", "=August!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInSeptember", "=September!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInOctober", "=October!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInNovember", "=November!$O$6:$O$100");
            workbook.DefinedNames.Add("ExpenseByCategoryInDecember", "=December!$O$6:$O$100");

        }
        public void AddTransaction(bool income, DateTime date, float sum, string category) {
            if (income) {
                if (!incomeCategorys.Contains(category)) {
                    incomeCategorys.Add(category);
                    UpdateCategorys();
                }
            } else {
                if (!expenseCategorys.Contains(category)) {
                    expenseCategorys.Add(category);
                    UpdateCategorys();
                }
            }
            Worksheet worksheet = workbook.Worksheets[date.Month - 1];
            CellCollection cells = worksheet.Cells;
            int i = 7;
            while ((income && !Equals(cells["C" + i].Value, CellValue.Empty)) ||
                  (!income && !Equals(cells["H" + i].Value, CellValue.Empty)))
                i++;

            if (income) {
                if (i == 7) cells["B7"].Formula = "=1";
                else cells["B" + i].Formula = "=B" + (i - 1) + "+1";
                cells["C" + i].Value = date.Day;
                cells["D" + i].Value = category;
                cells["E" + i].Value = sum;

                cells["E" + i].NumberFormat = Accounting;

                Range formattedRange = worksheet["B" + i + ":E" + i];
                Formatting formatting = formattedRange.BeginUpdateFormatting();
                try {
                    if (i % 2 == 0) formatting.Fill.BackgroundColor = Color.FromArgb(223, 245, 207);
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }
                cells["D" + i].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["D" + i].Alignment.Indent = 2;
                cells["E" + i].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                worksheet.Columns["E"].AutoFit();

                worksheet.Selection = worksheet["B" + i + ":E" + i];
            } else {
                if (i == 7) cells["G7"].Formula = "=1";
                else cells["G" + i].Formula = "=G" + (i - 1) + "+1";
                cells["H" + i].Value = date.Day;
                cells["I" + i].Value = category;
                cells["J" + i].Value = sum;

                cells["J" + i].NumberFormat = Accounting;

                Range formattedRange = worksheet["G" + i + ":J" + i];
                Formatting formatting = formattedRange.BeginUpdateFormatting();
                try {
                    if (i % 2 == 0) formatting.Fill.BackgroundColor = Color.FromArgb(255, 219, 215);
                    formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                    formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                }
                finally {
                    formattedRange.EndUpdateFormatting(formatting);
                }
                cells["I" + i].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["I" + i].Alignment.Indent = 2;
                cells["J" + i].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;

                worksheet.Columns["J"].AutoFit();

                worksheet.Selection = worksheet["G" + i + ":J" + i];
            }
            workbook.Worksheets.ActiveWorksheet = worksheet;
        }
        void AddPayments() {
            workbook.Worksheets["Summary"].Cells["D4"].Value = 10000;

            AddTransaction(true, new DateTime(2013, 1, 15), 13000, "Salary");
            AddTransaction(true, new DateTime(2013, 2, 15), 13000, "Salary");
            AddTransaction(true, new DateTime(2013, 3, 15), 13000, "Salary");
            AddTransaction(true, new DateTime(2013, 4, 15), 13000, "Salary");
            AddTransaction(true, new DateTime(2013, 5, 15), 13000, "Salary");
            AddTransaction(true, new DateTime(2013, 1, 30), 21300, "Salary");
            AddTransaction(true, new DateTime(2013, 2, 28), 21300, "Salary");
            AddTransaction(true, new DateTime(2013, 3, 30), 21300, "Salary");
            AddTransaction(true, new DateTime(2013, 4, 30), 21300, "Salary");
            AddTransaction(true, new DateTime(2013, 5, 30), 21300, "Salary");
            AddTransaction(true, new DateTime(2013, 1, 8), 80, "Scholarship");
            AddTransaction(true, new DateTime(2013, 2, 8), 80, "Scholarship");
            AddTransaction(true, new DateTime(2013, 3, 8), 80, "Scholarship");
            AddTransaction(true, new DateTime(2013, 4, 8), 80, "Scholarship");
            AddTransaction(true, new DateTime(2013, 5, 8), 80, "Scholarship");

            byte[] months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            for (int month = 0; month < 5; month++) {
                for (int day = 1; day <= months[month]; day++) {
                    AddTransaction(false, new DateTime(2013, month + 1, day), 126, "Transportation");
                }
            }

            for (int month = 0; month < 5; month++) {
                AddTransaction(false, new DateTime(2013, month + 1, 2), 2400, "Credit");
            }

            for (int month = 0; month < 5; month++) {
                AddTransaction(false, new DateTime(2013, month + 1, 12), 11000, "Apartment");
            }
        }
        void UpdateCategorys() {
            Worksheet worksheet;
            CellCollection cells;
            string[] incomeCategorysArray = incomeCategorys.ToArray();
            string[] expenseCategorysArray = expenseCategorys.ToArray();
            for (int i = 0; i < 12; i++) {
                worksheet = workbook.Worksheets[i];
                cells = worksheet.Cells;
                for (int j = 0; j < incomeCategorysArray.Length; j++) {
                    string index = (6 + j).ToString();
                    cells["L" + index].Value = incomeCategorysArray[j];
                    cells["M" + index].Formula = "=SUMIF(D7:D100,\"" + incomeCategorysArray[j] + "\",E7:E100)";

                    cells["M" + index].NumberFormat = Accounting;

                    cells["L" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                    cells["L" + index].Alignment.Indent = 1;
                    cells["M" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                    cells["M" + index].Alignment.Indent = 1;
                    if (j % 2 == 1) {
                        cells["L" + index].Fill.BackgroundColor = Color.FromArgb(242, 242, 242);
                        cells["M" + index].Fill.BackgroundColor = Color.FromArgb(223, 245, 207);
                    }
                }
                for (int j = 0; j < expenseCategorysArray.Length; j++) {
                    string index = (6 + j).ToString();
                    cells["N" + index].Value = expenseCategorysArray[j];
                    cells["O" + index].Formula = "=SUMIF(I6:I100,\"" + expenseCategorysArray[j] + "\",J6:J100)";

                    cells["O" + index].NumberFormat = Accounting;

                    cells["N" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                    cells["N" + index].Alignment.Indent = 1;
                    cells["O" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                    cells["O" + index].Alignment.Indent = 1;
                    if (j % 2 == 1) {
                        cells["N" + index].Fill.BackgroundColor = Color.FromArgb(242, 242, 242);
                        cells["O" + index].Fill.BackgroundColor = Color.FromArgb(255, 219, 215);
                    }
                }
            }

            worksheet = workbook.Worksheets["Summary"];
            cells = worksheet.Cells;
            for (int i = 0; i < incomeCategorysArray.Length; i++) {
                string index = (7 + i).ToString();
                cells["B" + index].Value = incomeCategorysArray[i];
                cells["C" + index].Formula = "=SUMIF(IncomeCategorysInJanuary,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInJanuary)+" +
                                             "SUMIF(IncomeCategorysInFebruary,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInFebruary)+" +
                                             "SUMIF(IncomeCategorysInMarch,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInMarch)+" +
                                             "SUMIF(IncomeCategorysInApril,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInApril)+" +
                                             "SUMIF(IncomeCategorysInMay,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInMay)+" +
                                             "SUMIF(IncomeCategorysInJune,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInJune)+" +
                                             "SUMIF(IncomeCategorysInJuly,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInJuly)+" +
                                             "SUMIF(IncomeCategorysInAugust,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInAugust)+" +
                                             "SUMIF(IncomeCategorysInSeptember,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInSeptember)+" +
                                             "SUMIF(IncomeCategorysInOctober,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInOctober)+" +
                                             "SUMIF(IncomeCategorysInNovember,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInNovember)+" +
                                             "SUMIF(IncomeCategorysInDecember,\"" + incomeCategorysArray[i] + "\",IncomeByCategoryInDecember)";
                cells["C" + index].NumberFormat = Accounting;

                cells["B" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["B" + index].Alignment.Indent = 1;
                cells["C" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                cells["C" + index].Alignment.Indent = 1;
                if (i % 2 == 1) {
                    cells["B" + index].Fill.BackgroundColor = Color.FromArgb(242, 242, 242);
                    cells["C" + index].Fill.BackgroundColor = Color.FromArgb(223, 245, 207);
                }
            }
            for (int i = 0; i < expenseCategorysArray.Length; i++) {
                string index = (7 + i).ToString();
                cells["D" + index].Value = expenseCategorysArray[i];
                cells["E" + index].Formula = "=SUMIF(ExpenseCategorysInJanuary,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInJanuary)+" +
                                             "SUMIF(ExpenseCategorysInFebruary,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInFebruary)+" +
                                             "SUMIF(ExpenseCategorysInMarch,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInMarch)+" +
                                             "SUMIF(ExpenseCategorysInApril,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInApril)+" +
                                             "SUMIF(ExpenseCategorysInMay,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInMay)+" +
                                             "SUMIF(ExpenseCategorysInJune,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInJune)+" +
                                             "SUMIF(ExpenseCategorysInJuly,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInJuly)+" +
                                             "SUMIF(ExpenseCategorysInAugust,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInAugust)+" +
                                             "SUMIF(ExpenseCategorysInSeptember,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInSeptember)+" +
                                             "SUMIF(ExpenseCategorysInOctober,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInOctober)+" +
                                             "SUMIF(ExpenseCategorysInNovember,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInNovember)+" +
                                             "SUMIF(ExpenseCategorysInDecember,\"" + expenseCategorysArray[i] + "\",ExpenseByCategoryInDecember)";

                cells["E" + index].NumberFormat = Accounting;

                cells["D" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                cells["D" + index].Alignment.Indent = 1;
                cells["E" + index].Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                cells["E" + index].Alignment.Indent = 1;
                if (i % 2 == 1) {
                    cells["D" + index].Fill.BackgroundColor = Color.FromArgb(242, 242, 242);
                    cells["E" + index].Fill.BackgroundColor = Color.FromArgb(255, 219, 215);
                }
            }
        }
    }
}
