using System;

namespace RichEditDemo {
    public partial class Views : RichEditDemoModule {
        public Views() {
            InitializeComponent();
            RtfLoadHelper.Load("TextWithImages.rtf", richEdit);
            ribbonControl1.SelectedPage = pageView;
        }
    }
}
