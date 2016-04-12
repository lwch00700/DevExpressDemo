using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using DevExpress.Xpf.Carousel;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;

namespace CarouselDemo {
    public partial class DXBook : CarouselDemoModule {
        public DXBook() {
            InitializeComponent();
            carouselItemsControl.ItemsSource = CreateItems(@"/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage);
        }
        void Grid_PreviewMouseDown(object sender, RoutedEventArgs e) {
            DependencyObject item = e.Source as DependencyObject;
            while(item != null) {
                CarouselPanel cp = VisualTreeHelper.GetParent(item) as CarouselPanel;
                if(cp != null && cp.Children.IndexOf(item as UIElement) == cp.Children.IndexOf(cp.ActiveItem))
                    cp.Move(-1);
                item = VisualTreeHelper.GetParent(item);
            }
        }
    }
}
