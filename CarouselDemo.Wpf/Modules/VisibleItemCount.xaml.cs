using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using DevExpress.Xpf.Carousel;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Media;

namespace CarouselDemo {
    public partial class VisibleItemCount : CarouselDemoModule {
        public VisibleItemCount() {
            InitializeComponent();
            itemsControl.ItemsSource = CreateItems(@"/CarouselDemo;component/Data/Images/Icons", ItemType.BinaryImage);
        }
    }
}
