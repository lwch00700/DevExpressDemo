using System;

namespace RichEditDemo {
    public partial class Zooming : RichEditDemoModule {
        public Zooming() {
            InitializeComponent();
            RtfLoadHelper.Load("Zoom.rtf", richEdit);
            ribbonControl1.SelectedPage = pageView;
        }
    }
}
