using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
    [CodeFile("Resources/MVVMDockLayoutResources(.SL).xaml")]
    [CodeFile("Resources/VS2010DockingResources(.SL).xaml")]
    [CodeFile("ViewModels/BarTemplateSelector.(cs)")]
    [CodeFile("ViewModels/MVVMDockLayoutViewModel.(cs)")]
    [CodeFile("ViewModels/VS2010DockingViewModel.(cs)")]
    public partial class VS2010Docking : DockingDemoModule {
        public VS2010Docking() {
            InitializeComponent();
        }
    }
}
