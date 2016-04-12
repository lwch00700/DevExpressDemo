using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public partial class Window3 : UserControl {
        public Window3() {
            InitializeComponent();
        }
        void bClose_ItemClick(object sender, ItemClickEventArgs ea) {
            DockLayoutManager manager = DockLayoutManager.GetDockLayoutManager(sender as DependencyObject);
            BaseLayoutItem item = DockLayoutManager.GetLayoutItem(sender as DependencyObject);
            manager.DockController.Close(item);
        }
    }
}
