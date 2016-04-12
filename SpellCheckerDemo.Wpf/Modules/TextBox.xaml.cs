using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Globalization;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.XtraRichEdit;
using System.IO;
using DevExpress.XtraSpellChecker;
using System.Collections.Generic;

namespace SpellCheckerDemo {
    public partial class TextBox : SpellCheckerDemoModule {
        List<FrameworkElement> elements;

        public TextBox() {
            InitializeComponent();
            this.elements = new List<FrameworkElement>() { tb };
        }

        protected override List<FrameworkElement> CheckingElements { get { return elements; } }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SpellChecker.Check(tb);
        }
        private void editCheckAsYouType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            ApplySpellCheckMode((bool)e.NewValue);
        }
        private void tb_Loaded(object sender, RoutedEventArgs e) {
            ApplySpellCheckMode(true);
        }
    }
}
