using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
    public partial class IDEDockLayout : DockingDemoModule {
        public IDEDockLayout() {
            InitializeComponent();
        }
        void bHome_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            NavigateHomePage();
        }
        void bAbout_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            ShowAbout();
        }
    }
}
