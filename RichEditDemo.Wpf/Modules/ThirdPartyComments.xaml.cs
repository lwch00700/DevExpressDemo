using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.XtraRichEdit;

namespace RichEditDemo {
    public partial class ThirdPartyComments : RichEditDemoModule {
        public ThirdPartyComments() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("Comments.docx", richEditControl1);
            ribbonControl1.SelectedPage = pageReview;
        }

        private void richEditControl1_Loaded(object sender, RoutedEventArgs e) {
            richEditControl1.Options.Comments.ShowAllAuthors = true;
            richEditControl1.Options.Comments.Visibility = RichEditCommentVisibility.Visible;
            FetchUsers();
        }
        void FetchUsers(){
            cbAuthor.Items.Clear();
            ObservableCollection<string> authors = richEditControl1.Options.Comments.VisibleAuthors;
            foreach (string author in authors) {
                cbAuthor.Items.Add(author);
            }
            cbAuthor.EditValue = cbAuthor.Items[0];
            UpdateAuthor(this.cbAuthor.EditValue.ToString());
        }

        private void UpdateAuthor(string author) {
            richEditControl1.Options.Comments.Author = author;
        }

        private void UpdateCommentColor() {
            System.Windows.Media.Color mediacolor = colorEdit1.Color;
            richEditControl1.Options.Comments.Color = System.Drawing.Color.FromArgb(mediacolor.A, mediacolor.R, mediacolor.G, mediacolor.B);
        }

        private void cbUserAuthor_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            UpdateAuthor(this.cbAuthor.EditValue.ToString());
        }

        private void edtViewComments_Checked(object sender, RoutedEventArgs e) {
            this.richEditControl1.Options.Comments.Visibility = RichEditCommentVisibility.Visible;
            edtHighlightCommentedRange.IsEnabled = false;
        }

        private void edtViewComments_Unchecked(object sender, RoutedEventArgs e) {
            this.richEditControl1.Options.Comments.Visibility = RichEditCommentVisibility.Hidden;
            edtHighlightCommentedRange.IsEnabled = true;
        }

        private void edtHighlightCommentedRange_Checked(object sender, RoutedEventArgs e) {
            this.richEditControl1.Options.Comments.HighlightCommentedRange = true;
        }

        private void edtHighlightCommentedRange_Unchecked(object sender, RoutedEventArgs e) {
            this.richEditControl1.Options.Comments.HighlightCommentedRange = false;
        }

        private void colorEdit1_ColorChanged(object sender, RoutedEventArgs e) {
            UpdateCommentColor();
            edtResetColor.IsChecked = false;
        }

        private void edtResetColor_Checked(object sender, RoutedEventArgs e) {
            this.richEditControl1.Options.Comments.ResetColor();
        }

        private void edtResetColor_Unchecked(object sender, RoutedEventArgs e) {
            UpdateCommentColor();
        }

    }
}
