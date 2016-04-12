using DevExpress.Xpf.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace RibbonDemo {
    public class ThemedContentControl : ContentControl {
        #region static
        public static readonly DependencyProperty IsHoverStateProperty =
            DependencyProperty.Register("IsHoverState", typeof(bool), typeof(ThemedContentControl), new PropertyMetadata(false, new PropertyChangedCallback(OnHoverStatePropertyChanged)));
        public static readonly DependencyProperty LightThemeNormalForegroundProperty =
            DependencyProperty.Register("LightThemeNormalForeground", typeof(Brush), typeof(ThemedContentControl), new PropertyMetadata(null));
        public static readonly DependencyProperty DarkThemeNormalForegroundProperty =
            DependencyProperty.Register("DarkThemeNormalForeground", typeof(Brush), typeof(ThemedContentControl), new PropertyMetadata(null));
        public static readonly DependencyProperty LightThemeHoverForegroundProperty =
            DependencyProperty.Register("LightThemeHoverForeground", typeof(Brush), typeof(ThemedContentControl), new PropertyMetadata(null));
        public static readonly DependencyProperty DarkThemeHoverForegroundProperty =
            DependencyProperty.Register("DarkThemeHoverForeground", typeof(Brush), typeof(ThemedContentControl), new PropertyMetadata(null));
        protected static void OnHoverStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ThemedContentControl)d).UpdateForeground();
        }
        #endregion
        #region dep props
        public bool IsHoverState {
            get { return (bool)GetValue(IsHoverStateProperty); }
            set { SetValue(IsHoverStateProperty, value); }
        }
        public Brush LightThemeNormalForeground {
            get { return (Brush)GetValue(LightThemeNormalForegroundProperty); }
            set { SetValue(LightThemeNormalForegroundProperty, value); }
        }
        public Brush DarkThemeNormalForeground {
            get { return (Brush)GetValue(DarkThemeNormalForegroundProperty); }
            set { SetValue(DarkThemeNormalForegroundProperty, value); }
        }
        public Brush LightThemeHoverForeground {
            get { return (Brush)GetValue(LightThemeHoverForegroundProperty); }
            set { SetValue(LightThemeHoverForegroundProperty, value); }
        }
        public Brush DarkThemeHoverForeground {
            get { return (Brush)GetValue(DarkThemeHoverForegroundProperty); }
            set { SetValue(DarkThemeHoverForegroundProperty, value); }
        }
        #endregion
        #region props
        bool IsDarkTheme { get { return ThemeManager.ApplicationThemeName == Theme.MetropolisDarkName || ThemeManager.ApplicationThemeName == Theme.TouchlineDarkName; }}
        #endregion
        public ThemedContentControl() {
            DefaultStyleKey = typeof(ThemedContentControl);
            Loaded += OnLoaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            UpdateForeground();
        }
        private void UpdateForeground() {
            if(IsHoverState)
                Foreground = IsDarkTheme ? DarkThemeHoverForeground : LightThemeHoverForeground;
            else
                Foreground = IsDarkTheme ? DarkThemeNormalForeground : LightThemeNormalForeground;
        }
    }
}
