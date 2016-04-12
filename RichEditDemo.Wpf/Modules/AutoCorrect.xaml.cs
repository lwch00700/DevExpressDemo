using System;
using System.Globalization;
using System.Windows;
using DevExpress.Xpf.Editors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraRichEdit.Utils;
using DevExpress.Office.NumberConverters;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Utils;
using DevExpress.Office.Utils;

namespace RichEditDemo {
    public partial class AutoCorrect : RichEditDemoModule {
        SpellChecker spellChecker;

        public AutoCorrect() {
            InitializeComponent();
            this.spellChecker = SpellChecking.InitializeSpellChecker();
            OpenXmlLoadHelper.Load("AutoCorrect.docx", richEdit);
        }

        void richEdit_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            richEdit.SpellChecker = spellChecker;

            richEdit.Options.AutoCorrect.CorrectTwoInitialCapitals = true;
            richEdit.Options.AutoCorrect.UseSpellCheckerSuggestions = true;

            IAutoCorrectService service = richEdit.GetService<IAutoCorrectService>();
            if (service != null) {
                AutoCorrectReplaceInfoCollection replaceTable = new AutoCorrectReplaceInfoCollection();
                replaceTable.Add("(C)", "©");
                replaceTable.Add(new AutoCorrectReplaceInfo(":)", OfficeImage.CreateImage(GetType().Assembly.GetManifestResourceStream("RichEditDemo.smile.png"))));
                replaceTable.Add("pctus", "Please do not hesitate to contact us again in case of any further questions.");
                replaceTable.Add("wnwd", "well-nourished, well-developed");
                service.SetReplaceTable(replaceTable);
            }
        }

        void edtReplaceAsYouType_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.AutoCorrect.ReplaceTextAsYouType = edtReplaceAsYouType.IsChecked.Value;
        }
        void edtDetectUrls_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.AutoCorrect.DetectUrls = edtDetectUrls.IsChecked.Value;
        }
        void edtCorrectTwoInitialCapitals_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.AutoCorrect.CorrectTwoInitialCapitals = edtCorrectTwoInitialCapitals.IsChecked.Value;
        }
        void edtUseSpellCheckerSuggestions_CheckedChanged(object sender, RoutedEventArgs e) {
            richEdit.Options.AutoCorrect.UseSpellCheckerSuggestions = edtUseSpellCheckerSuggestions.IsChecked.Value;
        }
        void richEdit_AutoCorrect(object sender, AutoCorrectEventArgs e) {
            AutoCorrectInfo info = e.AutoCorrectInfo;
            e.AutoCorrectInfo = null;

            if (!edtCustomAutoCorrect.IsChecked.Value)
                return;

            if (info.Text.Length <= 0 || info.Text[0] != '%')
                return;
            for (; ; ) {
                if (!info.DecrementStartPosition())
                    return;

                if (IsSeparator(info.Text[0]))
                    return;

                if (info.Text[0] == '%') {
                    string replaceString = CalculateFunction(info.Text);
                    if (!String.IsNullOrEmpty(replaceString)) {
                        info.ReplaceWith = replaceString;
                        e.AutoCorrectInfo = info;
                    }
                    return;
                }
            }
        }

        string CalculateFunction(string name) {
            name = name.ToLower();

            if (name.Length > 2 && name[0] == '%' && name.EndsWith("%")) {
                int value;
                if (Int32.TryParse(name.Substring(1, name.Length - 2), out value)) {
                    OrdinalBasedNumberConverter converter = OrdinalBasedNumberConverter.CreateConverter(NumberingFormat.CardinalText, LanguageId.English);
                    return converter.ConvertNumber(value);
                }
            }

            switch (name) {
                case "%date%":
                    return DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
                case "%time%":
                    return DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
                case "%bye%":
                    return "Yours sincerely,\r\nDavid B. Smith";
                default:
                    return String.Empty;
            }
        }
        bool IsSeparator(char ch) {
            return ch != '%' && (ch == '\r' || ch == '\n' || Char.IsPunctuation(ch) || Char.IsSeparator(ch) || Char.IsWhiteSpace(ch));
        }
    }
}
