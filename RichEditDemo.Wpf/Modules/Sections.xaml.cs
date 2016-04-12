using System;

namespace RichEditDemo {
    public partial class Sections : RichEditDemoModule {
        public Sections() {
            InitializeComponent();
            RtfLoadHelper.Load("Sections.rtf", richEdit);
            ribbonControl1.SelectedPage = pagePageLayout;
        }
    }
}
