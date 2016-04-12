using System;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    [CodeFile("ModuleResources/ConditionalFormatting/ConditionalFormattingViewModel.cs")]
    [CodeFile("ModuleResources/ConditionalFormatting/SaleOverviewData.cs")]
    public partial class ClipboardFormats : GridDemoModule {
        public ClipboardFormats() {
            InitializeComponent();
            tableView.SelectCells(5, grid.Columns.First(), 15, grid.Columns.Last());
            tableView.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        protected override bool IsGridBorderVisible { get { return true; } }
        private void Button_Click(object sender, RoutedEventArgs e) {
            tableView.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        internal void PasteClipboardData() {
            PasteHTMLFormat();
            PasteXLSFormat();
            PasteRTFFormat();
        }
        void PasteRTFFormat() {
            richEditControl.Document.RtfText = string.Empty;
            richEditControl.Document.Text = string.Empty;
            richEditControl.Document.AppendRtfText(Clipboard.GetText(TextDataFormat.Rtf));
        }
        void PasteXLSFormat() {
            spreadsheetControl.ActiveWorksheet.Clear(spreadsheetControl.ActiveWorksheet.GetDataRange());
            spreadsheetControl.LoadDocument(Clipboard.GetDataObject().GetData("Biff8") as Stream, DocumentFormat.Xls);
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = (int)spreadsheetControl.ActualWidth / grid.Columns.Count;
        }
        void PasteHTMLFormat() {
            string html = Clipboard.GetText(TextDataFormat.Html);
            if(string.IsNullOrEmpty(html))
                webBrowser.NavigateToString(" ");
            else {
                html = html.Remove(0, html.Substring(0, html.IndexOf("<html", StringComparison.OrdinalIgnoreCase)).Length);
                webBrowser.NavigateToString(html);
            }
        }
        public override void Leave() {
            webBrowser.Visibility = Visibility.Collapsed;
        }
        public override void Show() {
            webBrowser.Visibility = Visibility.Visible;
        }
        protected override void Clear() {
            base.Clear();
            webBrowser.Visibility = System.Windows.Visibility.Collapsed;
            webBrowser.Source = null;
            webBrowser.Dispose();
        }

    }
}
