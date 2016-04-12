using DevExpress.Xpf.Bars;
using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
    public partial class PanelOptions : DockingDemoModule {
        public PanelOptions() {
            InitializeComponent();
        }
        protected override void RaiseModuleAppear() {
            base.RaiseModuleAppear();
            InvalidateVisual();
        }
        void bHome_ItemClick(object sender, ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void bAbout_ItemClick(object sender, ItemClickEventArgs e) {
            ShowAbout();
        }
    }
}
