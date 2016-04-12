using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Globalization;
using DevExpress.Xpf.Editors;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraRichEdit;

namespace SpellCheckerDemo {
    public partial class DXRichEdit : SpellCheckerDemoModule {
        public DXRichEdit() {
            InitializeComponent();
            DocumentLoadHelper.Load("SpellChecker.rtf", DocumentFormat.Rtf, richEdit);
        }

        void richEdit_Loaded(object sender, RoutedEventArgs e) {
            SpellChecker.SpellingFormType = SpellingFormType.Word;
            richEdit.SpellChecker = SpellChecker;
            ApplySpellCheckMode();
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            SpellChecker.Check(richEdit);
        }
        void edtCheckAsYouType_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ApplySpellCheckMode();
        }
        void ApplySpellCheckMode() {
            if (edtCheckAsYouType.IsChecked == true)
                SpellChecker.SpellCheckMode = SpellCheckMode.AsYouType;
            else
                SpellChecker.SpellCheckMode = SpellCheckMode.OnDemand;
        }
    }
}
