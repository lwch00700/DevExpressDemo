using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.DemoBase;
using System.Windows.Controls;
using DevExpress.Xpf.Carousel;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Data;
using System;
using DevExpress.Xpf.Editors;

namespace CarouselDemo {
    public partial class MovingPath : CarouselDemoModule {
        public MovingPath() {
            InitializeComponent();
            AddItems(@"/CarouselDemo;component/Data/Images/Logos", ItemType.BinaryImage, carousel);
            paddingTopSlider.EditValueChanged += TrackBarValueChanged; ;
            paddingBottomSlider.EditValueChanged += TrackBarValueChanged;
            paddingLeftSlider.EditValueChanged += TrackBarValueChanged;
            paddingRightSlider.EditValueChanged += TrackBarValueChanged;
            carousel.Loaded += new RoutedEventHandler(carousel_Loaded);
        }
        void carousel_Loaded(object sender, RoutedEventArgs e) {
            paddingLineVisualizer.InvalidateVisual();
        }
        void TrackBarValueChanged(object sender, EditValueChangedEventArgs e) {
            paddingLineVisualizer.InvalidateVisual();
        }
    }
    public class LineVisualizer : Grid {
        public static readonly DependencyProperty CarouselProperty;
        static LineVisualizer() {
            CarouselProperty = DependencyProperty.Register("Carousel", typeof(CarouselPanel), typeof(LineVisualizer), new FrameworkPropertyMetadata(null));
        }
        public CarouselPanel Carousel {
            get { return (CarouselPanel)GetValue(CarouselProperty); }
            set { SetValue(CarouselProperty, value); }
        }
        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            if (Carousel != null) {
                Pen pen = new Pen(Brushes.Gray, 1d);
                dc.DrawLine(pen, new Point(0, Carousel.PathPadding.Top / 100d * Carousel.ActualHeight), new Point(Carousel.ActualWidth, Carousel.PathPadding.Top / 100d * Carousel.ActualHeight));
                dc.DrawLine(pen, new Point(0, (1d - Carousel.PathPadding.Bottom / 100d) * Carousel.ActualHeight), new Point(Carousel.ActualWidth, (1d - Carousel.PathPadding.Bottom / 100d) * Carousel.ActualHeight));
                dc.DrawLine(pen, new Point(Carousel.PathPadding.Left / 100d * Carousel.ActualWidth, 0), new Point(Carousel.PathPadding.Left / 100d * Carousel.ActualWidth, ActualHeight));
                dc.DrawLine(pen, new Point((1d - Carousel.PathPadding.Right / 100d) * Carousel.ActualWidth, 0), new Point((1d - Carousel.PathPadding.Right / 100d) * Carousel.ActualWidth, ActualHeight));
            }
        }
    }
    public class PaddingConverter : MarkupExtension, IMultiValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        #region IMultiValueConverter Members
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return new Thickness((double)values[0], (double)values[1], (double)values[2], (double)values[3]);
        }
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
