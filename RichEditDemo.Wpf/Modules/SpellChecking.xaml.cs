using System;
using System.Globalization;
using System.IO;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.SpellChecker;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraSpellChecker;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.XtraRichEdit.SpellChecker;

namespace RichEditDemo {
    public partial class SpellChecking : RichEditDemoModule {
        SpellChecker spellChecker;

        public SpellChecking() {
            InitializeComponent();
            this.spellChecker = InitializeSpellChecker();
            RtfLoadHelper.Load("SpellChecker.rtf", richEdit);
        }
        void richEdit_Loaded(object sender, RoutedEventArgs e) {
            richEdit.SpellChecker = spellChecker;
            ApplySpellCheckMode();
        }
        void btnCheckSpelling_Click(object sender, RoutedEventArgs e) {
            spellChecker.Check(richEdit);
        }
        void edtCheckAsYouType_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ApplySpellCheckMode();
        }
        void ApplySpellCheckMode() {
            if (edtCheckAsYouType.IsChecked == true)
                spellChecker.SpellCheckMode = SpellCheckMode.AsYouType;
            else
                spellChecker.SpellCheckMode = SpellCheckMode.OnDemand;
        }

        #region SpellChecker initialization
        public static SpellChecker InitializeSpellChecker() {
            SpellChecker spellChecker = new SpellChecker();
            spellChecker.Culture = new CultureInfo("en-US");
            spellChecker.SpellingFormType = SpellingFormType.Word;
            RegisterDictionary(spellChecker, GetDefaultDictionary());
            RegisterDictionary(spellChecker, GetCustomDictionary(spellChecker));

            SpellCheckTextControllersManager.Default.RegisterClass(typeof(RichEditControl), typeof(RichEditSpellCheckController));
            UndoControllerRepository repository = new UndoControllerRepository();
            repository.Register(typeof(RichEditControl), typeof(RichEditUndoController));
            return spellChecker;
        }
        static SpellCheckerDictionaryBase GetDefaultDictionary() {
            SpellCheckerISpellDictionary dic = new SpellCheckerISpellDictionary();
            dic.LoadFromStream(DemoUtils.GetDataStream("american.xlg"),
                               DemoUtils.GetDataStream("english.aff"),
                               DemoUtils.GetDataStream("EnglishAlphabet.txt"));
            return dic;
        }
        static void RegisterDictionary(SpellChecker spellChecker, SpellCheckerDictionaryBase dic) {
            dic.Culture = spellChecker.Culture;
            spellChecker.Dictionaries.Add(dic);
        }
        static SpellCheckerDictionaryBase GetCustomDictionary(SpellChecker spellChecker) {
            return new SpellCheckerCustomDictionary("CustomEnglish.dic", spellChecker.Culture);
        }
        #endregion
    }
}
