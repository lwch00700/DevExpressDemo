using System;

namespace RichEditDemo {
    public partial class LoadSaveRtf : RichEditDemoModule {
        public LoadSaveRtf() {
            InitializeComponent();
            RtfLoadHelper.Load("CharacterFormatting.rtf", richEdit);
        }
    }
}
