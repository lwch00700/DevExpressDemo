using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraSpellChecker;

namespace SpellCheckerDemo {
    public partial class CheckAsYouType : SpellCheckerDemoModule {
        List<FrameworkElement> elements;
        CultureInfo[] cultures;

        public CheckAsYouType() {
            this.cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            Array.Sort<CultureInfo>(this.cultures, new Comparison<CultureInfo>((c1, c2) => { return c1.Name.CompareTo(c2.Name); }));
            InitializeComponent();
            this.elements = new List<FrameworkElement>() { textBox, richTextBox, textEdit };
        }

        protected override List<FrameworkElement> CheckingElements { get { return elements; } }
        public static readonly DependencyProperty ActiveEditorProperty = DependencyProperty.Register("ActiveEditor", typeof(Control), typeof(CheckAsYouType), new PropertyMetadata(new PropertyChangedCallback(OnActiveEditorChanged)));
        static void OnActiveEditorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            CheckAsYouType module = (CheckAsYouType)d;
            Control newControl = (Control)e.NewValue;
            module.GroupSettingsHeader = String.Format("{0} Spelling Settings", newControl.GetType().Name);
        }
        public Control ActiveEditor {
            get { return (Control)GetValue(ActiveEditorProperty); }
            set { SetValue(ActiveEditorProperty, value); }
        }
        public static readonly DependencyProperty GroupSettingsHederProperty = DependencyProperty.Register("GroupSettingsHeader", typeof(string), typeof(CheckAsYouType));
        public string GroupSettingsHeader {
            get { return (string)GetValue(GroupSettingsHederProperty); }
            set { SetValue(GroupSettingsHederProperty, value); }
        }
        public string[] UnderlineStyles { get { return Enum.GetNames(typeof(UnderlineStyle)); } }
        public CultureInfo[] Cultures { get { return cultures; } }

        protected override SpellChecker CreateSpellCheckerControl() {
            SpellChecker result = new SpellChecker();
            SpellCheckerCustomDictionary customDictionary = new SpellCheckerCustomDictionary(String.Empty, CultureInfo.InvariantCulture);
            result.Dictionaries.Add(customDictionary);
            result.SpellCheckMode = SpellCheckMode.AsYouType;
            return result;
        }
        void ClearSettings(FrameworkElement element) {
            element.ClearValue(SpellingSettings.CheckAsYouTypeProperty);
            element.ClearValue(SpellingSettings.ShowSpellCheckMenuProperty);
            element.ClearValue(SpellingSettings.UnderlineColorProperty);
            element.ClearValue(SpellingSettings.UnderlineStyleProperty);
            element.ClearValue(SpellingSettings.IgnoreEmailsProperty);
            element.ClearValue(SpellingSettings.IgnoreMixedCaseWordsProperty);
            element.ClearValue(SpellingSettings.IgnoreRepeatedWordsProperty);
            element.ClearValue(SpellingSettings.IgnoreUpperCaseWordsProperty);
            element.ClearValue(SpellingSettings.IgnoreUriProperty);
            element.ClearValue(SpellingSettings.IgnoreWordsWithNumbersProperty);
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            if (ActiveEditor != null)
                ClearSettings(ActiveEditor);
        }
        void element_GotFocus(object sender, RoutedEventArgs e) {
            ActiveEditor = sender as Control;
        }
        void CheckAsYouTypeModule_Loaded(object sender, RoutedEventArgs e) {
            ActiveEditor = textBox;
            foreach (FrameworkElement element in CheckingElements) {
                element.GotFocus += element_GotFocus;
            }
        }
    }
    public class UnderlineStyleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            UnderlineStyle result;
            Enum.TryParse<UnderlineStyle>((string)value, out result);
            return result;
        }
    }
    public class DefaultBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            DefaultBoolean dbValue = (DefaultBoolean)value;
            return dbValue.Equals(DefaultBoolean.True) ? true : false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            bool boolValue = (bool)value;
            return boolValue ? DefaultBoolean.True : DefaultBoolean.False;
        }
    }
    public class OpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Control control = (Control)value;
            if (control == null)
                return 1.0;
            return control.Name.Equals(parameter) ? 1.0 : 0.5;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
