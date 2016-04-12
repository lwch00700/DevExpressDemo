using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DevExpress.Xpf.LayoutControlDemo {
    public partial class pageProducts : LayoutControlDemoModule {
        private int _SelectedPackIndex = -1;

        public pageProducts() {
            InitializeComponent();
            var DisabledProductBrush = (Brush)Resources["DisabledProductBrush"];
            RegisterName("DisabledProductBrush", DisabledProductBrush);
        }

        protected void UpdateProducts() {
            var DisabledProductBrushAnimation = (Storyboard)Resources["DisabledProductBrushAnimation"];
            DisabledProductBrushAnimation.Stop();

            string packName = "Pack" + SelectedPackIndex.ToString();
            var EnabledProductBrush = Resources["EnabledProductBrush"] as Brush;
            var DisabledProductBrush = Resources["DisabledProductBrush"] as Brush;

            foreach (FrameworkElement element in layoutProducts.GetVisibleChildren()) {
                var textBlock = element as TextBlock;
                if (textBlock == null || textBlock.Tag == null)
                    continue;
                bool isEnabled = SelectedPackIndex == -1 || ((string)textBlock.Tag).Contains(packName);
                textBlock.Foreground = isEnabled ? EnabledProductBrush : DisabledProductBrush;
            }

            DisabledProductBrushAnimation.Begin();
        }

        protected int SelectedPackIndex {
            get { return _SelectedPackIndex; }
            set {
                if(_SelectedPackIndex != value) {
                    _SelectedPackIndex = value;
                    UpdateProducts();
                }
            }
        }

        void PackTextBlock_MouseEnter(object sender, MouseEventArgs e) {
            var textBlock = (FrameworkElement)sender;
            SelectedPackIndex = int.Parse(textBlock.Name[textBlock.Name.Length - 1].ToString());
        }
        void PackTextBlock_MouseLeave(object sender, MouseEventArgs e) {
            SelectedPackIndex = -1;
        }
    }
}
