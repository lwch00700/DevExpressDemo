using System;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpellChecker;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.SpellChecker;

namespace RichEditDemo {
    public partial class FloatingObjects : RichEditDemoModule {
        public FloatingObjects() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("FloatingObjects.docx", richEdit);
        }
    }
}
