using System;
using System.Collections.Generic;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils.Themes;
using DevExpress.Xpf.SpellChecker;
using DevExpress.XtraSpellChecker.Native;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit.SpellChecker;
using System.Globalization;
using DevExpress.XtraSpellChecker;

namespace SpellCheckerDemo {
    public class SpellCheckerDemoModule : DemoModule {
        public static readonly DependencyProperty SpellCheckerProperty;

        static SpellCheckerDemoModule() {
            RegisterRichEditControlController();
            RegisterRichEditControlUndoManager();
            SpellCheckerProperty = DependencyProperty.Register("SpellChecker", typeof(SpellChecker), typeof(SpellCheckerDemoModule), new PropertyMetadata(null));
        }
        static void RegisterRichEditControlController() {
            SpellCheckTextControllersManager.Default.RegisterClass(typeof(RichEditControl), typeof(RichEditSpellCheckController));
        }
        static void RegisterRichEditControlUndoManager() {
            UndoControllerRepository.Default.Register(typeof(RichEditControl), typeof(RichEditUndoController));
        }

        public SpellCheckerDemoModule() {
            SpellChecker spellChecker = CreateSpellCheckerControl();
            if (spellChecker != null)
                SpellChecker = spellChecker;
            else
                SpellChecker = CreateDefaultSpellCheckerControl();
            DataContext = SpellChecker;
        }
        public SpellChecker SpellChecker {
            get { return (SpellChecker)GetValue(SpellCheckerProperty); }
            private set { SetValue(SpellCheckerProperty, value); }
        }
        protected virtual List<FrameworkElement> CheckingElements { get { return null; } }
        protected override string XamlSuffix { get { return ".xaml"; } }
        protected string DefaultXamlSuffix { get { return base.XamlSuffix; } }

        void CheckingElement_Loaded(object sender, RoutedEventArgs e) {
            ApplySpellCheckMode(true);
        }
        protected override void RaiseIsPopupContentInvisibleChanged(DependencyPropertyChangedEventArgs e) {
            base.RaiseIsPopupContentInvisibleChanged(e);
            if (CheckingElements == null)
                return;
            foreach (FrameworkElement element in CheckingElements) {
                if ((bool)e.NewValue)
                    element.Visibility = System.Windows.Visibility.Hidden;
                else
                    element.Visibility = System.Windows.Visibility.Visible;
            }
        }
        protected void ApplySpellCheckMode(bool isAsYouType) {
            if (isAsYouType)
                SpellChecker.SpellCheckMode = SpellCheckMode.AsYouType;
            else
                SpellChecker.SpellCheckMode = SpellCheckMode.OnDemand;
        }
        protected virtual SpellChecker CreateSpellCheckerControl() {
            return null;
        }
        private SpellChecker CreateDefaultSpellCheckerControl() {
            SpellChecker result = new SpellChecker();
            SpellCheckerHelper.RegisterDefaultDictionaries(result);
            result.Culture = new CultureInfo("en-US");
            return result;
        }
        protected override object GetModuleDataContext() {
            return SpellChecker;
        }
    }
}
