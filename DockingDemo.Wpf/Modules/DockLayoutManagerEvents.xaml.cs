using DevExpress.Xpf.DemoBase;

namespace DockingDemo {
    [CodeFile("ViewModels/DockLayoutManagerEventsViewModel.(cs)")]
    [CodeFile("Services/DockLayoutManagerEventsService.(cs)")]
    public partial class DockLayoutManagerEvents : DockingDemoModule {
        public DockLayoutManagerEvents() {
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
