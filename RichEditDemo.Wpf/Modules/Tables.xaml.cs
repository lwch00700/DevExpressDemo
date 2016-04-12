using System;

namespace RichEditDemo {
    public partial class Tables : RichEditDemoModule {
        public Tables() {
            InitializeComponent();
            OpenXmlLoadHelper.Load("ActiveCustomers.docx", richEdit);
        }
    }
}
