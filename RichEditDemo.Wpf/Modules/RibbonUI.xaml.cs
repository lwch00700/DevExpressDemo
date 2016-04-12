using System;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.SpellChecker;

namespace RichEditDemo {
    public partial class RibbonUI : RichEditDemoModule {
        SpellChecker spellChecker;

        public RibbonUI() {
            InitializeComponent();
            this.spellChecker = SpellChecking.InitializeSpellChecker();
            OpenXmlLoadHelper.Load("MovieRentals.docx", richEdit);
        }

        void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.SpellChecker = spellChecker;
            spellChecker.SpellCheckMode = SpellCheckMode.AsYouType;
        }
    }
}
