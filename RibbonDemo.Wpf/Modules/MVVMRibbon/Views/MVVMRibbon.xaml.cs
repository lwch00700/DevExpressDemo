using DevExpress.Xpf.DemoBase;

namespace RibbonDemo {
    [CodeFiles(@"Modules/MVVMRibbon/Views/MVVMRibbon(.SL).xaml;
                 Modules/MVVMRibbon/Views/MVVMRibbon.xaml.(cs);
                 Modules/MVVMRibbon/ViewModels/MVVMRibbonCommands.(cs);
                 Modules/MVVMRibbon/ViewModels/MVVMRibbonViewModel.(cs)")]
    public partial class MVVMRibbon : RibbonDemoModule {
        public MVVMRibbon() {
            InitializeComponent();
        }
    }
}
