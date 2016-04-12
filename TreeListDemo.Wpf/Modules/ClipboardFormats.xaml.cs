using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;


namespace TreeListDemo {
    public partial class ClipboardFormats : TreeListDemoModule {
        protected override bool ShowBorder { get { return true; } }
        public ClipboardFormats() {
            InitializeComponent();
            view.SelectCells(1, treeList.Columns.First(), 15, treeList.Columns.Last());
            CopyAndPasteClipboardData();
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            CopyAndPasteClipboardData();
        }
        void CopyAndPasteClipboardData() {
            view.CopySelectedCellsToClipboard();
            PasteClipboardData();
        }
        void PasteClipboardData() {
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
            spreadsheetControl.LoadDocument(Clipboard.GetDataObject().GetData("Biff8") as Stream, DevExpress.Spreadsheet.DocumentFormat.Xls);
            spreadsheetControl.ActiveWorksheet.DefaultColumnWidthInPixels = (int)spreadsheetControl.ActualWidth / treeList.Columns.Count;
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
