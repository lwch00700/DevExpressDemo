using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Globalization;
using DevExpress.Xpf.Editors;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraRichEdit;
using DevExpress.Xpf.SpellChecker;

namespace SpellCheckerDemo {
    public partial class HunspellDictionaries : SpellCheckerDemoModule {
        public HunspellDictionaries() {
            InitializeComponent();

            DocumentLoadHelper.Load("HunspellDictionaries.docx", DocumentFormat.OpenXml, richEdit);
        }

        protected override SpellChecker CreateSpellCheckerControl() {
            SpellChecker result = new SpellChecker();
            SpellCheckerHelper.RegisterHunspellDictionaries(result);
            result.Culture = CultureInfo.InvariantCulture;
            return result;
        }

        void richEdit_Loaded(object sender, RoutedEventArgs e) {
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
