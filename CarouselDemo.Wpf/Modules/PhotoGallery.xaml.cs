using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Carousel;
using System.Windows.Data;
using System.Windows.Markup;

namespace CarouselDemo {
    public partial class PhotoGallery : CarouselDemoModule {
        public PhotoGallery() {
            InitializeComponent();
            AddItems(@"/CarouselDemo;component/Data/Images/Photos", ItemType.BinaryImage, carousel);
        }
        protected override void AddItems(string path, ItemType it, CarouselPanel carousel) {
            var items = CreateItems(path, it);
            foreach (var item in items) {
                ContentControl control = new ContentControl();
                control.Template = carousel.Resources["itemTemplate"] as ControlTemplate;
                control.Style = carousel.Resources["itemStyle"] as Style;
                control.DataContext = ((Image)item).Source;
                carousel.Children.Add(control);
            }
        }
    }
}
