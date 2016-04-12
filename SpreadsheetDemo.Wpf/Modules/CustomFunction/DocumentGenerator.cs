using System;
using System.Collections.Generic;
using DevExpress.Office;
using DevExpress.Spreadsheet;
using System.Drawing;
using SpreadsheetFormatting = DevExpress.Spreadsheet.Formatting;
using System.Globalization;
using DevExpress.Spreadsheet.Functions;
using DevExpress.Docs.Text;

namespace DevExpress.Spreadsheet.Demos {
    public class CustomFunctionDocumentGenerator {
        const int languageCount = 14;
        const int firstValueRowIndex = 8;
        const int lastValueRowIndex = firstValueRowIndex + (languageCount - 1) * 2;

        const double borderRowHeight = 16.5;
        const double separateRowHeight = 6;
        const double valueRowHeight = 16.5;

        IWorkbook book;
        Worksheet sheet;
        ColumnCollection columns;
        RowCollection rows;
        CellCollection cells;
        StyleCollection styles;
        char separator;

        public CustomFunctionDocumentGenerator(CultureInfo culture) {
            this.book = new Workbook();
            this.book.Options.Culture = culture;
            InitCultureParams(culture);
        }
        public CustomFunctionDocumentGenerator(IWorkbook book) {
            this.book = book;
            InitCultureParams(book.Options.Culture);
        }
        void InitCultureParams(CultureInfo culture) {
            this.separator = culture.TextInfo.ListSeparator[0];
        }
        public IWorkbook Generate() {
            InitializeBook();
            return book;
        }

        void InitializeBook() {
            InitWorkbook();
            SetRowsHeight();
            CreateStyles();
            FormatCells();
            ApplyStyles();
            SetBorders();
            FillData();
            ApplyCustomFunction();
            SetColumnsWidth();
            sheet["C4"].Select();
        }
        void InitWorkbook() {
            book.Unit = DevExpress.Office.DocumentUnit.Point;

            book.Styles.DefaultStyle.Font.Name = "Calibri";
            book.Styles.DefaultStyle.Font.Size = 11;

            sheet = book.Worksheets[0];
            columns = sheet.Columns;
            rows = sheet.Rows;
            cells = sheet.Cells;

            sheet.ActiveView.Zoom = 100;
            sheet.ActiveView.ShowGridlines = false;

            sheet.DefaultRowHeight = 12.75;
            sheet.Name = "Number In Words";
            sheet.ActiveView.Orientation = PageOrientation.Landscape;
            sheet.PrintOptions.FitToPage = true;

            styles = book.Styles;
        }
        void ApplyCustomFunction() {
            int cultureIndex = 1;
            for (int i = firstValueRowIndex; i <= lastValueRowIndex; i = i + 2) {
                cells["C" + i].Formula = String.Format("=SPELLNUMBER(C4{0}{1})", separator, cultureIndex);
                cells["E" + i].Formula = String.Format("=SPELLNUMBER(C4{0}{1}{0}true)", separator, cultureIndex);
                cultureIndex++;
            }
        }
        void ApplyStyles() {
            sheet["C4"].Style = styles["Style 1"];

            for (int i = firstValueRowIndex; i <= lastValueRowIndex; i = i + 2) {
                sheet["C" + i].Style = styles["Style 1"];
                sheet["E" + i].Style = styles["Style 1"];
            }
        }
        void CreateStyles() {
            Style style1 = styles.Add("Style 1");
            style1.Font.Size = 12;
            style1.Fill.PatternType = PatternType.Solid;
            style1.Fill.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            style1.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            style1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            style1.Alignment.Indent = 1;
        }
        void SetBorders() {
            sheet["C4"].Borders.SetOutsideBorders(Color.FromArgb(255, 191, 191, 191), BorderLineStyle.Thin);

            for (int i = firstValueRowIndex; i <= lastValueRowIndex; i = i + 2) {
                sheet["C" + i].Borders.SetOutsideBorders(Color.FromArgb(255, 191, 191, 191), BorderLineStyle.Thin);
                sheet["E" + i].Borders.SetOutsideBorders(Color.FromArgb(255, 191, 191, 191), BorderLineStyle.Thin);
            }
        }
        void FormatCells() {
            Range range1 = sheet["B2"];
            Formatting rangeFormatting1 = range1.BeginUpdateFormatting();
            rangeFormatting1.Font.Color = Color.FromArgb(255, 64, 64, 64);
            rangeFormatting1.Font.Size = 28;
            rangeFormatting1.Font.Name = "Calibri Light";
            rangeFormatting1.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range1.EndUpdateFormatting(rangeFormatting1);

            Range range2 = sheet["B3:F" + (lastValueRowIndex + 1)];
            Formatting rangeFormatting2 = range2.BeginUpdateFormatting();
            rangeFormatting2.Fill.PatternType = PatternType.Solid;
            rangeFormatting2.Fill.BackgroundColor = Color.FromArgb(255, 242, 242, 242);
            rangeFormatting2.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            range2.EndUpdateFormatting(rangeFormatting2);

            Range range3 = sheet["B4:B" + lastValueRowIndex];
            Formatting rangeFormatting3 = range3.BeginUpdateFormatting();
            rangeFormatting3.Font.Size = 14;
            rangeFormatting3.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Left;
            rangeFormatting3.Alignment.Indent = 2;
            range3.EndUpdateFormatting(rangeFormatting3);

            Range range4 = sheet["C6:E6"];
            Formatting rangeFormatting4 = range4.BeginUpdateFormatting();
            rangeFormatting4.Font.Color = Color.FromArgb(255, 128, 128, 128);
            rangeFormatting4.Font.Size = 14;
            range4.EndUpdateFormatting(rangeFormatting4);
        }
        void SetRowsHeight() {
            rows[1].Height = 36;
            rows[2].Height = borderRowHeight;
            rows[3].Height = valueRowHeight;

            for (int i = 4; i < lastValueRowIndex; i += 2) {
                rows[i].Height = separateRowHeight;
                rows[i + 1].Height = valueRowHeight;
            }

            rows[lastValueRowIndex].Height = borderRowHeight;
        }
        void SetColumnsWidth() {
            columns[0].WidthInCharacters = 2.29;
            columns[1].WidthInCharacters = 33.77;
            columns[3].WidthInCharacters = 3.09;
            columns[5].WidthInCharacters = 3.09;

            columns[2].AutoFit();
            columns[4].AutoFit();
        }
        void FillData() {
            NumberInWordsFunction customFunction = new NumberInWordsFunction();
            if (!book.GlobalCustomFunctions.Contains(((ICustomFunction)customFunction).Name))
                book.GlobalCustomFunctions.Add(customFunction);

            cells["B2"].Value = "NUMBER IN WORDS";
            cells["B4"].Value = "number";
            cells["C4"].Value = 1235;
            cells["C6"].Value = "cardinal number in words                  ";
            cells["E6"].Value = "ordinal number in words                   ";

            cells["B8"].Value = "English";
            cells["B10"].Value = "English(United Kingdom)";
            cells["B12"].Value = "French(Français)";
            cells["B14"].Value = "German(Deutsch)";
            cells["B16"].Value = "Greek(Ελληνικά)";
            cells["B18"].Value = "Hindi(हिन्दी)";
            cells["B20"].Value = "Italian(Italiano)";
            cells["B22"].Value = "Portuguese(Português)";
            cells["B24"].Value = "Russian(Русский)";
            cells["B26"].Value = "Spanish(Español)";
            cells["B28"].Value = "Swedish(Svensk)";
            cells["B30"].Value = "Thai(ไทย)";
            cells["B32"].Value = "Turkish(Türkçe)";
            cells["B34"].Value = "Ukrainian(Український)";
        }
    }

    public class NumberInWordsFunction : Object, ICustomFunction {
        const string functionName = "SPELLNUMBER";
        readonly ParameterInfo[] functionParameters;
        static List<CultureInfo> cultureInfoParamTable = CreateCultureInfoParamTable();

        public NumberInWordsFunction() {
            this.functionParameters = new ParameterInfo[] { new ParameterInfo(ParameterType.Value), new ParameterInfo(ParameterType.Value), new ParameterInfo(ParameterType.Value, ParameterAttributes.Optional) };
        }

        string IFunction.Name { get { return functionName; } }
        ParameterInfo[] IFunction.Parameters { get { return functionParameters; } }
        ParameterType IFunction.ReturnType { get { return ParameterType.Value; } }
        bool IFunction.Volatile { get { return true; } }

        ParameterValue IFunction.Evaluate(IList<ParameterValue> parameters, EvaluationContext context) {
            bool isOrdinal = false;
            ParameterValue numberValue = parameters[0];
            if (numberValue.IsError)
                return numberValue;

            ParameterValue cultureValue = parameters[1];
            if (cultureValue.IsError)
                return cultureValue;
            if (cultureValue.NumericValue < 1 || cultureValue.NumericValue > cultureInfoParamTable.Count)
                return ParameterValue.ErrorNumber;

            if (parameters.Count == 3) {
                ParameterValue ordinalValue = parameters[1];
                if (ordinalValue.IsError)
                    return ordinalValue;

                isOrdinal = ordinalValue.BooleanValue;
            }

            double number = numberValue.NumericValue;
            CultureInfo culture = cultureInfoParamTable[(int)cultureValue.NumericValue - 1];

            if (number < 0 || number > long.MaxValue)
                return ParameterValue.ErrorNumber;

            if (isOrdinal)
                return NumberInWords.Ordinal.ConvertToText((long)Math.Round(number), culture);
            else
                return NumberInWords.Cardinal.ConvertToText((long)Math.Round(number), culture);
        }
        string IFunction.GetName(CultureInfo culture) {
            return functionName;
        }
        static List<CultureInfo> CreateCultureInfoParamTable() {
            List<CultureInfo> result = new List<CultureInfo>();
            result.Add(CultureInfo.GetCultureInfo("en-US"));
            result.Add(CultureInfo.GetCultureInfo("en-GB"));
            result.Add(CultureInfo.GetCultureInfo("fr-FR"));
            result.Add(CultureInfo.GetCultureInfo("de-DE"));
            result.Add(CultureInfo.GetCultureInfo("el-GR"));
            result.Add(CultureInfo.GetCultureInfo("hi-IN"));
            result.Add(CultureInfo.GetCultureInfo("it-IT"));
            result.Add(CultureInfo.GetCultureInfo("pt-PT"));
            result.Add(CultureInfo.GetCultureInfo("ru-RU"));
            result.Add(CultureInfo.GetCultureInfo("es-ES"));
            result.Add(CultureInfo.GetCultureInfo("sv-SE"));
            result.Add(CultureInfo.GetCultureInfo("th-TH"));
            result.Add(CultureInfo.GetCultureInfo("tr-TR"));
            result.Add(CultureInfo.GetCultureInfo("uk-UA"));
            return result;
        }
    }
}
