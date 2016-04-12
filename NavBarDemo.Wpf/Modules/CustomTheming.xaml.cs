using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
using DevExpress.Xpf.NavBar;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.Utils;

namespace NavBarDemo {
    public partial class CustomTheming : NavBarDemoModule {
        public CustomTheming() {
            InitializeComponent();
        }
    }
    public class SlideNavBarGroup : NavBarGroup {
        public static readonly DependencyProperty ItemVisibleIndexProperty;
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty RadioButtonStyleProperty;

        static SlideNavBarGroup() {
            BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(SlideNavBarGroup), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
            ItemVisibleIndexProperty = DependencyProperty.Register("ItemVisibleIndex", typeof(int), typeof(SlideNavBarGroup), new PropertyMetadata(0));
            RadioButtonStyleProperty = DependencyProperty.Register("RadioButtonStyle", typeof(Style), typeof(SlideNavBarGroup), new PropertyMetadata(null));
        }
        public Brush Background {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public int ItemVisibleIndex {
            get { return (int)GetValue(ItemVisibleIndexProperty); }
            set { SetValue(ItemVisibleIndexProperty, value); }
        }
        public Style RadioButtonStyle {
            get { return (Style)GetValue(RadioButtonStyleProperty); }
            set { SetValue(RadioButtonStyleProperty, value); }
        }
    }
    public class PageSmoothScroller : NavBarSmoothScroller {
        #region static
        public static readonly DependencyProperty ItemVisibleIndexProperty;

        static PageSmoothScroller() {
            ItemVisibleIndexProperty = DependencyProperty.Register("ItemVisibleIndex", typeof(int), typeof(PageSmoothScroller), new PropertyMetadata(2, new PropertyChangedCallback(OnCurrentVisibleItemPropertyChanged)));
        }
        protected static void OnCurrentVisibleItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PageSmoothScroller)d).OnCurrentVisibleItemChanged();
        }
        #endregion
        public PageSmoothScroller() {
            Loaded += new RoutedEventHandler(OnLoaded);
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            SetBinding(ItemVisibleIndexProperty, new Binding("ItemVisibleIndex") { Mode = BindingMode.TwoWay });
        }
        public int ItemVisibleIndex {
            get { return (int)GetValue(ItemVisibleIndexProperty); }
            set { SetValue(ItemVisibleIndexProperty, value); }
        }
        protected internal double NextChildOffset {
            get {
                double height = 0;
                StackPanel itemsPanel = (StackPanel)GetItemsPanel(this);
                if(itemsPanel == null)
                    return height;
                if(ItemVisibleIndex == itemsPanel.Children.Count - 1)
                    return ContentHeight - ((FrameworkElement)itemsPanel.Children[ItemVisibleIndex]).ActualHeight + 15;
                for(int i = 0; i < ItemVisibleIndex; i++) {
                    height += ((FrameworkElement)itemsPanel.Children[i]).ActualHeight;
                }
                return height;
            }
        }
        protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e) { }
        protected override void PerformScrollDownCommand() {
            int visibleIndex = ItemVisibleIndex + 1;
            if(ActualHeight * visibleIndex > ContentHeight)
                return;
            ItemVisibleIndex += 1;
        }
        protected override void PerformScrollUpCommand() {
            int visibleIndex = ItemVisibleIndex - 1;
            if(visibleIndex < 0)
                return;
            ItemVisibleIndex -= 1;
        }
        protected internal void OnCurrentVisibleItemChanged() {
            StartAnimation();
        }
        FrameworkElement GetItemsPanel(DependencyObject reference) {
            int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
            if(childrenCount > 0) {
                FrameworkElement child = (FrameworkElement)VisualTreeHelper.GetChild(reference, 0);
                if(child.Name == "itemsPanel")
                    return child;
                else
                    return GetItemsPanel(child);
            }
            return null;
        }
        void StartAnimation() {
            Storyboard storyBoard = new Storyboard();
            Storyboard.SetTargetProperty(storyBoard, new PropertyPath(ChildOffsetProperty));
            DoubleAnimation scrollAnimation = new DoubleAnimation();

            scrollAnimation.From = ChildOffset;
            scrollAnimation.To = NextChildOffset;
            scrollAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(500));

            DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 200)), Value = 0.5 });
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 500)), Value = 0.5 });
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 0, 600)), Value = 1 });
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(UIElement.OpacityProperty));
            Storyboard.SetTarget(opacityAnimation, Child);

            storyBoard.Children.Add(scrollAnimation);
            storyBoard.Children.Add(opacityAnimation);
            storyBoard.Begin(this, true);
        }
    }
    public class RadioButtonsPanel : StackPanel {
        public static readonly DependencyProperty RadioButtonStyleProperty;
        public static readonly DependencyProperty SelectedItemIndexProperty;

        static RadioButtonsPanel() {
            RadioButtonStyleProperty = DependencyProperty.Register("RadioButtonStyle", typeof(Style), typeof(RadioButtonsPanel), new PropertyMetadata(null));
            SelectedItemIndexProperty = DependencyProperty.Register("SelectedItemIndex", typeof(int), typeof(RadioButtonsPanel), new PropertyMetadata(0, new PropertyChangedCallback(OnSelectedItemIndexPropertyChanged)));
        }

        public RadioButtonsPanel() {
            Loaded += new RoutedEventHandler(OnLoaded);
        }

        public Style RadioButtonStyle {
            get { return (Style)GetValue(RadioButtonStyleProperty); }
            set { SetValue(RadioButtonStyleProperty, value); }
        }
        public int SelectedItemIndex {
            get { return (int)GetValue(SelectedItemIndexProperty); }
            set { SetValue(SelectedItemIndexProperty, value); }
        }

        public static void OnSelectedItemIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((RadioButtonsPanel)d).UpdateSelectedItemIndex();
        }
        void GenerateContent() {
            if(DataContext == null || Children.Count == ((NavBarGroup)DataContext).Items.Count)
                return;
            int itemsCount = ((NavBarGroup)DataContext).Items.Count;
            for(int i = itemsCount - 1; i >= 0; i--) {
                RadioButton rb = new RadioButton() { Style = RadioButtonStyle, Content = i };
                if(i == 0)
                    rb.Margin = new Thickness(2, 2, 4, 2);
                if(i == itemsCount - 1)
                    rb.Margin = new Thickness(4, 2, 2, 2);
                rb.Click += new RoutedEventHandler(OnRadioButtonClick);
                Children.Add(rb);
            }
        }
        void OnInitialized() {
            SetBinding(SelectedItemIndexProperty, new Binding("ItemVisibleIndex"));
            GenerateContent();
            UpdateSelectedItemIndex();
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            OnInitialized();
        }
        void OnRadioButtonClick(object sender, RoutedEventArgs e) {
            RadioButton rb = (RadioButton)sender;
            if(rb.IsChecked.HasValue && rb.IsChecked.Value)
                ((SlideNavBarGroup)DataContext).ItemVisibleIndex = Int32.Parse((rb).Content.ToString());

        }
        void UpdateSelectedItemIndex() {
            if(SelectedItemIndex >= Children.Count)
                return;
            ((RadioButton)Children[Children.Count - SelectedItemIndex - 1]).IsChecked = true;
        }
    }

    public class ItemContent : BindableBase {
        public ItemContent() {
            Description = string.Empty;
            Header = string.Empty;
        }
        public string Description {
            get { return GetProperty(() => Description); }
            set { SetProperty(() => Description, value); }
        }
        public string Header {
            get { return GetProperty(() => Header); }
            set { SetProperty(() => Header, value); }
        }
        public ImageSource ImageSource {
            get { return GetProperty(() => ImageSource); }
            set { SetProperty(() => ImageSource, value); }
        }
        public double ImageHeight { get; set; }
    }

    public class HeaderContent : BindableBase {
        public ImageSource ImageSource {
            get { return GetProperty(() => ImageSource); }
            set { SetProperty(() => ImageSource, value); }
        }
    }
}
