using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Core;


namespace NavBarDemo {
    public class NavBarDemoModule : DemoModule {
        public NavBarDemoModule() {
            this.Loaded += new RoutedEventHandler(OnLoaded);
            this.Unloaded += new RoutedEventHandler(OnUnloaded);
            OnThemeChanged(null, null);
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            ThemeManager.ThemeChanged += new ThemeChangedRoutedEventHandler(OnThemeChanged);

            OnThemeChanged(null, null);
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            ThemeManager.ThemeChanged -= OnThemeChanged;
            OnThemeChanged(null, null);
        }

        void OnThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e) {
            if(navBarControl != null) {
                navBarControl.ClearValue(ForegroundProperty);
                navBarControl.SetResourceReference(ForegroundProperty, new DevExpress.Xpf.NavBar.Themes.CommonElementsThemeKeyExtension() { ResourceKey = DevExpress.Xpf.NavBar.Themes.CommonElementsThemeKeys.ItemForegroundBrush, ThemeName = ThemeManager.ApplicationThemeName });
            }
            ItemForegroundOnBackgroundPanel = new System.Windows.Media.SolidColorBrush(IsDarkBackgroundTheme() ? System.Windows.Media.Colors.White : System.Windows.Media.Colors.Black);
        }

        public System.Windows.Media.SolidColorBrush ItemForegroundOnBackgroundPanel {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(ItemForegroundOnBackgroundPanelProperty); }
            set { SetValue(ItemForegroundOnBackgroundPanelProperty, value); }
        }


        public static readonly DependencyProperty ItemForegroundOnBackgroundPanelProperty =
            DependencyPropertyManager.Register("ItemForegroundOnBackgroundPanel", typeof(System.Windows.Media.SolidColorBrush), typeof(NavBarDemoModule), new FrameworkPropertyMetadata(null));


        public static readonly DependencyProperty ItemForegroundProperty =
            DependencyPropertyManager.Register("ItemForeground", typeof(System.Windows.Media.SolidColorBrush), typeof(NavBarDemoModule), new FrameworkPropertyMetadata(null));



        private static bool IsDarkTheme() {
            return ThemeManager.ApplicationThemeName == "Office2010Black" || ThemeManager.ApplicationThemeName == "MetropolisDark" || ThemeManager.ApplicationThemeName == "VS2010";
        }
        private static bool IsDarkBackgroundTheme() {
            return ThemeManager.ApplicationThemeName == "MetropolisDark" || ThemeManager.ApplicationThemeName == "Office2010Black";
        }

        protected internal NavBarControl navBarControl { get; set; }

        protected virtual ExplorerBarView GetExplorerBarView() {
            return new ExplorerBarView();
        }
        protected virtual NavigationPaneView GetNavigationPaneView() {
            return new NavigationPaneView();
        }
        protected virtual SideBarView GetSideBarView() {
            return new SideBarView();
        }
        protected virtual void SelectView(object sender, RoutedEventArgs e) {
            if(navBarControl == null)
                return;
            string name = (string)((ListBoxEdit)sender).SelectedItem;
            switch(name) {
                case "Explorer Bar":
                    navBarControl.View = GetExplorerBarView();
                    break;
                case "Navigation Pane":
                    navBarControl.View = GetNavigationPaneView();
                    break;
                case "Side Bar":
                    navBarControl.View = GetSideBarView();
                    break;
                default:
                    throw new ArgumentException("Could not find specified view.");
            }
        }
    }
    public class DoubleToTextConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Double.Parse((string)value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class IntToTextConverter : MarkupExtension, IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (System.Convert.ToInt32(value)).ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Int32.Parse((string)value);
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class ItemToEnabledConverter : MarkupExtension, IValueConverter {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value != null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
    public class IntToVisibilityConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)value > 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class IntToBooleanConverter : IValueConverter {
        #region IValueConverter Members
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (int)value > 1;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class BooleanInvertAndMultiValueConverter : MarkupExtension, IMultiValueConverter {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            bool value = true;
            foreach(var obj in values)
                value &= obj is bool ? !(bool)obj : true;
            return value;
        }
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class WidthsToPaddingConverter : MarkupExtension, IMultiValueConverter {
        #region IMultiValueConverter Members

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(!(values[0] is double) || !(values[1] is double))
                return new Thickness();
            double columnWidth = (double)values[0];
            double left = 1 - columnWidth + Math.Floor(columnWidth);
            double contentWidth = (double)values[1] - left;
            double right = contentWidth - Math.Floor(contentWidth);
            return new Thickness(left, 0, right, 0);
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    public class NavBarDemoGridControl : ContentControl {
        #region Fields
        public static readonly DependencyProperty StretchProperty;
        public static readonly DependencyProperty ContentMinWidthProperty;
        public static readonly DependencyProperty ContentMaxWidthProperty;
        #endregion
        static NavBarDemoGridControl() {
            StretchProperty = DependencyProperty.Register("Stretch", typeof(bool), typeof(NavBarDemoGridControl), new PropertyMetadata(false));
            ContentMinWidthProperty = DependencyProperty.Register("ContentMinWidth", typeof(double), typeof(NavBarDemoGridControl), new PropertyMetadata(0d));
            ContentMaxWidthProperty = DependencyProperty.Register("ContentMaxWidth", typeof(double), typeof(NavBarDemoGridControl), new PropertyMetadata(double.PositiveInfinity));
        }
        public NavBarDemoGridControl() {
            this.SetDefaultStyleKey(typeof(NavBarDemoGridControl));
        }
        #region Properties
        public bool Stretch {
            get { return (bool)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        public double ContentMinWidth {
            get { return (double)GetValue(ContentMinWidthProperty); }
            set { SetValue(ContentMinWidthProperty, value); }
        }
        public double ContentMaxWidth {
            get { return (double)GetValue(ContentMaxWidthProperty); }
            set { SetValue(ContentMaxWidthProperty, value); }
        }
        #endregion
        public override void OnApplyTemplate() {
            DemoModuleControl demoControl = DemoModuleControl.FindParentDemoModuleControl(this);
            if(demoControl == null)
                return;
            NavBarControl navBar = (NavBarControl)DemoModuleControl.FindDemoContent(typeof(NavBarControl), (DependencyObject)this.Content);
            ((NavBarDemoModule)demoControl.DemoModule).navBarControl = navBar;
            demoControl.DataContext = navBar;
        }
    }
    public class RootDemoTreeViewItem : TreeViewItem {
        public RootDemoTreeViewItem() {
            Loaded += new RoutedEventHandler(OnLoaded);
            Unloaded += new RoutedEventHandler(OnUnloaded);
        }

        void OnUnloaded(object sender, RoutedEventArgs e) {
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            UpdateForeground();
        }
        public System.Windows.Media.Brush AlternateForeground {
            get { return (System.Windows.Media.Brush)GetValue(AlternateForegroundProperty); }
            set { SetValue(AlternateForegroundProperty, value); }
        }
        public static readonly DependencyProperty AlternateForegroundProperty =
            DependencyPropertyManager.Register("AlternateForeground", typeof(System.Windows.Media.Brush), typeof(RootDemoTreeViewItem), new FrameworkPropertyMetadata(new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black)));
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateForeground();
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseMove(e);
            UpdateForeground();
        }
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseLeave(e);
            UpdateForeground();
        }
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseEnter(e);
            UpdateForeground();
        }
        private void UpdateForeground() {
            if(!IsSelected && !IsMouseOver) Foreground = AlternateForeground;
            else ClearValue(ForegroundProperty);
        }
    }

    public class FreezableGetAsFrozenConverterExtension : MarkupExtension, IValueConverter {
        public FreezableGetAsFrozenConverterExtension() {

        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Freezable fvalue = value as Freezable;
            if (fvalue == null)
                return fvalue;
            return fvalue.CanFreeze ? fvalue.Clone() : fvalue.GetAsFrozen();
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}
