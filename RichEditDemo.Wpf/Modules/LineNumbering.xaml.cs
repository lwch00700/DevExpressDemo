using System;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.SpellChecker;

namespace RichEditDemo {
    public partial class LineNumbering : RichEditDemoModule {
        public LineNumbering() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("LineNumbering.docx", richEdit);
        }

        private void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.Views.DraftView.Padding = new System.Windows.Forms.Padding(50, 4, 0, 0);
            richEdit.Views.SimpleView.Padding = new System.Windows.Forms.Padding(50, 4, 0, 0);
        }
    }
}
