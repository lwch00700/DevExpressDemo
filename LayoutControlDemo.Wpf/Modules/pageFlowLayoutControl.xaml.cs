using System.Windows;

namespace DevExpress.Xpf.LayoutControlDemo {
    public partial class pageFlowLayoutControl : LayoutControlDemoModule {
        public pageFlowLayoutControl() {
            InitializeComponent();
        }

        void AllowLayerSizingCheckBox_Checked(object sender, RoutedEventArgs e) {
            layoutItems.ShowLayerSeparators = true;
        }
        void StretchContentCheckBox_Checked(object sender, RoutedEventArgs e) {
            foreach (var child in layoutItems.GetLogicalChildren(false))
                child.Width = double.NaN;
        }
    }
}
