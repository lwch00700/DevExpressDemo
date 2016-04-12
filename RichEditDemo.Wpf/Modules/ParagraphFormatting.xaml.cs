using System;

namespace RichEditDemo {
    public partial class ParagraphFormatting : RichEditDemoModule {
        public ParagraphFormatting() {
            InitializeComponent();
            RtfLoadHelper.Load("ParagraphFormatting.rtf", richEdit);
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
