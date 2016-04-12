using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using DevExpress.Xpf.Carousel;
namespace CarouselDemo {
    public partial class VideoCatalog : CarouselDemoModule {
        public static readonly DependencyProperty MediaPathProperty =
            DependencyProperty.RegisterAttached(
            "MediaPath",
            typeof(string),
            typeof(VideoCatalog),
            new FrameworkPropertyMetadata("test"), new ValidateValueCallback(ValidateMediaPathProperty));
        public static bool ValidateMediaPathProperty(object value) {
            return true;
        }
        public static string GetMediaPath(ImageControl ic) {
            return (string)ic.GetValue(MediaPathProperty);
        }
        public static void SetMediaPath(ImageControl ic, string value) {
            ic.SetValue(MediaPathProperty, value);
        }
        public VideoCatalog() {
            InitializeComponent();
            AddItems(@"/CarouselDemo;component/Data/Images/VideoPhotos", ItemType.BinaryImage, carousel);
        }
        protected override void AddItems(string path, ItemType it, CarouselPanel carousel) {
            var items = CreateItems(path, it);
            string[] links = new string[]{
"http://tv.devexpress.com//Content//ASPxGridView//ASPxBinaryImageIntro//ASPxBinaryImageIntro.flv",
"http://tv.devexpress.com//Content//XtraBars/Lesson4//XtraBars04.flv",
"http://tv.devexpress.com//Content//AgLayoutControl//AgLayoutPromo1//AgLayoutPromo1.flv",
"http://tv.devexpress.com//Content//SkinningLibrary//SkinsPromo1//SkinsPromo1.flv",
"http://tv.devexpress.com//Content//ASPxperience//Lesson1//ASPxperience01.flv",
"http://tv.devexpress.com//Content//Mehul//AGVFilterControl//AGVFilterControl.flv",
"http://tv.devexpress.com//Content//ASPxGridView/AGVInitNewRows//AGVInitNewRows.flv",
"http://tv.devexpress.com//Content//XtraSpellChecker//ASPxSpellChecker01//ASPxSpellChecker01.flv",
"http://tv.devexpress.com//Content//ASPxGridView//AGVBindArrayList//AGVBindArrayList.flv",
"http://tv.devexpress.com//Content//Mehul//FreeSilverLight//FreeSilverLightQuestions.flv"
            };
            ImageControl imageControl;
            int i = 0;
            foreach(var item in items) {
                var border = new Border();
                imageControl = new ImageControl();
                imageControl.Width = 70;
                imageControl.Height = 70;
                imageControl.Source = ((Image)item).Source;
                imageControl.Template = (ControlTemplate)TryFindResource("imageControlTemplate");
                SetMediaPath(imageControl, links[i++]);
                carousel.Children.Add(imageControl);
            }
        }
        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e) {
            splashImage.Visibility = Visibility.Collapsed;
        }

        private void MediaElement_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e) {
            splashImage.Visibility = Visibility.Visible;
        }

        private void splashImage_MouseDown(object sender, MouseButtonEventArgs e) {
            mediaElement.Play();
            splashImage.Visibility = Visibility.Collapsed;
        }
        private void carousel_MouseDown(object sender, MouseButtonEventArgs e) {
            mediaElement.Stop();
            mediaElement.Close();
            splashImage.Visibility = Visibility.Visible;
        }
    }
}
