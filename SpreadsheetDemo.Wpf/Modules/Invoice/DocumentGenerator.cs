using System;
using System.Data;
using System.Collections.Generic;
using DevExpress.Spreadsheet;
using System.Text;
using System.Drawing;
using DevExpress.Utils;

namespace DevExpress.XtraSpreadsheet.Demos {
    #region InvoiceInMemoryData
    internal class InvoiceInMemoryData {
        public string ProductName;
        public double Price;
        public double Quantity;
        public double Discount;
        public InvoiceInMemoryData(double q) {
            ProductName = string.Empty;
            Price = 0;
            Quantity = q;
            Discount = 0;
        }

        public InvoiceInMemoryData(string productName, double price) {
            ProductName = productName;
            Price = price;
            Quantity = 0;
            Discount = 0;
        }
    }
    #endregion

    internal class InvoiceDocumentGenerator {
        #region Fields
        const int maxItemsInInvoice = 18;
        const int goodsStartRow = 16;
        const string accountingFormat = "_($* #,##0.00_);_($* (#,##0.00);_($* \" - \"??_);_(@_)";
        const string amountNumberFormat = "_(* #,##0.00_);_(* (#,##0.00);_(* \" - \"??_);_(@_)";
        const string dateNumberFormat = "mmmm d, yyyy";
        const string CurrencyWithRedNegativeAndEmptyZero = "_-[$$-409]* #,##0.00_ ;_-[$$-409]* \\-#,##0.00\\ ;;_-@_ ";
        const string PercentageWithRedNegativeAndEmptyZero = "0.00%;[Red]-0.00%;;@";
        const string defaultRegularFontName = @"Segoe UI";
        const string defaultLightFontName = @"Segoe UI Light";
        const float defaultFontSize = 10.5f;
        readonly Color watermarkColor = DXColor.FromArgb(0xff, 0xE0, 0xE0, 0xE0);
        readonly Color goodsEvenColor = DXColor.FromArgb(0xff, 0xF1, 0xF1, 0xF1);
        Worksheet sheet;
        IWorkbook workbook;
        public const string FromCompanyId = "fromCompany";
        public const string FromContactNameId = "fromContactName";
        public const string FromEMailId = "fromEMail";
        public const string FromCompanySloganId = "fromCompanySlogan";
        public const string FromAddressId = "fromAddress";
        public const string FromCityId = "fromCity";
        public const string FromStateId = "fromState";
        public const string FromZipId = "fromZip";
        public const string FromPhoneId = "fromPhone";
        public const string FromFaxId = "fromFax";
        public const string ToNameId = "toName";
        public const string ToCompanyId = "toCompany";
        public const string ToStreetId = "toStreet";
        public const string ToCityId = "toCity";
        public const string ToStateId = "toState";
        public const string ToZipId = "toZip";
        public const string ToPhoneId = "toPhone";
        Dictionary<string, string> invoiceParameters;
        List<InvoiceInMemoryData> invoiceGoods;

        #endregion

        public InvoiceDocumentGenerator(IWorkbook workbook) {
            this.workbook = workbook;
            invoiceParameters = new Dictionary<string, string>();
            invoiceGoods = new List<InvoiceInMemoryData>();
            InitializeBook();
        }

        public IWorkbook Workbook { get { return workbook; } }
        public Dictionary<string, string> InvoiceParameters { get { return invoiceParameters; } }
        public List<InvoiceInMemoryData> InvoiceGoods { get { return invoiceGoods; } protected set { invoiceGoods = value; } }

        public void Generate() {
            FillInvoiceSheet();
            Workbook.History.Clear();
        }

        void InitializeBook() {
            PrepareBook(Workbook);
        }

        void FillInvoiceSheet() {
            if (!Workbook.Worksheets.Contains(sheet.Name))
                return;

            Workbook.BeginUpdate();
            try {
                Range usedRange = sheet.GetUsedRange();
                sheet.UnMergeCells(usedRange);
                sheet.Clear(usedRange);

                PrepareColumns(sheet);
                PrepareRows(sheet);

                Cell currentCell = sheet.Cells[0, 1];
                currentCell.Font.Name = defaultLightFontName;
                currentCell.Font.Size = 27;
                currentCell.Alignment.Vertical = SpreadsheetVerticalAlignment.Bottom;

                currentCell = sheet.Cells[2, 1];
                currentCell.Font.FontStyle = SpreadsheetFontStyle.BoldItalic;
                currentCell.Font.Size = 13.5;

                SetLeftAlignedBoldValue(sheet.Cells[3, 4], "DATE:");
                SetLeftAlignedBoldValue(sheet.Cells[4, 4], "INVOICE #:");
                SetLeftAlignedBoldValue(sheet.Cells[5, 4], "FOR:");

                PutWatermarkStyleValue(sheet.Cells[0, 5]);

                currentCell = sheet.Cells[3, 5];
                currentCell.NumberFormat = dateNumberFormat;
                currentCell.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
                currentCell.Formula = "=TODAY()";

                currentCell = sheet.Cells[4, 5];
                currentCell.NumberFormat = "0";
                currentCell.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
                currentCell.Value = 100;

                currentCell = sheet.Cells[5, 5];
                currentCell.Alignment.WrapText = true;
                currentCell.Font.Italic = true;
                currentCell.Value = "Project or service description";

                currentCell = sheet.Cells[7, 1];
                currentCell.Font.Size = 13.5;
                currentCell.Font.Bold = true;
                currentCell.Value = "BILL TO:";

                FillInvoiceCore();
            }
            finally {
                Workbook.EndUpdate();
            }
        }

        public void ApplyParameter(string key, string value) {
            if (invoiceParameters.ContainsKey(key))
                invoiceParameters[key] = value;
            else
                invoiceParameters.Add(key, value);
        }

        string GetParameter(string key) {
            string result = string.Empty;
            if (!invoiceParameters.TryGetValue(key, out result))
                result = string.Empty;
            return result;
        }

        public object GetGoodsAttribute(string key, int rowIndex) {
            object result = null;
            switch (key) {
                case "Quantity":
                    result = invoiceGoods[rowIndex].Quantity;
                    break;
                case "AppendIntoInvoice":
                    result = invoiceGoods[rowIndex].Quantity > 0;
                    break;
                case "Discount":
                    result = invoiceGoods[rowIndex].Discount * 100;
                    break;
                case "ProductName":
                    result = invoiceGoods[rowIndex].ProductName;
                    break;
                case "UnitPrice":
                    result = invoiceGoods[rowIndex].Price;
                    break;
            }
            return result;
        }
        public bool SetGoodsAttribute(string key, int rowIndex, object value) {
            bool result = false;
            switch (key) {
                case "Quantity":
                    invoiceGoods[rowIndex].Quantity = Convert.ToDouble(value);
                    break;
                case "AppendIntoInvoice":
                    bool boolValue = Convert.ToBoolean(value);
                    double newValue = boolValue ? 1 : 0;
                    invoiceGoods[rowIndex].Quantity = newValue;
                    result = true;
                    break;
                case "Discount":
                    invoiceGoods[rowIndex].Discount = Convert.ToDouble(value) * 0.01;
                    break;
            }
            return result;
        }

        public void ClearGoods() {
            if (invoiceGoods == null)
                invoiceGoods = new List<InvoiceInMemoryData>();
            else
                invoiceGoods.Clear();
        }

        public void AddGoods(InvoiceInMemoryData value) {
            invoiceGoods.Add(value);
        }

        public void SelectRandomGoods() {
            if ((invoiceGoods == null) || (invoiceGoods.Count < 1))
                return;
            Random random = new Random();
            for (int i = 0; i < 8; ++i) {
                int itemIndex = random.Next(invoiceGoods.Count);
                double itemQuantity = random.Next(20) + 1;
                invoiceGoods[itemIndex].Quantity = itemQuantity;
            }
        }

        #region book default values, sheet names, non-default columns and rows dimensions
        void PrepareBook(IWorkbook book) {
            int numberOfSheets = book.Worksheets.Count;
            for(int i = numberOfSheets - 1; i > 0; --i)
                book.Worksheets.RemoveAt(i);
            book.Unit = Office.DocumentUnit.Point;
            book.Styles.DefaultStyle.Font.Name = defaultRegularFontName;
            book.Styles.DefaultStyle.Font.Size = defaultFontSize;
            book.Styles.DefaultStyle.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            sheet = book.Worksheets[0];
            sheet.Name = "SimpleInvoice";
            sheet.ActiveView.ShowGridlines = false;
            sheet.PrintOptions.FitToPage = true;
        }

        void PrepareColumns(Worksheet sheet) {
            sheet.Columns[0].WidthInCharacters = 6;
            sheet.Columns[1].WidthInCharacters = 47.86;
            sheet.Columns[2].WidthInCharacters = 12;
            sheet.Columns[3].WidthInCharacters = 18;
            sheet.Columns[4].WidthInCharacters = 16;
            sheet.Columns[5].WidthInCharacters = 21;
        }

        void PrepareRows(Worksheet sheet) {
            Row currentRow = sheet.Rows[0];
            currentRow.Height = 52.5;

            currentRow = sheet.Rows[2];
            currentRow.Height = Math.Min(sheet.DefaultRowHeight * 2, 400);

            Range range = sheet["2:6"];
            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Top;

            currentRow = sheet.Rows[5];
            currentRow.Height = 30;

            range = sheet["7:11"];
            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Bottom;

            range = sheet["16:24"];
            range.RowHeight = 21;

            range = sheet["37:38"];
            range.Alignment.Vertical = SpreadsheetVerticalAlignment.Bottom;
        }
        #endregion

        #region cell and range decoration primitives
        void CreateColumnTitle(Range range, string text) {
            Formatting style = range.BeginUpdateFormatting();
            try {
                style.Font.Name = defaultRegularFontName;
                style.Font.FontStyle = SpreadsheetFontStyle.Bold;
                style.Font.Color = DXColor.FromArgb(0xff, 0x33, 0x33, 0x33);
                style.Borders.BottomBorder.LineStyle = BorderLineStyle.Medium;
                style.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            }
            finally {
                range.EndUpdateFormatting(style);
            }
            range.Value = text;
        }

        void CreateTableColumn(Worksheet sheet, int column, int row, int rowCount, string caption, string numberFormat) {
            CreateTableColumn(sheet, column, row, rowCount, caption, numberFormat, Spreadsheet.SpreadsheetHorizontalAlignment.General, 0);
        }

        void CreateTableColumn(Worksheet sheet, int column, int row, int rowCount, string caption, string numberFormat, Spreadsheet.SpreadsheetHorizontalAlignment  SpreadsheetHorizontalAlignment, int indent) {
            CreateColumnTitle(sheet.Range.FromLTRB(column, row, column, row), caption);
            Range range = sheet.Range.FromLTRB(column, row, column, row + rowCount);
            Formatting style = range.BeginUpdateFormatting();
            try {
                style.Alignment.Horizontal =  SpreadsheetHorizontalAlignment;
                if(!string.IsNullOrEmpty(numberFormat))
                    style.NumberFormat = numberFormat;
                style.Alignment.Indent = indent;
            }
            finally {
                range.EndUpdateFormatting(style);
            }
        }

        void SetCenteredBoldValue(Cell cell, string value) {
            Formatting newFmt = cell.BeginUpdateFormatting();
            try {
                newFmt.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Center;
                newFmt.Font.Bold = true;
                newFmt.Font.Name = defaultRegularFontName;
                newFmt.Font.Size = 13.5;
            }
            finally {
                cell.EndUpdateFormatting(newFmt);
            }
            cell.Value = value;
        }

        void SetLeftAlignedBoldValue(Cell cell, string text) {
            Formatting newFmt = cell.BeginUpdateFormatting();
            try {
                newFmt.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
                newFmt.Font.Name = defaultRegularFontName;
                newFmt.Font.Bold = true;
                newFmt.Font.Size = defaultFontSize;
                newFmt.Font.Color = Color.FromArgb(0xFF, 0xBB, 0xBA, 0xBA);
            }
            finally {
                cell.EndUpdateFormatting(newFmt);
            }
            cell.Value = text;
        }

        void PutWatermarkStyleValue(Cell cell) {
            Formatting newFmt = cell.BeginUpdateFormatting();
            try {
                newFmt.Font.Size = 36;
                newFmt.Font.FontStyle = SpreadsheetFontStyle.BoldItalic;
                newFmt.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Right;
                newFmt.Alignment.Vertical = SpreadsheetVerticalAlignment.Bottom;
                newFmt.Font.Color = watermarkColor;
            }
            finally {
                cell.EndUpdateFormatting(newFmt);
            }
            cell.Value = "INVOICE";
        }

        void CreateContactInfoRange(Range range) {
            range.Merge();
            range.RowHeight = 20;
            Formatting style = range.BeginUpdateFormatting();
            try {
                style.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
                style.Alignment.WrapText = true;
            }
            finally {
                range.EndUpdateFormatting(style);
            }
        }

        void SetInvoiceTotalCell(Cell cell, int goodsCount) {
            Formatting newFmt = cell.BeginUpdateFormatting();
            try {
                newFmt.NumberFormat = accountingFormat;
                newFmt.Font.Color = DXColor.Black;
                newFmt.Fill.BackgroundColor = DXColor.FromArgb(0xff, 0xea, 0xea, 0xea);
                newFmt.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Center;
                newFmt.Font.Size = 13.5;
            }
            finally {
                cell.EndUpdateFormatting(newFmt);
            }
            string formula = String.Format("=SUM(F{0}:F{1})", goodsStartRow + 1, goodsStartRow + goodsCount);
            cell.Formula = formula;
        }

        void CreateThankfulRange(Range range) {
            range.Merge();
            Formatting style = range.BeginUpdateFormatting();
            try {
                style.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
                style.Font.FontStyle = SpreadsheetFontStyle.BoldItalic;
                style.Font.Name = defaultRegularFontName;
            }
            finally {
                range.EndUpdateFormatting(style);
            }
            range.Value = "THANK YOU FOR YOUR BUSINESS!";
        }
        #endregion


        #region Cell values primitives
        void ApplySimpleCellValue(string value, int column, int row) {
            Worksheet currentSheet = Workbook.Worksheets[0];
            Cell currentCell = currentSheet.Cells[row, column];
            currentCell.Value = value;
        }

        void ApplyComplexCellValue(int column, int row, string format, params object[] values) {
            Worksheet currentSheet = Workbook.Worksheets[0];
            Cell currentCell = currentSheet.Cells[row, column];
            object[] formatValues = new object[values.Length];
            for (int i = 0; i < values.Length; ++i) {
                formatValues[i] = values[i];
            }
            currentCell.Value = string.Format(format, values);
        }
        #endregion

        void CreateVariativePart(int goodsCount) {
            CreateTableColumn(sheet, 1, 15, 18, "Description", string.Empty, Spreadsheet.SpreadsheetHorizontalAlignment.Left, 1);
            CreateTableColumn(sheet, 2, 15, 18, "QTY", string.Empty, Spreadsheet.SpreadsheetHorizontalAlignment.Center, 0);
            CreateTableColumn(sheet, 3, 15, 18, "Price", CurrencyWithRedNegativeAndEmptyZero, Spreadsheet.SpreadsheetHorizontalAlignment.Center, 0);
            CreateTableColumn(sheet, 4, 15, 18, "Discount", PercentageWithRedNegativeAndEmptyZero, Spreadsheet.SpreadsheetHorizontalAlignment.Center, 0);
            CreateTableColumn(sheet, 5, 15, 18, "Amount", CurrencyWithRedNegativeAndEmptyZero, Spreadsheet.SpreadsheetHorizontalAlignment.Center, 0);

            string range = String.Format("F{0}:F{1}", goodsStartRow + 1, goodsStartRow + goodsCount + 1);
            string formula = String.Format("C{0}*D{0}*(1-E{0})", goodsStartRow + 1);

            sheet[range].Formula = formula;

            int totalRowIndex = goodsStartRow + goodsCount + 1;
            SetCenteredBoldValue(sheet.Cells[totalRowIndex, 4], "Total");
            SetInvoiceTotalCell(sheet.Cells[totalRowIndex, 5], goodsCount);

            Cell currentCell = sheet.Cells[totalRowIndex + 2, 1];
            currentCell.Alignment.Horizontal = Spreadsheet.SpreadsheetHorizontalAlignment.Left;
            currentCell.NumberFormat = "0";
            currentCell.Value = "Make all checks payable to Your Company Name";

            CreateContactInfoRange(sheet.Range.FromLTRB(1, totalRowIndex + 3, 5, totalRowIndex + 4));

            CreateThankfulRange(sheet.Range.FromLTRB(1, totalRowIndex + 6, 5, totalRowIndex + 6));
        }

        #region Transfer selected products to invoice
        int PopulateInvoiceGoodsList() {
            int goodsCount = 0;
            Worksheet currentSheet = Workbook.Worksheets[0];
            for (int i = 0; i < invoiceGoods.Count; ++i) {
                if (invoiceGoods[i].Quantity > 0) {
                    int currentRowIndex = goodsStartRow + goodsCount;
                    Row currentRow = currentSheet.Rows[currentRowIndex];
                    InvoiceInMemoryData item = invoiceGoods[i];
                    currentRow[1].Value = item.ProductName;
                    currentRow[2].Value = item.Quantity;
                    currentRow[3].Value = item.Price;
                    currentRow[4].Value = item.Discount;
                    if ((goodsCount % 2) != 0)
                        SetRangeFillColor(currentSheet.Range.FromLTRB(1, currentRowIndex, 5, currentRowIndex), goodsEvenColor);
                    ++goodsCount;
                    if (goodsCount >= maxItemsInInvoice)
                        break;
                }
            }
            return goodsCount;
        }
        #endregion

        void SetRangeFillColor(Range currentRange, Color color) {
            Formatting newFmt = currentRange.BeginUpdateFormatting();
            try {
                newFmt.Fill.BackgroundColor = color;
            }
            finally {
                currentRange.EndUpdateFormatting(newFmt);
            }
        }

        #region Set invoice caption
        void FillInvoiceCore() {
            ApplySimpleCellValue(GetParameter(FromCompanyId), 1, 0);
            ApplySimpleCellValue(GetParameter(FromCompanySloganId), 1, 2);
            ApplySimpleCellValue(GetParameter(FromAddressId), 1, 3);
            ApplyComplexCellValue(1, 4, "{0}, {1}, {2}", GetParameter(FromCityId), GetParameter(FromStateId), GetParameter(FromZipId));
            ApplyComplexCellValue(1, 5, "Phone: {0}, Fax: {1}", GetParameter(FromPhoneId), GetParameter(FromFaxId));
            ApplySimpleCellValue(GetParameter(ToNameId), 1, 8);
            ApplySimpleCellValue(GetParameter(ToCompanyId), 1, 9);
            ApplySimpleCellValue(GetParameter(ToStreetId), 1, 10);
            ApplyComplexCellValue(1, 11, "{0}, {1}, {2}", GetParameter(ToCityId), GetParameter(ToStateId), GetParameter(ToZipId));
            ApplyComplexCellValue(1, 12, "Phone: {0}", GetParameter(ToPhoneId));
            int goodsRowCount = PopulateInvoiceGoodsList();
            CreateVariativePart(goodsRowCount);
            int footerRowIndex = goodsStartRow + goodsRowCount + 3;
            ApplyComplexCellValue(1, footerRowIndex, "Make all checks payable to {0}", GetParameter(FromCompanyId));
            ApplyComplexCellValue(1, footerRowIndex + 1, "If you have any questions concerning this invoice, contact {0}, {1}, {2}", GetParameter(FromContactNameId), GetParameter(FromPhoneId), GetParameter(FromEMailId));
        }

        void FillInvoice() {
            Workbook.BeginUpdate();
            try {
                FillInvoiceCore();
            }
            finally {
                Workbook.EndUpdate();
            }
        }
        #endregion
    }
}
