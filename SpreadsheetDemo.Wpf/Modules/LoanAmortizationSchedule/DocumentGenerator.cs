using System;
using DevExpress.Office;
using System.Drawing;
using SpreadsheetFormatting = DevExpress.Spreadsheet.Formatting;
using System.Globalization;

namespace DevExpress.Spreadsheet.Demos {
    public class LoanAmortizationScheduleDocumentGenerator {
        #region Fields

        Color titleColor = Color.FromArgb(0, 176, 80);
        Color headerColor = Color.FromArgb(166, 166, 166);
        const string SummaryAccounting = "\\$ #,##0.00;\\$ #,##0.00;\\$ \" - \"??;@";
        const string Accounting = "_(\\$* #,##0.00_);_(\\$ (#,##0.00);_(\\$* \" - \"??_);_(@_)";
        const string DateFormat = "m/d/yyyy";

        IWorkbook workbook;
        Worksheet worksheet;
        ColumnCollection columns;
        RowCollection rows;
        CellCollection cells;
        int previousScheduledNumberOfPayments;

        #endregion

        #region Properties

        public IWorkbook Workbook { get { return workbook; } }
        public double LoanAmount { get { return cells["E4"].Value.NumericValue; } set { cells["E4"].Value = value; } }
        public double InterestRate { get { return cells["E5"].Value.NumericValue; } set { cells["E5"].Value = value; } }
        public int PeriodInYears { get { return (int)cells["E6"].Value.NumericValue; } set { cells["E6"].Value = value; } }
        public DateTime StartDateOfLoan { get { return cells["E8"].Value.DateTimeValue; } set { cells["E8"].Value = value; } }
        public bool FitToPage {
            get { return worksheet.PrintOptions.FitToPage; }
            set {
                if (value) {
                    worksheet.PrintOptions.FitToPage = true;
                    worksheet.PrintOptions.FitToWidth = 1;
                    worksheet.PrintOptions.FitToHeight = 0;
                }
                else
                    worksheet.PrintOptions.FitToPage = false;
            }
        }

        public bool AnnuityPayments { get; set; }

        int ActualNumberOfPayments { get { return (int)Math.Round(cells["I6"].Value.NumericValue); } }
        string ActualLastRow { get { return (11 + ActualNumberOfPayments).ToString(); } }
        int ScheduledNumberOfPayments { get { return (int)Math.Round(cells["I5"].Value.NumericValue); } }
        string ScheduledLastRow { get { return (11 + ScheduledNumberOfPayments).ToString(); } }
        string PreviousLastRow { get { return (previousScheduledNumberOfPayments + 11).ToString(); } }

        #endregion

        public LoanAmortizationScheduleDocumentGenerator(CultureInfo culture) {
            workbook = new Workbook();
            workbook.Options.Culture = culture;
            AnnuityPayments = true;
            InitializeWorkbook();
            SetupColumnsAndRows();
            GenerateTitle();
            GenerateFieldsForDataEntry();
            GenerateFieldsForCalculationResults();
            GenerateTableHeader();
            SetBasicData();
        }

        public LoanAmortizationScheduleDocumentGenerator(IWorkbook book) {
            AnnuityPayments = true;
            workbook = book;
            InitializeWorkbook();
            SetupColumnsAndRows();
            GenerateTitle();
            GenerateFieldsForDataEntry();
            GenerateFieldsForCalculationResults();
            GenerateTableHeader();
            SetBasicData();
        }


        public IWorkbook Generate() {
            ClearTable();
            if (AnnuityPayments) {
                AddDefinedNamesForAnnuity();
                GenerateAnnuityTableBody();
            }
            else {
                AddDefinedNamesForScaled();
                GenerateScaledTableBody();
            }
            GenerateTableGrid();
            previousScheduledNumberOfPayments = ScheduledNumberOfPayments;
            return Workbook;
        }

        #region Workbook generation

        void InitializeWorkbook() {
            workbook.Unit = DocumentUnit.Point;

            workbook.Styles.DefaultStyle.Font.Name = "Tahoma";
            workbook.Styles.DefaultStyle.Font.Size = 10;

            worksheet = workbook.Worksheets[0];
            worksheet.ActiveView.ShowGridlines = false;
            worksheet.DefaultRowHeight = 15;
            worksheet.Name = "Loan Amortization Schedule";
            worksheet.DefinedNames.Add("_xlnm.Print_Titles", "'Loan Amortization Schedule'!$11:$11");

            columns = worksheet.Columns;
            rows = worksheet.Rows;
            cells = worksheet.Cells;
        }
        void SetupColumnsAndRows() {
            worksheet.DefaultColumnWidthInCharacters = 9f;

            columns["A"].WidthInCharacters = 3.43;
            columns["B"].WidthInCharacters = 5.14;
            worksheet["C:K"].ColumnWidthInCharacters = 11.71;

            rows["2"].Height = 31;
            rows["11"].Height = 35;
        }
        void GenerateTitle() {
            cells["B2"].Value = "Loan Amortization Schedule";
            worksheet["B2:K2"].Merge();

            Range formattedRange = worksheet["B2:K2"];
            SpreadsheetFormatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Font.Size = 26;
                formatting.Font.Color = titleColor;
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }
        }
        void GenerateFieldsForDataEntry() {
            worksheet["B4:D4"].Merge();
            worksheet["B5:D5"].Merge();
            worksheet["B6:D6"].Merge();
            worksheet["B7:D7"].Merge();
            worksheet["B8:D8"].Merge();
            worksheet["B9:D9"].Merge();

            Range formattedRange = worksheet["B4:D9"];
            SpreadsheetFormatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Indent = 1;
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                formatting.Font.Size = 10;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            formattedRange = worksheet["E4:E9"];
            formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                formatting.Font.Size = 10;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            cells["B4"].Value = "Loan amount:";
            cells["B5"].Value = "Annual interest rate:";
            cells["B6"].Value = "Loan period in years:";
            cells["B7"].Value = "Number of payments per year:";
            cells["B8"].Value = "Start date of loan:";
            cells["B9"].Value = "Optional extra payments:";

            cells["E4"].NumberFormat = SummaryAccounting;
            cells["E5"].NumberFormat = "0.00%";
            cells["E8"].NumberFormat = DateFormat;
            cells["E9"].NumberFormat = SummaryAccounting;
        }
        void GenerateFieldsForCalculationResults() {
            worksheet["F4:H4"].Merge();
            worksheet["F5:H5"].Merge();
            worksheet["F6:H6"].Merge();
            worksheet["F7:H7"].Merge();
            worksheet["F8:H8"].Merge();

            Range formattedRange = worksheet["F4:H8"];
            SpreadsheetFormatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Indent = 1;
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
                formatting.Font.Size = 10;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            formattedRange = worksheet["I4:I8"];
            formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
                formatting.Font.Size = 10;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            cells["F4"].Value = "First scheduled payment:";
            cells["F5"].Value = "Scheduled number of payments:";
            cells["F6"].Value = "Actual number of payments:";
            cells["F7"].Value = "Total early payments:";
            cells["F8"].Value = "Total interest:";

            cells["I4"].NumberFormat = SummaryAccounting;
            cells["I7"].NumberFormat = SummaryAccounting;
            cells["I8"].NumberFormat = SummaryAccounting;
        }
        void GenerateTableHeader() {
            Range formattedRange = worksheet["B11:K11"];
            SpreadsheetFormatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Horizontal = DevExpress.Spreadsheet.SpreadsheetHorizontalAlignment.Center;
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                formatting.Alignment.WrapText = true;
                formatting.Fill.BackgroundColor = headerColor;
                formatting.Font.Color = Color.White;
                formatting.Font.Size = 10;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            cells["B11"].Value = "No.";
            cells["C11"].Value = "Payment Date";
            cells["D11"].Value = "Beginning Balance";
            cells["E11"].Value = "Scheduled Payment";
            cells["F11"].Value = "Extra Payment";
            cells["G11"].Value = "Total Payment";
            cells["H11"].Value = "Principal";
            cells["I11"].Value = "Interest";
            cells["J11"].Value = "Ending Balance";
            cells["K11"].Value = "Cumulative Interest";
        }
        void SetBasicData() {
            cells["E4"].Value = 19900;
            cells["E5"].Value = 0.055;
            cells["E6"].Value = 2;
            cells["E7"].Value = 12;
            cells["E8"].Value = DateTime.Now;
            cells["E9"].Value = 200;
        }
        void ClearTable() {
            if (previousScheduledNumberOfPayments != 0) {
                Range formattedRange = worksheet["B12:K" + PreviousLastRow];
                formattedRange.ClearFormats();
                worksheet["B12:K" + PreviousLastRow].Value = CellValue.Empty;
            }
        }

        string GetSheetNameWrappedInQuotes() {
            return "'" + worksheet.Name + "'";
        }

        void AddDefinedNamesForAnnuity() {
            string sheetName = GetSheetNameWrappedInQuotes();
            DefinedNameCollection definedNames = workbook.DefinedNames;
            definedNames.Clear();
            definedNames.Add("Loan_Amount", sheetName + "!$E$4");
            definedNames.Add("Interest_Rate", sheetName + "!$E$5");
            definedNames.Add("Loan_Years", sheetName + "!$E$6");
            definedNames.Add("Number_of_Payments_Per_Year", sheetName + "!$E$7");
            definedNames.Add("Loan_Start", sheetName + "!$E$8");
            definedNames.Add("Extra_Payments", sheetName + "!$E$9");
            definedNames.Add("Scheduled_payment", sheetName + "!$I$4");
            definedNames.Add("Scheduled_Number_Payments", sheetName + "!$I$5");
            definedNames.Add("Interest_Rate_Per_Month", "=Interest_Rate/Number_of_Payments_Per_Year");
            definedNames.Add("Actual_Number_Payments", "=NPER(Interest_Rate_Per_Month, " + sheetName + "!$I$4+Extra_Payments, -Loan_Amount)");
        }

        void AddDefinedNamesForScaled() {
            string sheetName = GetSheetNameWrappedInQuotes();
            DefinedNameCollection definedNames = workbook.DefinedNames;
            definedNames.Clear();
            definedNames.Add("Values_Entered", "IF(Loan_Amount*Interest_Rate*Loan_Years*Loan_Start>0,1,0)");
            definedNames.Add("Full_Print", sheetName + "!$A:$K");
            definedNames.Add("Loan_Amount", sheetName + "!$E$4");
            definedNames.Add("Interest_Rate", sheetName + "!$E$5");
            definedNames.Add("Loan_Years", sheetName + "!$E$6");
            definedNames.Add("Number_of_Payments_Per_Year", sheetName + "!$E$7");
            definedNames.Add("Loan_Start", sheetName + "!$E$8");
            definedNames.Add("Extra_Payments", sheetName + "!$E$9");
            definedNames.Add("Scheduled_Monthly_Payment", "Loan_Amount/(Loan_Years*Number_of_Payments_Per_Year)");
            definedNames.Add("Scheduled_Number_Payments", sheetName + "!$I$5");
            definedNames.Add("Real_Number_Payments", sheetName + "!$I$6");
            definedNames.Add("Total_Early_Payments", sheetName + "!$I$7");
            definedNames.Add("Total_Interest", sheetName + "!$I$8");
            definedNames.Add("Beg_Bal", sheetName + "!$D$12:$D$" + ScheduledLastRow);
            definedNames.Add("Cum_Int", sheetName + "!$K$12:$K$" + ScheduledLastRow);
            definedNames.Add("Data", sheetName + "!$B$12:$K$" + ScheduledLastRow);
            definedNames.Add("End_Bal", sheetName + "!$J$12:$J$" + ScheduledLastRow);
            definedNames.Add("Extra_Pay", sheetName + "!$F$12:$F$" + ScheduledLastRow);
            definedNames.Add("Header_Row", "ROW(" + sheetName + "!$17:$17)");
            definedNames.Add("Int", sheetName + "!$I$12:$I$" + ScheduledLastRow);
            definedNames.Add("Last_Row", "IF(Values_Entered,Header_Row+Number_of_Payments,Header_Row)");
            definedNames.Add("Number_of_Payments", "=MATCH(0.01,End_Bal,-1)+1");
            definedNames.Add("Pay_Date", sheetName + "!$C$12:$C$" + ScheduledLastRow);
            definedNames.Add("Pay_Num", sheetName + "!$B$12:$B$" + ScheduledLastRow);
            definedNames.Add("Payment_Date", "DATE(YEAR(Loan_Start),MONTH(Loan_Start)+Payment_Number,DAY(Loan_Start))");
            definedNames.Add("Princ", sheetName + "!$H$12:$H$" + ScheduledLastRow);
            definedNames.Add("Print_Area_Reset", "OFFSET(Full_Print,0,0,Last_Row)");
            definedNames.Add("Sched_Pay", sheetName + "!$E$12:$E$" + ScheduledLastRow);
            definedNames.Add("Total_Pay", sheetName + "!$G$12:$G$" + ScheduledLastRow);
            definedNames.Add("Total_Payment", "Scheduled_Payment+Extra_Payment");
            definedNames.Add("Payment_Number", "ROW()-Header_Row");
            definedNames.Add("Loan_Not_Paid", "IF(Payment_Number<=Number_of_Payments,1,0)");
        }

        void GenerateAnnuityTableBody() {
            cells["I4"].Formula = "=PMT(Interest_Rate_Per_Month,Scheduled_Number_Payments,-Loan_Amount)";
            cells["I5"].Formula = "=Loan_Years*Number_of_Payments_Per_Year";
            cells["I6"].Formula = "=ROUNDUP(Actual_Number_Payments,0)";
            workbook.Calculate();
            cells["I7"].Formula = "=SUM(F12:F" + ActualLastRow + ")";
            cells["I8"].Formula = "=SUM($I$12:$I$" + ActualLastRow + ")";

            if (ScheduledNumberOfPayments == 0)
                return;

            for (int i = 0; i < ActualNumberOfPayments; i++)
                cells["B" + (i + 12).ToString()].Value = i + 1;
            worksheet["C12:C" + ActualLastRow].Formula = "=DATE(YEAR(Loan_Start),MONTH(Loan_Start)+(B12)*12/Number_of_Payments_Per_Year,DAY(Loan_Start))";
            cells["D12"].Formula = "=Loan_Amount";
            if (ScheduledNumberOfPayments > 1)
                worksheet["D13:D" + ActualLastRow].Formula = "=J12";
            worksheet["E12:E" + ActualLastRow].Formula = "=IF(D12>0,IF(Scheduled_payment<D12, Scheduled_payment, D12),0)";
            worksheet["F12:F" + ActualLastRow].Formula = "=IF(Extra_Payments<>0, IF(Scheduled_payment<D12, G12-E12, 0), 0)";
            worksheet["G12:G" + ActualLastRow].Formula = "=H12+I12";
            worksheet["H12:H" + ActualLastRow].Formula = "=IF(J12>0,PPMT(Interest_Rate_Per_Month,B12,Actual_Number_Payments,-Loan_Amount),D12)";
            worksheet["I12:I" + ActualLastRow].Formula = "=IF(D12>0,IPMT(Interest_Rate_Per_Month,B12,Actual_Number_Payments,-Loan_Amount),0)";
            worksheet["J12:J" + ActualLastRow].Formula = "=IF(D12-PPMT(Interest_Rate_Per_Month,B12,Actual_Number_Payments,-Loan_Amount)>0,D12-PPMT(Interest_Rate_Per_Month,B12,Actual_Number_Payments,-Loan_Amount),0)";
            worksheet["K12:K" + ActualLastRow].Formula = "=SUM($I$12:$I12)";
        }
        void GenerateScaledTableBody() {
            cells["I4"].Formula = "=IF(Values_Entered,Scheduled_Monthly_Payment,\"\")";
            cells["I5"].Formula = "=IF(Values_Entered,Loan_Years*Number_of_Payments_Per_Year,\"\")";
            cells["I6"].Formula = "=IF(Values_Entered,Number_of_Payments,\"\")";
            cells["I7"].Formula = "=IF(Values_Entered,SUMIF(Beg_Bal,\">0\",Extra_Pay),\"\")";
            cells["I8"].Formula = "=IF(Values_Entered,SUMIF(Beg_Bal,\">0\",Int),\"\")";
            workbook.Calculate();

            if (ScheduledNumberOfPayments == 0)
                return;
            cells["B12"].Formula = "=1";
            if (ScheduledNumberOfPayments > 1)
                worksheet["B13:B" + ScheduledLastRow].Formula = "=IF(NOT(OR(J12=0,J12=\"\")),B12+1,\"\")";
            worksheet["C12:C" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",DATE(YEAR(Loan_Start),MONTH(Loan_Start)+(Pay_Num)*12/Number_of_Payments_Per_Year,DAY(Loan_Start)),\"\")";
            cells["D12"].Formula = "=IF(Values_Entered,Loan_Amount,\"\")";
            if (ScheduledNumberOfPayments > 1)
                worksheet["D13:D" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",J12,\"\")";
            worksheet["E12:E" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",Scheduled_Monthly_Payment,\"\")";
            worksheet["F12:F" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",IF(Sched_Pay+Extra_Payments<Beg_Bal,Extra_Payments,IF(AND(Pay_Num<>\"\",Beg_Bal-Sched_Pay>0),Beg_Bal-Sched_Pay,IF(Pay_Num<>\"\",0,\"\"))),\"\")";
            worksheet["G12:G" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",IF(Sched_Pay+Extra_Pay<Beg_Bal,Princ+Int+Extra_Pay,IF(Pay_Num<>\"\",Beg_Bal,\"\")),\"\")";
            worksheet["H12:H" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",Scheduled_Monthly_Payment,\"\")";
            worksheet["I12:I" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",Beg_Bal*(Interest_Rate/Number_of_Payments_Per_Year),\"\")";
            worksheet["J12:J" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",IF(Sched_Pay+Extra_Pay<Beg_Bal,Beg_Bal-Princ,IF(Pay_Num<>\"\",0,\"\")),\"\")";
            worksheet["K12:K" + ScheduledLastRow].Formula = "=IF(Pay_Num<>\"\",SUM($I$12:$I12),\"\")";
        }

        void GenerateTableGrid() {
            workbook.Calculate();
            Range formattedRange;
            for (int i = 1; i < ActualNumberOfPayments; i += 2) {
                formattedRange = worksheet.Range.FromLTRB(1, 11 + i, 10, 11 + i);
                formattedRange.Fill.BackgroundColor = Color.FromArgb(217, 217, 217);
            }

            formattedRange = worksheet["B11:K" + ActualLastRow];
            SpreadsheetFormatting formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Borders.InsideVerticalBorders.LineStyle = BorderLineStyle.Thin;
                formatting.Borders.InsideVerticalBorders.Color = Color.White;
                formatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            formattedRange = worksheet["B12:C" + ActualLastRow];
            formattedRange.RowHeight = 15;
            formatting = formattedRange.BeginUpdateFormatting();
            try {
                formatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Right;
            }
            finally {
                formattedRange.EndUpdateFormatting(formatting);
            }

            formattedRange = worksheet["C11:C" + ActualLastRow];
            formattedRange.NumberFormat = DateFormat;
            formattedRange = worksheet["D11:K" + ActualLastRow];
            formattedRange.NumberFormat = Accounting;
        }

        #endregion
    }
}
