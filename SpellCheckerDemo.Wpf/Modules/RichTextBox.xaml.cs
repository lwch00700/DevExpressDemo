using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Globalization;
using DevExpress.Xpf.Editors;
using DevExpress.XtraSpellChecker;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace SpellCheckerDemo {
    public partial class RichTextBox : SpellCheckerDemoModule {
        List<FrameworkElement> elements;

        public RichTextBox() {
            InitializeComponent();
            this.imgDiagram.Source = DemoUtils.GetBitmapImage("Diagram.png");
            this.elements = new List<FrameworkElement>() { richTextBox };
        }

        protected override string XamlSuffix { get { return DefaultXamlSuffix; } }
        protected override List<FrameworkElement> CheckingElements { get { return elements; } }

        void Button_Click(object sender, RoutedEventArgs e) {
            SpellChecker.Check(richTextBox);
        }
        private void editCheckAsYouType_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ApplySpellCheckMode((bool)e.NewValue);
        }
        private void richTextBox_Loaded(object sender, RoutedEventArgs e) {
            ApplySpellCheckMode(true);
        }
    }
}
