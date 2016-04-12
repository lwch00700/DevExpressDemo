using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.XtraRichEdit.Commands;

namespace SpellCheckerDemo {
    public partial class TextEdit : SpellCheckerDemoModule {
        List<FrameworkElement> elements;

        public TextEdit() {
            InitializeComponent();
            this.elements = new List<FrameworkElement>() { textEdit };
        }

        protected override List<FrameworkElement> CheckingElements { get { return elements; } }

        private void Button_Click(object sender, RoutedEventArgs e) {
            SpellChecker.Check(textEdit);
        }
        private void editCheckAsYouType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e) {
            ApplySpellCheckMode((bool)e.NewValue);
        }
        private void textEdit_Loaded(object sender, RoutedEventArgs e) {
            ApplySpellCheckMode(true);
        }
    }
}
