using System;
using System.Windows.Forms;
using DevExpress.Office.Internal;
using DevExpress.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace RichEditDemo {
    public partial class HyperlinksAndBookmarks : RichEditDemoModule {
        public HyperlinksAndBookmarks() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("Hyperlinks.docx", richEdit);
            ribbonControl1.SelectedPage = pageInsert;
            InsertLinksInDocument();
        }

        private void InsertLinksInDocument() {
            Document doc = this.richEdit.Document;
            DocumentRange[] foundRanges = doc.FindAll("DevExpress WinForms Rich Text Editor", SearchOptions.CaseSensitive);
            if (foundRanges.Length > 0) {
                Hyperlink hlink = doc.Hyperlinks.Create(foundRanges[0]);
                hlink.NavigateUri = "https://www.devexpress.com/Products/NET/Controls/WinForms/rich_editor/";
            }

            Bookmark bm = doc.Bookmarks.Create(doc.Range.Start, 1, "Top");
            doc.Paragraphs.Append();
            DocumentRange lastRange = doc.AppendText("To the Top");
            Hyperlink hlinkBookmark = doc.Hyperlinks.Create(lastRange);
            hlinkBookmark.Anchor = "Top";
        }

        private void edtShowBoomarks_Checked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Bookmarks.Visibility = RichEditBookmarkVisibility.Visible;
        }

        private void edtShowBookmarks_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Bookmarks.Visibility = RichEditBookmarkVisibility.Hidden;
        }

        private void colorEdit1_ColorChanged(object sender, System.Windows.RoutedEventArgs e) {
            UpdateBookmarkColor();
        }
        private void UpdateBookmarkColor() {
            System.Windows.Media.Color mediacolor = colorEdit1.Color;
            richEdit.Options.Bookmarks.Color = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        }

        private void edtShowTooltip_Checked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ShowToolTip = true;
        }

        private void edtShowTooltip_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ShowToolTip = false;
        }

        private void edtCtrl_Checked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys |= Keys.Control;
        }

        private void edtCtrl_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys &= ~Keys.Control;
        }

        private void edtAlt_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys &= ~Keys.Alt;
        }

        private void edtAlt_Checked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys |= Keys.Alt;
        }

        private void edtShift_Checked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys |= Keys.Shift;
        }

        private void edtShift_Unchecked(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Hyperlinks.ModifierKeys &= ~Keys.Shift;
        }

        private void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Options.Bookmarks.Visibility = RichEditBookmarkVisibility.Visible;
            UpdateBookmarkColor();
        }
    }
}
