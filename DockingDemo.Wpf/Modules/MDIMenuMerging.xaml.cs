using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public partial class MDIMenuMerging : DockingDemoModule {
        public MDIMenuMerging() {
            InitializeComponent();
            dockManager.Merge += dockManager_Merge;
            dockManager.UnMerge += dockManager_UnMerge;
            dockManager.ActiveMDIItem = mdiContainer;
        }
        void dockManager_Merge(object sender, BarMergeEventArgs e) {
            Bar childBar = e.ChildBarManager.Bars["childBar"];
            Bar commonBar = e.BarManager.Bars["Common"];
            if(commonBar != null)
                commonBar.Merge(childBar);
        }
        void dockManager_UnMerge(object sender, BarMergeEventArgs e) {
            Bar commonBar = e.BarManager.Bars["Common"];
            if(commonBar != null)
                commonBar.UnMerge();
        }
    }
}
