using System;

namespace RichEditDemo {
    public partial class FindAndReplace : RichEditDemoModule {
        public FindAndReplace() {
            InitializeComponent();
            RtfLoadHelper.Load("Search.rtf", richEdit);
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
