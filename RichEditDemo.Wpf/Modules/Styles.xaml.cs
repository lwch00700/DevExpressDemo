using System;

namespace RichEditDemo {
    public partial class Styles : RichEditDemoModule {
        public Styles() {
            InitializeComponent();
            RtfLoadHelper.Load("Styles.rtf", richEdit);
        }
    }
}
