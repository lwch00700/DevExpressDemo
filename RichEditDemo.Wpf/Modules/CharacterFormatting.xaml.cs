using System;

namespace RichEditDemo {
    public partial class CharacterFormatting : RichEditDemoModule {
        public CharacterFormatting() {
            InitializeComponent();
            RtfLoadHelper.Load("CharacterFormatting.rtf", richEdit);
            ribbonControl1.SelectedPage = pageHome;
        }
    }
}
