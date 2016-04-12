using System;

namespace RichEditDemo {
    public partial class HeadersAndFooters : RichEditDemoModule {
        public HeadersAndFooters() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("HeadersFooters.docx", richEdit);
            ribbonControl1.SelectedPage = pageInsert;
        }
    }
}
