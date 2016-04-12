using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public partial class Window1 : UserControl {
        public Window1() {
            InitializeComponent();
        }
        void bClose_ItemClick(object sender, ItemClickEventArgs ea) {
            DockLayoutManager manager = DockLayoutManager.GetDockLayoutManager(sender as DependencyObject);
            BaseLayoutItem item = DockLayoutManager.GetLayoutItem(sender as DependencyObject);
            manager.DockController.Close(item);
        }
    }
}
