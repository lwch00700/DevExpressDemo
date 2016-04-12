using System;
using System.Collections.Generic;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using System.Drawing;
using SpreadsheetFormatting = DevExpress.Spreadsheet.Formatting;
using System.Globalization;

namespace DevExpress.Spreadsheet.Demos {
    public class SportResultsDocumentGenerator {
        IWorkbook book;
        Worksheet sheet1;
        Worksheet sheet2;
        char separator;

        public SportResultsDocumentGenerator(CultureInfo culture) {
            this.book = new Workbook();
            this.book.Options.Culture = culture;
            InitCultureParams(culture);
        }
        public SportResultsDocumentGenerator(IWorkbook book) {
            this.book = book;
            InitCultureParams(book.Options.Culture);
        }
        void InitCultureParams(CultureInfo culture) {
            this.separator = culture.TextInfo.ListSeparator[0];
        }
        void InitializeBook() {
            InitWorkbook();

            SetRowsHeight();
            SetColumnsWidth();
            SetColumnsVisibility();
            AddStyles();
            CreateResultsSheet();
            CreatePointsSheet();
            FormatCells();
        }
        public IWorkbook Generate() {
            InitializeBook();
            return book;
        }
        void SetColumnsVisibility() {
            sheet1.Columns[6].Visible = false;
            sheet1.Columns[7].Visible = false;
            sheet1.Columns[8].Visible = false;
        }
        void FormatCells() {
            Range range1 = sheet1["B3"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet1["C3"];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet1["D3:J3"];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet1["L3"];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            range4.EndUpdateFormatting(rangeFormatting4);

            Range range5 = sheet1["M3"];
            Formatting rangeFormatting5 = range5.BeginUpdateFormatting();
            rangeFormatting5.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            range5.EndUpdateFormatting(rangeFormatting5);

            Range range6 = sheet1["N3"];
            Formatting rangeFormatting6 = range6.BeginUpdateFormatting();
            rangeFormatting6.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            range6.EndUpdateFormatting(rangeFormatting6);

            Range range7 = sheet1["C4:C23"];
            Formatting rangeFormatting7 = range7.BeginUpdateFormatting();
            rangeFormatting7.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            range7.EndUpdateFormatting(rangeFormatting7);

            Range range8 = sheet1["M4:M13"];
            Formatting rangeFormatting8 = range8.BeginUpdateFormatting();
            rangeFormatting8.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting8.NumberFormat = "0";
            range8.EndUpdateFormatting(rangeFormatting8);

            Range range9 = sheet1["B25"];
            Formatting rangeFormatting9 = range9.BeginUpdateFormatting();
            rangeFormatting9.Font.Color = Color.FromArgb(255, 89, 89, 89);
            rangeFormatting9.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting9.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting9.Alignment.Indent = 1;
            range9.EndUpdateFormatting(rangeFormatting9);

            Range range10 = sheet2["B25"];
            Formatting rangeFormatting10 = range10.BeginUpdateFormatting();
            rangeFormatting10.Font.Color = Color.FromArgb(255, 89, 89, 89);
            rangeFormatting10.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting10.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            rangeFormatting10.Alignment.Indent = 1;
            range10.EndUpdateFormatting(rangeFormatting10);
        }
        void CreatePointsSheet() {
            CreatePointsSheetHeader();
            CreateTableRacePoints();
            sheet2.Cells["B25"].Value = "Copyright © Extreme Racing Inc. 2013";
        }
        void CreateTableRacePoints() {
            CreateTableRacePointsHeader();
            ApplyStyleInTableRacePoints();
            CreateTableTRacePointsDataRange();
        }
        void CreateTableTRacePointsDataRange() {
            sheet2.Cells["B4"].Value = 1;
            sheet2.Cells["C4"].Value = 30;
            sheet2.Cells["B5"].Value = 2;
            sheet2.Cells["C5"].Value = 25;
            sheet2.Cells["B6"].Value = 3;
            sheet2.Cells["C6"].Value = 20;
            sheet2.Cells["B7"].Value = 4;
            sheet2.Cells["C7"].Value = 18;
            sheet2.Cells["B8"].Value = 5;
            sheet2.Cells["C8"].Value = 16;
            sheet2.Cells["B9"].Value = 6;
            sheet2.Cells["C9"].Value = 14;
            sheet2.Cells["B10"].Value = 7;
            sheet2.Cells["C10"].Value = 12;
            sheet2.Cells["B11"].Value = 8;
            sheet2.Cells["C11"].Value = 10;
            sheet2.Cells["B12"].Value = 9;
            sheet2.Cells["C12"].Value = 8;
            sheet2.Cells["B13"].Value = 10;
            sheet2.Cells["C13"].Value = 6;
            sheet2.Cells["B14"].Value = 11;
            sheet2.Cells["C14"].Value = 5;
            sheet2.Cells["B15"].Value = 12;
            sheet2.Cells["C15"].Value = 4;
            sheet2.Cells["B16"].Value = 13;
            sheet2.Cells["C16"].Value = 3;
            sheet2.Cells["B17"].Value = 14;
            sheet2.Cells["C17"].Value = 2;
            sheet2.Cells["B18"].Value = 15;
            sheet2.Cells["C18"].Value = 1;
            sheet2.Cells["B19"].Value = 16;
            sheet2.Cells["C19"].Value = 0;
            sheet2.Cells["B20"].Value = 17;
            sheet2.Cells["C20"].Value = 0;
            sheet2.Cells["B21"].Value = 18;
            sheet2.Cells["C21"].Value = 0;
            sheet2.Cells["B22"].Value = 19;
            sheet2.Cells["C22"].Value = 0;
            sheet2.Cells["B23"].Value = 20;
            sheet2.Cells["C23"].Value = 0;
        }
        void ApplyStyleInTableRacePoints() {
            for(int rowIndex = 4; rowIndex < 24; rowIndex++) {
                if(rowIndex % 2 != 0)
                    sheet2["B" + rowIndex + ":C" + rowIndex].Style = book.Styles["Style 5"];
                else
                    sheet2["B" + rowIndex + ":C" + rowIndex].Style = book.Styles["Style 4"];
            }
        }
        void CreateTableRacePointsHeader() {
            sheet2.Cells["B3"].Value = "Position";
            sheet2.Cells["C3"].Value = "Points";

            sheet2["B2"].Style = book.Styles["Style 2"];
            sheet2["B3:C3"].Style = book.Styles["Style 3"];
        }
        void CreatePointsSheetHeader() {
            sheet2.Cells["B1"].Value = "EXTREME RACING 2013";
            sheet2.Cells["B2"].Value = "RACE POINTS";

            sheet2["B1"].Style = book.Styles["Style 1"];
            sheet2["B2"].Style = book.Styles["Style 2"];
        }
        void InitWorkbook() {
            book.Worksheets.Add();
            book.Unit = DevExpress.Office.DocumentUnit.Point;
            book.Styles.DefaultStyle.Font.Name = "Calibri";
            book.Styles.DefaultStyle.Font.Size = 11;

            sheet1 = book.Worksheets[0];
            sheet1.Name = "Results";
            sheet1.DefaultRowHeight = 15;
            sheet1.ActiveView.Orientation = PageOrientation.Portrait;
            sheet1.PrintOptions.FitToPage = true;
            sheet1.ActiveView.ShowGridlines = false;

            sheet2 = book.Worksheets[1];
            sheet2.Name = "Points";
            sheet2.DefaultRowHeight = 15;
            sheet2.ActiveView.Orientation = PageOrientation.Portrait;
            sheet2.PrintOptions.FitToPage = true;
            sheet2.ActiveView.ShowGridlines = false;

            book.Worksheets.ActiveWorksheet = sheet1;
        }
        void CreateResultsSheet() {
            CreateResultsSheetHeader();
            CreateTableResults();
            CreateTableTop10();
            sheet1.Cells["B25"].Value = "Copyright © Extreme Racing Inc. 2013";
        }
        void CreateTableTop10() {
            CreateTableTop10Header();
            ApplyStyleInTableTop10();
            CreateTableTop10DataRange();
        }
        void CreateTableTop10DataRange() {
            sheet1.Cells["L4"].Value = 1;
            sheet1.Cells["N4"].Formula = String.Format("=LARGE(J$4:J$23{0}ROW()-ROW(N$3))", separator);
            sheet1["N5:N13"].Formula = String.Format("=LARGE(J$4:J$23{0}ROW()-ROW(N$3))", separator);

            sheet1.Cells["L5"].Value = 2;
            sheet1.Cells["L6"].Value = 3;
            sheet1.Cells["L7"].Value = 4;
            sheet1.Cells["L9"].Value = 6;
            sheet1.Cells["L10"].Value = 7;
            sheet1.Cells["L11"].Value = 8;
            sheet1.Cells["L12"].Value = 9;
            sheet1.Cells["L13"].Value = 10;

            sheet1["M4"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N4{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N4{0}N4)))", separator);
            sheet1["M5"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N5{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N5{0}N5)))", separator);
            sheet1["M6"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N6{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N6{0}N6)))", separator);
            sheet1["M7"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N7{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N7{0}N7)))", separator);
            sheet1["M8"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N8{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N8{0}N8)))", separator);
            sheet1["M9"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N9{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N9{0}N9)))", separator);

            sheet1["M10"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N10{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N10{0}N10)))", separator);
            sheet1["M11"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N11{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N11{0}N11)))", separator);
            sheet1["M12"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N12{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N12{0}N12)))", separator);
            sheet1["M13"].ArrayFormula = String.Format("=INDEX(C$4:C$23{0}SMALL(IF(J$4:J$23=N13{0}ROW(J$4:J$23)-ROW(N$4)+1){0}COUNTIF(N$4:N13{0}N13)))", separator);
        }
        void ApplyStyleInTableTop10() {
            for(int rowIndex = 4; rowIndex < 14; rowIndex++) {
                if(rowIndex % 2 != 0)
                    sheet1["L" + rowIndex + ":N" + rowIndex].Style = book.Styles["Style 5"];
                else
                    sheet1["L" + rowIndex + ":N" + rowIndex].Style = book.Styles["Style 4"];
            }
        }
        void CreateTableTop10Header() {
            sheet1.Cells["L3"].Value = "Pos";
            sheet1.Cells["M3"].Value = "Driver";
            sheet1.Cells["N3"].Value = "Points";

            sheet1["L2:N2"].Style = book.Styles["Style 2"];
            sheet1["L3:N3"].Style = book.Styles["Style 3"];
        }
        void CreateTableResults() {
            CreateTableResultsHeader();
            ApplyStyleInTableResults();
            CreateTableResultsDataRange();
        }
        void ApplyStyleInTableResults() {
            for(int rowIndex = 4; rowIndex < 24; rowIndex++) {
                if(rowIndex % 2 != 0)
                    sheet1["B" + rowIndex + ":J" + rowIndex].Style = book.Styles["Style 5"];
                else
                    sheet1["B" + rowIndex + ":J" + rowIndex].Style = book.Styles["Style 4"];
            }
        }
        void CreateTableResultsDataRange() {
            JohnSmithResults();
            AntonyCarcherResults();
            AlanMuskResults();
            RobertSeldonResults();
            MaxPerryResults();
            SamJordan();
            FabrizioManzoniResults();
            BobRansomeResults();
            AndyFarrelResults();
            JackSommersResults();
            FrankJacksonResults();
            AlexTedeskiResults();
            JohnFaulerResults();
            BenRustikResults();
            AntonyBiggsResults();
            EllyVicenzoResults();
            SamWintersResults();
            ChuckFisherResults();
            AndyClarkResults();
            JackBrandelResults();

            sheet1["J4:J23"].Formula = String.Format("=SUM(G4:I4) + COUNTIF(G4:I4{0}\">0\")/10 + (20-B4)/100", separator);
        }
        void JackBrandelResults() {
            sheet1.Cells["B23"].Value = 20;
            sheet1.Cells["C23"].Value = "Jack Brandel";
            sheet1.Cells["D23"].Value = 20;
            sheet1.Cells["E23"].Value = 18;
            sheet1.Cells["F23"].Value = 19;

            sheet1.Cells["G23"].Formula = String.Format("=LOOKUP(D23{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H23"].Formula = String.Format("=LOOKUP(E23{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I23"].Formula = String.Format("=LOOKUP(F23{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AndyClarkResults() {
            sheet1.Cells["B22"].Value = 19;
            sheet1.Cells["C22"].Value = "Andy Clark";
            sheet1.Cells["D22"].Value = 8;
            sheet1.Cells["E22"].Value = 10;
            sheet1.Cells["F22"].Value = 9;

            sheet1.Cells["G22"].Formula = String.Format("=LOOKUP(D22{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H22"].Formula = String.Format("=LOOKUP(E22{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I22"].Formula = String.Format("=LOOKUP(F22{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void ChuckFisherResults() {
            sheet1.Cells["B21"].Value = 18;
            sheet1.Cells["C21"].Value = "Chuck Fisher";
            sheet1.Cells["D21"].Value = 7;
            sheet1.Cells["E21"].Value = 12;
            sheet1.Cells["F21"].Value = 6;

            sheet1.Cells["G21"].Formula = String.Format("=LOOKUP(D21{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H21"].Formula = String.Format("=LOOKUP(E21{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I21"].Formula = String.Format("=LOOKUP(F21{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void SamWintersResults() {
            sheet1.Cells["B20"].Value = 17;
            sheet1.Cells["C20"].Value = "Sam Winters";
            sheet1.Cells["D20"].Value = 4;
            sheet1.Cells["E20"].Value = 3;
            sheet1.Cells["F20"].Value = 5;

            sheet1.Cells["G20"].Formula = String.Format("=LOOKUP(D20{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H20"].Formula = String.Format("=LOOKUP(E20{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I20"].Formula = String.Format("=LOOKUP(F20{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void EllyVicenzoResults() {
            sheet1.Cells["B19"].Value = 16;
            sheet1.Cells["C19"].Value = "Elly Vicenzo";
            sheet1.Cells["D19"].Value = 18;
            sheet1.Cells["E19"].Value = 19;
            sheet1.Cells["F19"].Value = 20;

            sheet1.Cells["G19"].Formula = String.Format("=LOOKUP(D19{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H19"].Formula = String.Format("=LOOKUP(E19{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I19"].Formula = String.Format("=LOOKUP(F19{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AntonyBiggsResults() {
            sheet1.Cells["B18"].Value = 15;
            sheet1.Cells["C18"].Value = "Antony Biggs";
            sheet1.Cells["D18"].Value = 12;
            sheet1.Cells["E18"].Value = 9;
            sheet1.Cells["F18"].Value = 10;

            sheet1.Cells["G18"].Formula = String.Format("=LOOKUP(D18{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H18"].Formula = String.Format("=LOOKUP(E18{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I18"].Formula = String.Format("=LOOKUP(F18{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void BenRustikResults() {
            sheet1.Cells["B17"].Value = 14;
            sheet1.Cells["C17"].Value = "Ben Rustik";
            sheet1.Cells["D17"].Value = 6;
            sheet1.Cells["E17"].Value = 4;
            sheet1.Cells["F17"].Value = 7;

            sheet1.Cells["G17"].Formula = String.Format("=LOOKUP(D17{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H17"].Formula = String.Format("=LOOKUP(E17{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I17"].Formula = String.Format("=LOOKUP(F17{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void JohnFaulerResults() {
            sheet1.Cells["B16"].Value = 13;
            sheet1.Cells["C16"].Value = "John Fauler";
            sheet1.Cells["D16"].Value = 14;
            sheet1.Cells["E16"].Value = 11;
            sheet1.Cells["F16"].Value = 13;

            sheet1.Cells["G16"].Formula = String.Format("=LOOKUP(D16{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H16"].Formula = String.Format("=LOOKUP(E16{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I16"].Formula = String.Format("=LOOKUP(F16{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AlexTedeskiResults() {
            sheet1.Cells["B15"].Value = 12;
            sheet1.Cells["C15"].Value = "Alex Tedeski";
            sheet1.Cells["D15"].Value = 19;
            sheet1.Cells["E15"].Value = 20;
            sheet1.Cells["F15"].Value = 17;

            sheet1.Cells["G15"].Formula = String.Format("=LOOKUP(D15{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H15"].Formula = String.Format("=LOOKUP(E15{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I15"].Formula = String.Format("=LOOKUP(F15{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void FrankJacksonResults() {
            sheet1.Cells["B14"].Value = 11;
            sheet1.Cells["C14"].Value = "Frank Jackson";
            sheet1.Cells["D14"].Value = 5;
            sheet1.Cells["E14"].Value = 6;
            sheet1.Cells["F14"].Value = 3;

            sheet1.Cells["G14"].Formula = String.Format("=LOOKUP(D14{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H14"].Formula = String.Format("=LOOKUP(E14{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I14"].Formula = String.Format("=LOOKUP(F14{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void JackSommersResults() {
            sheet1.Cells["B13"].Value = 10;
            sheet1.Cells["C13"].Value = "Jack Sommers";
            sheet1.Cells["D13"].Value = 10;
            sheet1.Cells["E13"].Value = 13;
            sheet1.Cells["F13"].Value = 11;

            sheet1.Cells["G13"].Formula = String.Format("=LOOKUP(D13{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H13"].Formula = String.Format("=LOOKUP(E13{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I13"].Formula = String.Format("=LOOKUP(F13{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AndyFarrelResults() {
            sheet1.Cells["B12"].Value = 9;
            sheet1.Cells["C12"].Value = "Andy Farrel";
            sheet1.Cells["D12"].Value = 17;
            sheet1.Cells["E12"].Value = 14;
            sheet1.Cells["F12"].Value = 12;

            sheet1.Cells["G12"].Formula = String.Format("=LOOKUP(D12{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H12"].Formula = String.Format("=LOOKUP(E12{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I12"].Formula = String.Format("=LOOKUP(F12{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void BobRansomeResults() {
            sheet1.Cells["B11"].Value = 8;
            sheet1.Cells["C11"].Value = "Bob Ransome";
            sheet1.Cells["D11"].Value = 3;
            sheet1.Cells["E11"].Value = 5;
            sheet1.Cells["F11"].Value = 2;

            sheet1.Cells["G11"].Formula = String.Format("=LOOKUP(D11{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H11"].Formula = String.Format("=LOOKUP(E11{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I11"].Formula = String.Format("=LOOKUP(F11{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void FabrizioManzoniResults() {
            sheet1.Cells["B10"].Value = 7;
            sheet1.Cells["C10"].Value = "Fabrizio Manzoni";
            sheet1.Cells["D10"].Value = 16;
            sheet1.Cells["E10"].Value = 15;
            sheet1.Cells["F10"].Value = 16;

            sheet1.Cells["G10"].Formula = String.Format("=LOOKUP(D10{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H10"].Formula = String.Format("=LOOKUP(E10{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I10"].Formula = String.Format("=LOOKUP(F10{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void SamJordan() {
            sheet1.Cells["B9"].Value = 6;
            sheet1.Cells["C9"].Value = "Sam Jordan";
            sheet1.Cells["D9"].Value = 9;
            sheet1.Cells["E9"].Value = 7;
            sheet1.Cells["F9"].Value = 8;

            sheet1.Cells["G9"].Formula = String.Format("=LOOKUP(D9{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H9"].Formula = String.Format("=LOOKUP(E9{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I9"].Formula = String.Format("=LOOKUP(F9{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void MaxPerryResults() {
            sheet1.Cells["B8"].Value = 5;
            sheet1.Cells["C8"].Value = "Max Perry";
            sheet1.Cells["D8"].Value = 1;
            sheet1.Cells["E8"].Value = 2;
            sheet1.Cells["F8"].Value = 1;

            sheet1.Cells["G8"].Formula = String.Format("=LOOKUP(D8{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H8"].Formula = String.Format("=LOOKUP(E8{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I8"].Formula = String.Format("=LOOKUP(F8{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void RobertSeldonResults() {
            sheet1.Cells["B7"].Value = 4;
            sheet1.Cells["C7"].Value = "Robert Seldon";
            sheet1.Cells["D7"].Value = 2;
            sheet1.Cells["E7"].Value = 1;
            sheet1.Cells["F7"].Value = 4;

            sheet1.Cells["G7"].Formula = String.Format("=LOOKUP(D7{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H7"].Formula = String.Format("=LOOKUP(E7{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I7"].Formula = String.Format("=LOOKUP(F7{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AlanMuskResults() {
            sheet1.Cells["B6"].Value = 3;
            sheet1.Cells["C6"].Value = "Alan Musk";
            sheet1.Cells["D6"].Value = 11;
            sheet1.Cells["E6"].Value = 8;
            sheet1.Cells["F6"].Value = 14;

            sheet1.Cells["G6"].Formula = String.Format("=LOOKUP(D6{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H6"].Formula = String.Format("=LOOKUP(E6{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I6"].Formula = String.Format("=LOOKUP(F6{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void AntonyCarcherResults() {
            sheet1.Cells["B5"].Value = 2;
            sheet1.Cells["C5"].Value = "Antony Carcher";
            sheet1.Cells["D5"].Value = 13;
            sheet1.Cells["E5"].Value = 16;
            sheet1.Cells["F5"].Value = 15;

            sheet1.Cells["G5"].Formula = String.Format("=LOOKUP(D5{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H5"].Formula = String.Format("=LOOKUP(E5{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I5"].Formula = String.Format("=LOOKUP(F5{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void JohnSmithResults() {
            sheet1.Cells["B4"].Value = 1;
            sheet1.Cells["C4"].Value = "John Smith";
            sheet1.Cells["D4"].Value = 15;
            sheet1.Cells["E4"].Value = 17;
            sheet1.Cells["F4"].Value = 18;

            sheet1.Cells["G4"].Formula = String.Format("=LOOKUP(D4{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["H4"].Formula = String.Format("=LOOKUP(E4{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
            sheet1.Cells["I4"].Formula = String.Format("=LOOKUP(F4{0}Points!$B$4:$B$23{0}Points!$C$4:$C$23)", separator);
        }
        void CreateTableResultsHeader() {
            sheet1.Cells["B3"].Value = "Start No.";
            sheet1.Cells["C3"].Value = "Driver";
            sheet1.Cells["D3"].Value = "Race 1";
            sheet1.Cells["E3"].Value = "Race 2";
            sheet1.Cells["F3"].Value = "Race 3";
            sheet1.Cells["G3"].Value = "Points 1";
            sheet1.Cells["H3"].Value = "Points 2";
            sheet1.Cells["I3"].Value = "Points 3";
            sheet1.Cells["J3"].Value = "Points";

            sheet1["B3:J3"].Style = book.Styles["Style 3"];
        }
        void CreateResultsSheetHeader() {
            sheet1.Cells["B1"].Value = "EXTREME RACING 2013";
            sheet1.Cells["B2"].Value = "RESULTS";
            sheet1.Cells["L2"].Value = "TOP 10";

            sheet1["B1"].Style = book.Styles["Style 1"];
            sheet1["B2:J2"].Style = book.Styles["Style 2"];
        }
        void MergeCells() {
            sheet1["B2:J2"].Merge();
            sheet1["L2:N2"].Merge();
            sheet1["B1:N1"].Merge();
            sheet1["B25:J25"].Merge();
            sheet2["B1:I1"].Merge();
            sheet2["B25:F25"].Merge();
            sheet2["B2:J2"].Merge();
        }
        void AddStyles() {
            StyleCollection styles = book.Styles;

            Style style1 = styles.Add("Style 1");
            style1.Fill.PatternType = PatternType.Solid;
            style1.Fill.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            style1.Font.Color = Color.FromArgb(255, 218, 0, 0);
            style1.Font.Size = 36;
            style1.Font.Name = "Calibri Light";
            style1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            style1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;

            Style style2 = styles.Add("Style 2");
            style2.Fill.PatternType = PatternType.Solid;
            style2.Fill.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            style2.Font.Color = Color.FromArgb(255, 89, 89, 89);
            style2.Font.Size = 18;
            style2.Font.Name = "Calibri Light";

            Style style3 = styles.Add("Style 3");
            style3.Fill.PatternType = PatternType.Solid;
            style3.Fill.BackgroundColor = Color.FromArgb(255, 89, 89, 89);
            style3.Font.Color = Color.FromArgb(255, 255, 255, 255);
            style3.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            Style style4 = styles.Add("Style 4");

            style4.Fill.PatternType = PatternType.Solid;
            style4.Fill.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            style4.Font.Color = Color.FromArgb(255, 89, 89, 89);
            style4.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            style4.NumberFormat = "0";

            Style style5 = styles.Add("Style 5");
            style5.Fill.PatternType = PatternType.Solid;
            style5.Fill.BackgroundColor = Color.FromArgb(255, 238, 238, 238);
            style5.Font.Color = Color.FromArgb(255, 89, 89, 89);
            style5.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            style5.NumberFormat = "0";
        }
        void SetColumnsWidth() {
            sheet1.Columns[0].WidthInCharacters = 2.42;
            sheet1.Columns[1].WidthInCharacters = 9.69;
            sheet1.Columns[2].WidthInCharacters = 18.03;
            sheet1.Columns[3].WidthInCharacters = 9.01;
            sheet1.Columns[4].WidthInCharacters = 8.34;
            sheet1.Columns[5].WidthInCharacters = 8.48;
            sheet1.Columns[6].WidthInCharacters = 8.07;
            sheet1.Columns[7].WidthInCharacters = 8.07;
            sheet1.Columns[8].WidthInCharacters = 8.07;
            sheet1.Columns[9].WidthInCharacters = 8.07;
            sheet1.Columns[10].WidthInCharacters = 2.56;
            sheet1.Columns[11].WidthInCharacters = 6.59;
            sheet1.Columns[12].WidthInCharacters = 18.03;
            sheet1.Columns[13].WidthInCharacters = 7.67;
            sheet1.Columns[14].WidthInCharacters = 3.63;

            sheet2.Columns[0].WidthInCharacters = 2.69;
            sheet2.Columns[1].WidthInCharacters = 9.69;
            sheet2.Columns[2].WidthInCharacters = 10.36;
            sheet2.Columns[3].WidthInCharacters = 3.63;
            sheet2.Columns[4].WidthInCharacters = 6.59;
            sheet2.Columns[5].WidthInCharacters = 18.03;
            sheet2.Columns[6].WidthInCharacters = 7.67;
            sheet2.Columns[7].WidthInCharacters = 3.63;
        }
        void SetRowsHeight() {
            sheet1.Rows[0].Height = 51.95;
            sheet1.Rows[1].Height = 23.25;
            sheet1.Rows[23].Height = 15;
            sheet1.Rows[24].Height = 15;

            sheet2.Rows[0].Height = 51.95;
            sheet2.Rows[1].Height = 23.25;
            sheet2.Rows[24].Height = 15;
        }
    }
}
