using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public partial class Window2 : UserControl {
        MatrixTransform transform = new MatrixTransform();
        public Window2() {
            InitializeComponent();
            image.LayoutTransform = transform;
        }
        void bClose_ItemClick(object sender, ItemClickEventArgs ea) {
            DockLayoutManager manager = DockLayoutManager.GetDockLayoutManager(sender as DependencyObject);
            BaseLayoutItem item = DockLayoutManager.GetLayoutItem(sender as DependencyObject);
            manager.DockController.Close(item);
        }
        void biFlipHorz_ItemClick(object sender, ItemClickEventArgs e) {
            transform.Matrix = Matrix.Multiply(transform.Matrix, new Matrix(-1, 0, 0, 1, 0, 0));
        }
        void biFlipVert_ItemClick(object sender, ItemClickEventArgs e) {
            transform.Matrix = Matrix.Multiply(transform.Matrix, new Matrix(1, 0, 0, -1, 0, 0));
        }
        void biRotateCW_ItemClick(object sender, ItemClickEventArgs e) {
            transform.Matrix = Matrix.Multiply(transform.Matrix, new Matrix(0, 1, -1, 0, 0, 0));
        }
        void biRotateCCW_ItemClick(object sender, ItemClickEventArgs e) {
            transform.Matrix = Matrix.Multiply(transform.Matrix, new Matrix(0, -1, 1, 0, 0, 0));
        }
    }
}
