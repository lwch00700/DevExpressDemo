using System;

namespace RichEditDemo {
    public partial class LoadSaveDoc : RichEditDemoModule {
        public LoadSaveDoc() {
            InitializeComponent();
            DocLoadHelper.Load("DocLoading.doc", richEdit);
        }
    }
}
