using System;

namespace RichEditDemo {
    public partial class BulletsAndNumbering : RichEditDemoModule {
        public BulletsAndNumbering() {
            InitializeComponent();
            RtfLoadHelper.Load("BulletsAndNumbering.rtf", richEdit);
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
